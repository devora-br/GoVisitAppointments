using GoVisit.Models;
using GoVisit.Settings;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace GoVisit.Infrastructure
{
    public class MongoAppointmentRepository : IAppointmentRepository
    {
        private readonly IMongoCollection<Appointment> _collection;

        public MongoAppointmentRepository(IMongoClientFactory clientFactory, IOptions<MongoDbSettings> settings)
        {
            var client = clientFactory.GetClient();
            var db = client.GetDatabase(settings.Value.DatabaseName);
            _collection = db.GetCollection<Appointment>(settings.Value.AppointmentsCollection);
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            appointment.CreatedAt = DateTime.UtcNow;
            appointment.UpdatedAt = appointment.CreatedAt;
            await _collection.InsertOneAsync(appointment);
            return appointment;
        }

        public async Task<Appointment?> GetByIdAsync(string id)
        {
            var filter = Builders<Appointment>.Filter.Eq(a => a.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync(int limit = 50)
        {
            return await _collection.Find(_ => true).Limit(limit).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            appointment.UpdatedAt = DateTime.UtcNow;
            var filter = Builders<Appointment>.Filter.Eq(a => a.Id, appointment.Id);
            var result = await _collection.ReplaceOneAsync(filter, appointment);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(a => a.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
