namespace ChatGPTInterface
{
    public interface IChatGPT
    {
        (string answer, List<KnowledgeRecordBasicContent> contextList) GetChatGPTAnswerForQuestion(string question, bool briefDetails);
        EmbeddingResponse GetEmbedding(string embedding);
    }
}