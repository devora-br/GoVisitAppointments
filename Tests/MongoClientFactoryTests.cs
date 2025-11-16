using Moq;
using Xunit;
using Microsoft.Extensions.Options;

using GoVisit.Infrastructure;
using GoVisit.Settings;
namespace GoVisit.Tests
{

    public class MongoClientFactoryTests
    {
        [Fact]
        public void GetClient_ShouldReturnSameInstance()
        {
            var settings = Options.Create(new MongoDbSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "testdb"
            });

            var factory = new MongoClientFactory(settings.Value.ConnectionString);

            var client1 = factory.GetClient();
            var client2 = factory.GetClient();

            Assert.Same(client1, client2);
        }

        [Fact]
        public void GetClient_ShouldNotReturnNull()
        {
        var settings = Options.Create(new MongoDbSettings
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "testdb"
        });

        var factory = new MongoClientFactory(settings.Value.ConnectionString);

        var client = factory.GetClient();

        Assert.NotNull(client);
        }
    }
}

