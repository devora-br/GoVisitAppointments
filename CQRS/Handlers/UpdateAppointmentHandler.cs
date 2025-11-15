using MediatR;
using GoVisit.CQRS.Commands;
using GoVisit.Infrastructure;

namespace GoVisit.CQRS.Handlers
{
    public class UpdateAppointmentHandler : IRequestHandler<UpdateAppointmentCommand, bool>
    {
        private readonly IAppointmentRepository _repo;

        public UpdateAppointmentHandler(IAppointmentRepository repo) => _repo = repo;

        public async Task<bool> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing is null) return false;
            if (request.UpdateDto.ServiceId is not null) existing.ServiceId = request.UpdateDto.ServiceId;
            if (request.UpdateDto.StartAt.HasValue) existing.StartAt = request.UpdateDto.StartAt.Value;
            if (request.UpdateDto.EndAt.HasValue) existing.EndAt = request.UpdateDto.EndAt.Value;
            if (request.UpdateDto.Status.HasValue) existing.Status = request.UpdateDto.Status.Value;
            if (request.UpdateDto.Notes is not null) existing.Notes = request.UpdateDto.Notes;
            if (existing.EndAt <= existing.StartAt) throw new ArgumentException("EndAt must be after StartAt");

            return await _repo.UpdateAsync(existing);
        }
    }
}
