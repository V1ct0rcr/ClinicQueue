using eUseControl.BusinessLayer;
using eUseControl.BusinessLayer.Interfaces;
using eUseControl.Domain.Models.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientAction _patientAction;

    public PatientsController()
    {
        var bl = new BusinessLogic();
        _patientAction = bl.PatientAction();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ActionResult<List<PatientDto>> GetAll()
    {
        return Ok(_patientAction.GetAll());
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<PatientDto> GetById(int id)
    {
        var patient = _patientAction.GetById(id);
        if (patient is null)
            return NotFound();

        return Ok(patient);
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult<PatientDto> Create([FromBody] PatientDto dto)
    {
        var result = _patientAction.Create(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<PatientDto> Update(int id, [FromBody] PatientDto dto)
    {
        if (_patientAction.GetById(id) is null)
            return NotFound();

        var result = _patientAction.Update(id, dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        if (_patientAction.GetById(id) is null)
            return NotFound();

        _patientAction.Delete(id);
        return NoContent();
    }
}
