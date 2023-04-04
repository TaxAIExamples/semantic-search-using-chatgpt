using AI.Dev.OpenAI.GPT;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SemanticSearch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTInterface
{
    public class ChatGPT : IChatGPT
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public ChatGPT(
            IConfiguration configuration,
            ApplicationDbContext context
            )
        {
            _configuration = configuration;
            _context = context;
        }

        public EmbeddingResponse GetEmbedding(string embedding)
        {
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (apiKey == null)
            {
                throw new Exception("OPEN AI KEY not available");
            }

            string apiUrl = _configuration.GetSection("Embeddings").GetValue<string>("URL");

            // Create a new HttpClient instance
            HttpClient client = new HttpClient();

            // Set the API key in the Authorization header
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            // Set the content type to JSON
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Create the JSON payload
            var payload = new
            {
                model = _configuration.GetSection("Embeddings").GetValue<string>("Model"),
                input = embedding
            };
            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

            // Create the request content
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Send the request
            var response = client.PostAsync(apiUrl, content).GetAwaiter().GetResult();

            // Check if the API call was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (responseContent == null)
                {
                    throw new Exception($"Error: responseContent is null");
                }

                // Deserialize the JSON response into a C# class
                EmbeddingResponse embeddingResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<EmbeddingResponse>(responseContent);

                return embeddingResponse;
            }
            else
            {
                // Handle the error
                throw new Exception($"Error: {response.StatusCode} - {response.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");
            }
        }

        double[] GetEmbeddingScores(string embedding)
        {
            EmbeddingResponse response = GetEmbedding(embedding);
            return (response.Data[0].Embedding);
        }

        private List<Similarity> GetSimilarityScoreForQuestion(string question)
        {
            double[] questionEmbeddings = GetEmbeddingScores(question);
            string stringVector = string.Join(",", questionEmbeddings);

            // Set up connection string
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Set up SQL command
            string commandText = _configuration.GetSection("Similarity").GetValue<string>("SimilarityScoreSPName");
            SqlCommand command = new SqlCommand(commandText);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            // Add parameters
            SqlParameter csvListParam = new SqlParameter("@csvList", System.Data.SqlDbType.VarChar);
            csvListParam.Value = stringVector;
            command.Parameters.Add(csvListParam);

            SqlParameter maxResultsParam = new SqlParameter("@maxResults", System.Data.SqlDbType.Int);
            maxResultsParam.Value = _configuration.GetSection("Similarity").GetValue<int>("MaxResults");
            command.Parameters.Add(maxResultsParam);

            // Set up connection
            SqlConnection connection = new SqlConnection(connectionString);
            command.Connection = connection;

            // Open connection and execute command
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Read data into a list of objects
            List<Similarity> resultList = new List<Similarity>();
            while (reader.Read())
            {
                resultList.Add(new Similarity
                {
                    KnowledgeRecordId = Convert.ToInt32(reader["id"]),
                    SimilarityScore = Convert.ToDouble(reader["similarity"])
                }
                );
            }

            // Close connection and reader
            reader.Close();
            connection.Close();

            return resultList;
        }

        private (string context, List<KnowledgeRecordBasicContent> contextList) GetContextForQuestion(string question)
        {
            List<Similarity> resultList = GetSimilarityScoreForQuestion(question);
            if (resultList.Count == 0)
            {
                return ("", new List<KnowledgeRecordBasicContent>());
            }

            List<KnowledgeRecordBasicContent> theContextList = new List<KnowledgeRecordBasicContent>();
            double similarityScoreThreshold = _configuration.GetSection("Completions").GetValue<double>("MinSimilarityThreshold");
            // Add the number of tokens to each result
            foreach (var item in resultList)
            {
                KnowledgeRecord theRecord = _context.KnowledgeRecords.Where(p => p.Id == item.KnowledgeRecordId).AsNoTracking().FirstOrDefault();
                item.Tokens = theRecord.Tokens;
                item.Text = theRecord.Content;

                if(item.SimilarityScore >= similarityScoreThreshold)
                {
                    theContextList.Add(new KnowledgeRecordBasicContent()
                    {
                        ID = item.KnowledgeRecordId,
                        Title = theRecord.Title,
                        Content = theRecord.Content,
                        SimilarityScore = item.SimilarityScore
                    });
                }
            }

            int maxSectionLen = _configuration.GetSection("Completions").GetValue<int>("MaxSectionLen");
            string separator = "\n* ";

            List<int> tokens = GPT3Tokenizer.Encode(separator);
            int separatorTokens = tokens.Count;

            int totalLenght = 0;

            StringBuilder context = new StringBuilder();
            foreach (var item in resultList)
            {
                if (context.Length == 0)
                {
                    context.Append(item.Text);
                }
                else
                {
                    context.Append(separator);
                    context.Append(item.Text);
                }

                totalLenght += item.Tokens;
                if (totalLenght > maxSectionLen)
                {
                    break;
                }
            }

            return (context.ToString(), theContextList);
        }

        private (string prompt, List<KnowledgeRecordBasicContent> contextList) GetPromptForQuestion(string question, bool briefDetails)
        {
            (string context, List<KnowledgeRecordBasicContent> contextList) = GetContextForQuestion(question);

            if (String.IsNullOrEmpty(context))
            {
                return ("", new List<KnowledgeRecordBasicContent>());
            }

            // TODO:  Get this from some configuration or environment variable
            string header = "Answer the question as honestly as possible using the context text provided " +
                "and if the answer is not included in the text below, just say \"I don't know\".  " +
                "Avoid repeating the question";
            if (briefDetails)
            {
                header = header + " and be as brief as possible.";
            }
            else
            {
                header = header + " and provide as much detail as possible.";
            }

            header = header + "  The Context ends with the string \">>>>>\"\r\n\r\nContext: <<<<< \r\n";

            return (header + context + "\r\n>>>>>\r\n\r\n Question: " + question + "\r\n Answer:", contextList);
        }

        public (string answer, List<KnowledgeRecordBasicContent> contextList) GetChatGPTAnswerForQuestion(string question, bool briefDetails)
        {
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (apiKey == null)
            {
                throw new Exception("OPEN AI KEY not available");
            }

            string apiUrl = _configuration.GetSection("Completions").GetValue<string>("URL");

            // Create a new HttpClient instance
            HttpClient client = new HttpClient();

            // Set the API key in the Authorization header
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            // Set the content type to JSON
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Create the JSON payload
            (string prompt, List<KnowledgeRecordBasicContent> contextList) = GetPromptForQuestion(question, briefDetails);

            if (String.IsNullOrEmpty(prompt))
            {
                return ("I don't know", new List<KnowledgeRecordBasicContent>());
            }

            var payload = new
            {
                model = _configuration.GetSection("Completions").GetValue<string>("Model"),
                prompt = prompt,
                max_tokens = _configuration.GetSection("Completions").GetValue<int>("MaxTokens"),
                temperature = _configuration.GetSection("Completions").GetValue<int>("Temperature")
            };

            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

            // Create the request content
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Send the request
            var response = client.PostAsync(apiUrl, content).GetAwaiter().GetResult();

            // Check if the API call was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (responseContent == null)
                {
                    throw new Exception($"Error: responseContent is null");
                }

                // Deserialize the JSON response into a C# class
                CompletionResponse completionResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CompletionResponse>(responseContent);

                if (completionResponse == null)
                {
                    throw new Exception($"Error: completionResponse is null");
                }

                return (completionResponse.Choices[0].Text, contextList);
            }
            else
            {
                // Handle the error
                throw new Exception($"Error: {response.StatusCode} - {response.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");
            }
        }
    }
}
