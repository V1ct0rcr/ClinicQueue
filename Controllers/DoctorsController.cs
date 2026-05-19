using eUseControl.Api.Domain.Entities.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private static readonly List<DoctorData> _doctors = new();
    private static int _nextId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<DoctorData>> GetAll()
    {
        return Ok(_doctors);
    }

    [HttpGet("{id:int}")]
    public ActionResult<DoctorData> GetById(int id)
    {
        var doctor = _doctors.FirstOrDefault(d => d.Id == id);
        if (doctor is null)
            return NotFound();

        return Ok(doctor);
    }

    [HttpPost]
    public ActionResult<DoctorData> Create([FromBody] DoctorData doctor)
    {
        if (string.IsNullOrWhiteSpace(doctor.Name) || string.IsNullOrWhiteSpace(doctor.Specialty))
            return BadRequest("Name and Specialty are required.");

        doctor.Id = _nextId++;
        _doctors.Add(doctor);

        return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor);
    }

    [HttpPut("{id:int}")]
    public ActionResult<DoctorData> Update(int id, [FromBody] DoctorData doctor)
    {
        if (string.IsNullOrWhiteSpace(doctor.Name) || string.IsNullOrWhiteSpace(doctor.Specialty))
            return BadRequest("Name and Specialty are required.");

        var existing = _doctors.FirstOrDefault(d => d.Id == id);
        if (existing is null)
            return NotFound();

        existing.Name = doctor.Name;
        existing.Specialty = doctor.Specialty;
        existing.Rating = doctor.Rating;
        existing.Location = doctor.Location;

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var existing = _doctors.FirstOrDefault(d => d.Id == id);
        if (existing is null)
            return NotFound();

        _doctors.Remove(existing);
        return NoContent();
    }
}
