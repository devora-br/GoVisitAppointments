using Microsoft.AspNetCore.Mvc;
using MediatR;
using GoVisit.CQRS.Commands;
using GoVisit.CQRS.Queries;
using GoVisit.DTOs;

namespace GoVisit.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Create appointment
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Createappointment([FromBody] CreateAppointmentDto dto)
        {
            var appointment = await mediator.Send(new CreateAppointmentCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
        }

        /// <summary>
        /// Update appointment (partial)
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Updateappointment(string id, [FromBody] UpdateAppointmentDto updateDto)
        {
            var ok = await mediator.Send(new UpdateAppointmentCommand(id, updateDto));
            if (!ok) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete appointment
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(string id)
        {
            var ok = await mediator.Send(new DeleteAppointmentCommand(id));
            if (!ok) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Get list of appointments
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAppointments([FromQuery] int limit = 50)
        {
            var appointments = await mediator.Send(new GetAppointmentsQuery(limit));
            return Ok(appointments);
        }

        /// <summary>
        /// Get appointment by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var appointment = await mediator.Send(new GetAppointmentByIdQuery(id));
            if (appointment is null) return NotFound();
            return Ok(appointment);
        }
    }
}
