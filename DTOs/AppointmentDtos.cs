using GoVisit.Enums;

namespace GoVisit.DTOs
{
    public record CreateAppointmentDto(string ServiceId, string UserId, DateTime StartAt, DateTime EndAt, string? Notes);
    public record UpdateAppointmentDto(string? ServiceId, DateTime? StartAt, DateTime? EndAt, AppointmentStatus? Status, string? Notes);
    public record AppointmentReadDto(string Id, string ServiceId, string UserId, DateTime StartAt, DateTime EndAt, AppointmentStatus Status, string? Notes, DateTime CreatedAt, DateTime UpdatedAt);

}
