using GoVisit.Models;


namespace GoVisit.Infrastructure
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAsync(Appointment appointment);
        Task<Appointment?> GetByIdAsync(string id);
        Task<IEnumerable<Appointment>> GetAllAsync(int limit = 50);
        Task<bool> UpdateAsync(Appointment appointment);
        Task<bool> DeleteAsync(string id);
        Task<bool> IsSlotAvailableAsync(string serviceId, DateTime startAt, DateTime endAt, CancellationToken cancellationToken);
    }
}
