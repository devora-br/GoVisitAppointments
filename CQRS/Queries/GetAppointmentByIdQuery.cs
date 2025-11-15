using MediatR;
using GoVisit.DTOs;

namespace GoVisit.CQRS.Queries
{
    public record GetAppointmentByIdQuery(string Id) : IRequest<AppointmentReadDto?>;
}
