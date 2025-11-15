using Microsoft.AspNetCore.Mvc;
using MediatR;
using GoVisit.CQRS.Commands;
using GoVisit.CQRS.Queries;
using GoVisit.DTOs;

namespace GoVisit.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AppointmentsController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Create appointment
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            var result = await _mediator.Send(new CreateAppointmentCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Update appointment (partial)
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAppointmentDto updateDto)
        {
            var ok = await _mediator.Send(new UpdateAppointmentCommand(id, updateDto));
            if (!ok) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete appointment
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ok = await _mediator.Send(new DeleteAppointmentCommand(id));
            if (!ok) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Get list of appointments
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int limit = 50)
        {
            var list = await _mediator.Send(new GetAppointmentsQuery(limit));
            return Ok(list);
        }

        /// <summary>
        /// Get appointment by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var a = await _mediator.Send(new GetAppointmentByIdQuery(id));
            if (a is null) return NotFound();
            return Ok(a);
        }
    }
}
