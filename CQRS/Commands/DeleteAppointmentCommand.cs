using MediatR;

namespace GoVisit.CQRS.Commands
{
    public record DeleteAppointmentCommand(string Id) : IRequest<bool>;
}
