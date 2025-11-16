using Xunit;
using Moq;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using GoVisit.Infrastructure;
using GoVisit.Models;
using GoVisit.Settings;

namespace GoVisit.Tests
{
    public class MongoAppointmentRepositoryTests
    {
        private readonly Mock<IMongoClientFactory> _clientFactoryMock;
        private readonly Mock<IMongoDatabase> _databaseMock;
        private readonly Mock<IMongoCollection<Appointment>> _collectionMock;
        private readonly Mock<IOptions<MongoDbSettings>> _settingsMock;
        private readonly MongoAppointmentRepository _repository;

        public MongoAppointmentRepositoryTests()
        {
            _clientFactoryMock = new Mock<IMongoClientFactory>();
            var clientMock = new Mock<MongoDB.Driver.IMongoClient>();
            _databaseMock = new Mock<IMongoDatabase>();
            _collectionMock = new Mock<IMongoCollection<Appointment>>();
            _settingsMock = new Mock<IOptions<MongoDbSettings>>();

            var settings = new MongoDbSettings
            {
                DatabaseName = "TestDb",
                AppointmentsCollection = "Appointments"
            };
            _settingsMock.Setup(x => x.Value).Returns(settings);

            _clientFactoryMock.Setup(f => f.GetClient()).Returns(clientMock.Object);
            clientMock.Setup(c => c.GetDatabase(settings.DatabaseName, null)).Returns(_databaseMock.Object);
            _databaseMock.Setup(d => d.GetCollection<Appointment>(settings.AppointmentsCollection, null)).Returns(_collectionMock.Object);

            _repository = new MongoAppointmentRepository(_clientFactoryMock.Object, _settingsMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldInsertAppointmentAndReturnIt()
        {
            var appointment = new Appointment { Id = "1", ServiceId = "service-1" };
            _collectionMock.Setup(c => c.InsertOneAsync(appointment, null, default)).Returns(Task.CompletedTask);

            var result = await _repository.CreateAsync(appointment);

            _collectionMock.Verify(c => c.InsertOneAsync(appointment, null, default), Times.Once);
            Assert.Equal(appointment, result);
            Assert.True(appointment.CreatedAt <= DateTime.UtcNow);
            Assert.Equal(appointment.CreatedAt, appointment.UpdatedAt);
        }
    }
}