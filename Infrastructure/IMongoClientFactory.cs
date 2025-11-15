using MongoDB.Driver;

namespace GoVisit.Infrastructure
{
    public interface IMongoClientFactory
    {
        IMongoClient GetClient();
    }
}
