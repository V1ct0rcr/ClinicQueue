using eUseControl.Api.Domain.Entities.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private static readonly List<AppointmentData> _appointments = new();
    private static int _nextId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<AppointmentData>> GetAll()
    {
        return Ok(_appointments);
    }

    [HttpGet("{id:int}")]
    public ActionResult<AppointmentData> GetById(int id)
    {
        var appointment = _appointments.FirstOrDefault(a => a.Id == id);
        if (appointment is null)
            return NotFound();

        return Ok(appointment);
    }

    [HttpPost]
    public ActionResult<AppointmentData> Create([FromBody] AppointmentData appointment)
    {
        var validationError = ValidateAppointment(appointment);
        if (validationError is not null)
            return BadRequest(validationError);

        if (string.IsNullOrWhiteSpace(appointment.Status))
            appointment.Status = "Pending";

        appointment.Id = _nextId++;
        _appointments.Add(appointment);

        return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
    }

    [HttpPut("{id:int}")]
    public ActionResult<AppointmentData> Update(int id, [FromBody] AppointmentData appointment)
    {
        var validationError = ValidateAppointment(appointment);
        if (validationError is not null)
            return BadRequest(validationError);

        var existing = _appointments.FirstOrDefault(a => a.Id == id);
        if (existing is null)
            return NotFound();

        existing.PatientId = appointment.PatientId;
        existing.DoctorId = appointment.DoctorId;
        existing.AppointmentDate = appointment.AppointmentDate;
        existing.Status = string.IsNullOrWhiteSpace(appointment.Status) ? "Pending" : appointment.Status;

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var existing = _appointments.FirstOrDefault(a => a.Id == id);
        if (existing is null)
            return NotFound();

        _appointments.Remove(existing);
        return NoContent();
    }

    private static string? ValidateAppointment(AppointmentData appointment)
    {
        if (appointment.DoctorId <= 0 || appointment.PatientId <= 0)
            return "DoctorId and PatientId must be greater than 0.";

        if (appointment.AppointmentDate < DateTime.Now)
            return "AppointmentDate cannot be in the past.";

        return null;
    }
}
