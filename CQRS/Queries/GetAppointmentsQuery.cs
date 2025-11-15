using MediatR;
using GoVisit.DTOs;

namespace GoVisit.CQRS.Queries
{
    public record GetAppointmentsQuery(int Limit = 50) : IRequest<IEnumerable<AppointmentReadDto>>;
}

