using MediatR;
using GoVisit.DTOs;

namespace GoVisit.CQRS.Commands
{
    public record CreateAppointmentCommand(CreateAppointmentDto CreateDto) : IRequest<AppointmentCreatedResult>;
    public record AppointmentCreatedResult(string Id);
}

