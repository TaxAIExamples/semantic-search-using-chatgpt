using AI.Dev.OpenAI.GPT;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SemanticSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ChatGPTInterface
{
    public class KnowledgeRecordManager
    {
        private readonly AppDBContext context;
        public KnowledgeRecordManager(IConfiguration configuration)
        {
            context = new AppDBContext(configuration);
            context.Database.EnsureCreated();
        }

        public KnowledgeRecord AddRecord(KnowledgeRecord newRecord, IConfiguration configuration)
        {
            KnowledgeRecord recordAdded;

            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    List<int> tokens = GPT3Tokenizer.Encode(newRecord.Content);
                    newRecord.Tokens = tokens.Count;

                    // Ahora, tenemos que conseguir los encodings del text

                    var embeddingResult = ChatGPTInterface.GetEmbedding(newRecord.Content, configuration);

                    if (embeddingResult == null)
                    {
                        throw new Exception($"No embeddings are available");
                    }

                    if (embeddingResult.Data[0].Embedding.Length != 1536)
                    {
                        throw new Exception($"Expected 1536 values in embedding vector but got {embeddingResult.Data[0].Embedding.Length} instead");
                    }

                    newRecord.KnowledgeVector = new List<KnowledgeVectorItem>();
                    foreach (var item in embeddingResult.Data[0].Embedding)
                    {
                        newRecord.KnowledgeVector.Add(new KnowledgeVectorItem() { Id = 0, VectorValue = item });
                    }

                    recordAdded = context.KnowledgeRecords.Add(newRecord).Entity;

                    context.SaveChanges();

                    transactionScope.Complete();

                }
                catch (Exception ex)
                {
                    // If an exception is thrown, the transaction is rolled back.
                    throw;
                }

                return recordAdded;
            }
        }

        public void ModifyRecord(KnowledgeRecord record, IConfiguration configuration)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    List<int> tokens = GPT3Tokenizer.Encode(record.Content);
                    record.Tokens = tokens.Count;

                    // Primero tenemos que borrar el vector anterior
                    if (record.KnowledgeVector != null)
                    {
                        foreach (var item in record.KnowledgeVector)
                        {
                            context.KnowledgeVectorItems.Remove(item);
                        }
                    }

                    // Ahora, tenemos que conseguir los encodings del text
                    var embeddingResult = ChatGPTInterface.GetEmbedding(record.Content, configuration);

                    if (embeddingResult == null)
                    {
                        throw new Exception($"No embeddings are available");
                    }

                    if (embeddingResult.Data[0].Embedding.Length != 1536)
                    {
                        throw new Exception($"Expected 1536 values in embedding vector but got {embeddingResult.Data[0].Embedding.Length} instead");
                    }

                    record.KnowledgeVector = new List<KnowledgeVectorItem>();
                    foreach (var item in embeddingResult.Data[0].Embedding)
                    {
                        record.KnowledgeVector.Add(new KnowledgeVectorItem() { Id = 0, VectorValue = item });
                    }

                    context.KnowledgeRecords.Update(record);

                    context.SaveChanges();

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    // If an exception is thrown, the transaction is rolled back.
                    throw;
                }
            }
        }

        public void DeleteRecord(int id)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    KnowledgeRecord? recordToDelete = context.KnowledgeRecords
                        .Where(p => p.Id == id)
                        .Include(p => p.KnowledgeVector)
                        .AsTracking()
                        .FirstOrDefault();

                    if (recordToDelete != null)
                    {
                        if (recordToDelete.KnowledgeVector != null)
                        {
                            foreach (var item in recordToDelete.KnowledgeVector)
                            {
                                context.KnowledgeVectorItems.Remove(item);
                            }
                        }

                        context.KnowledgeRecords.Remove(recordToDelete);

                        context.SaveChanges();
                    }

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    // If an exception is thrown, the transaction is rolled back.
                    throw;
                }
            }
        }

        public List<KnowledgeRecord> GetAllRecordsNoTracking()
        {
            return context.KnowledgeRecords
                .AsNoTracking()
                .ToList();
        }

        public KnowledgeRecord GetSingleRecord(int id)
        {
            return context.KnowledgeRecords.Find(id);
        }

        public KnowledgeRecord GetSingleRecordNoTrackin(int id)
        {
            return context.KnowledgeRecords.Where(p => p.Id == id).AsNoTracking().FirstOrDefault();
        }

        public List<KnowledgeRecord> GetAllRecordsNoTracking(string searchTerm)
        {
            return context.KnowledgeRecords
                .Where(r => EF.Functions.Like(r.Title, "%" + searchTerm + "%") || EF.Functions.Like(r.Content, "%" + searchTerm + "%"))
                .AsNoTracking()
                .ToList();
        }
    }
}
