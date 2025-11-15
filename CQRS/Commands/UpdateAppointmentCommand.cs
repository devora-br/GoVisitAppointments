using MediatR;
using GoVisit.DTOs;

namespace GoVisit.CQRS.Commands
{
    public record UpdateAppointmentCommand(string Id, UpdateAppointmentDto UpdateDto) : IRequest<bool>;
}

