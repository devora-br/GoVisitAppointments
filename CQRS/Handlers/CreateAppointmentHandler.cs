using MediatR;
using GoVisit.CQRS.Commands;
using GoVisit.Infrastructure;
using GoVisit.Models;

namespace GoVisit.CQRS.Handlers
{
    public class CreateAppointmentHandler(IAppointmentRepository repo) : IRequestHandler<CreateAppointmentCommand, AppointmentCreatedResult>
    {
        private readonly IAppointmentRepository _repo = repo; 

        public async Task<AppointmentCreatedResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            if (request.CreateDto.EndAt <= request.CreateDto.StartAt)
                throw new ArgumentException("EndAt must be after StartAt");

            var isSlotAvailable = await _repo.IsSlotAvailableAsync(
                request.CreateDto.ServiceId,
                request.CreateDto.StartAt,
                request.CreateDto.EndAt,
                cancellationToken);

            if (!isSlotAvailable)
                throw new InvalidOperationException("The requested appointment time slot is already booked or unavailable.");

            var appointment = new Appointment
            {
                ServiceId = request.CreateDto.ServiceId,
                UserId = request.CreateDto.UserId,
                StartAt = request.CreateDto.StartAt,
                EndAt = request.CreateDto.EndAt,
                Notes = request.CreateDto.Notes
            };

            var created = await _repo.CreateAsync(appointment);
            return new AppointmentCreatedResult(created.Id!);
        }
    }
}
