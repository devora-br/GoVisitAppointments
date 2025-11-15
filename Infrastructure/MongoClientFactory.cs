using MongoDB.Driver;

namespace GoVisit.Infrastructure
{
    public class MongoClientFactory : IMongoClientFactory
    {
        private readonly string _connectionString;
        private readonly MongoClient _client;

        public MongoClientFactory(string connectionString)
        {
            _connectionString = connectionString;
            _client = new MongoClient(_connectionString);
        }     

        public IMongoClient GetClient() => _client;
    }
}
