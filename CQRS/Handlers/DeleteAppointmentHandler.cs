using MediatR;
using GoVisit.CQRS.Commands;
using GoVisit.Infrastructure;

namespace GoVisit.CQRS.Handlers
{
    public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointmentCommand, bool>
    {
        private readonly IAppointmentRepository _repo;

        public DeleteAppointmentHandler(IAppointmentRepository repo) => _repo = repo;

        public async Task<bool> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteAsync(request.Id);
        }
    }
}
