using eUseControl.BusinessLayer;
using eUseControl.BusinessLayer.Interfaces;
using eUseControl.Domain.Models.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentAction _appointmentAction;

    public AppointmentsController()
    {
        var bl = new BusinessLogic();
        _appointmentAction = bl.AppointmentAction();
    }

    [HttpGet]
    public ActionResult<List<AppointmentDto>> GetAll()
    {
        return Ok(_appointmentAction.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<AppointmentDto> GetById(int id)
    {
        var appointment = _appointmentAction.GetById(id);
        if (appointment is null)
            return NotFound();

        return Ok(appointment);
    }

    [HttpPost]
    public ActionResult<AppointmentDto> Create([FromBody] AppointmentDto dto)
    {
        var result = _appointmentAction.Create(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpPut("{id:int}")]
    public ActionResult<AppointmentDto> Update(int id, [FromBody] AppointmentDto dto)
    {
        if (_appointmentAction.GetById(id) is null)
            return NotFound();

        var result = _appointmentAction.Update(id, dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_appointmentAction.GetById(id) is null)
            return NotFound();

        _appointmentAction.Delete(id);
        return NoContent();
    }
}
