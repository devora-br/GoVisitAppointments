// File: CQRS/Handlers/GetAppointmentHandlers.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using GoVisit.CQRS.Queries;
using GoVisit.Infrastructure;
using GoVisit.DTOs;
using System.Linq;

namespace GoVisit.CQRS.Handlers
{
    public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentReadDto?>
    {
        private readonly IAppointmentRepository _repo;
        public GetAppointmentByIdHandler(IAppointmentRepository repo) => _repo = repo;

        public async Task<AppointmentReadDto?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var a = await _repo.GetByIdAsync(request.Id);
            if (a is null) return null;
            return new AppointmentReadDto(a.Id!, a.ServiceId, a.UserId, a.StartAt, a.EndAt, a.Status, a.Notes, a.CreatedAt, a.UpdatedAt);
        }
    }

    public class GetAppointmentsHandler : IRequestHandler<GetAppointmentsQuery, IEnumerable<AppointmentReadDto>>
    {
        private readonly IAppointmentRepository _repo;
        public GetAppointmentsHandler(IAppointmentRepository repo) => _repo = repo;

        public async Task<IEnumerable<AppointmentReadDto>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var list = await _repo.GetAllAsync(request.Limit);
            return list.Select(a => new AppointmentReadDto(a.Id!, a.ServiceId, a.UserId, a.StartAt, a.EndAt, a.Status, a.Notes, a.CreatedAt, a.UpdatedAt));
        }
    }
}
