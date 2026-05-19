using eUseControl.Api.Domain.Entities.Patient;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private static readonly List<PatientData> _patients = new();
    private static int _nextId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<PatientData>> GetAll()
    {
        return Ok(_patients);
    }

    [HttpGet("{id:int}")]
    public ActionResult<PatientData> GetById(int id)
    {
        var patient = _patients.FirstOrDefault(p => p.Id == id);
        if (patient is null)
            return NotFound();

        return Ok(patient);
    }

    [HttpPost]
    public ActionResult<PatientData> Create([FromBody] PatientData patient)
    {
        if (string.IsNullOrWhiteSpace(patient.FirstName) || string.IsNullOrWhiteSpace(patient.Phone))
            return BadRequest("FirstName and Phone are required.");

        patient.Id = _nextId++;
        _patients.Add(patient);

        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
    }

    [HttpPut("{id:int}")]
    public ActionResult<PatientData> Update(int id, [FromBody] PatientData patient)
    {
        if (string.IsNullOrWhiteSpace(patient.FirstName) || string.IsNullOrWhiteSpace(patient.Phone))
            return BadRequest("FirstName and Phone are required.");

        var existing = _patients.FirstOrDefault(p => p.Id == id);
        if (existing is null)
            return NotFound();

        existing.FirstName = patient.FirstName;
        existing.LastName = patient.LastName;
        existing.Phone = patient.Phone;
        existing.Email = patient.Email;

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var existing = _patients.FirstOrDefault(p => p.Id == id);
        if (existing is null)
            return NotFound();

        _patients.Remove(existing);
        return NoContent();
    }
}
