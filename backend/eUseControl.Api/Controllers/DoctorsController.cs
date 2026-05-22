using eUseControl.BusinessLayer;
using eUseControl.BusinessLayer.Interfaces;
using eUseControl.Domain.Models.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorAction _doctorAction;

    public DoctorsController()
    {
        var bl = new BusinessLogic();
        _doctorAction = bl.DoctorAction();
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<DoctorDto>> GetAll()
    {
        return Ok(_doctorAction.GetAll());
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public ActionResult<DoctorDto> GetById(int id)
    {
        var doctor = _doctorAction.GetById(id);
        if (doctor is null)
            return NotFound();

        return Ok(doctor);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult<DoctorDto> Create([FromBody] DoctorDto dto)
    {
        var result = _doctorAction.Create(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<DoctorDto> Update(int id, [FromBody] DoctorDto dto)
    {
        if (_doctorAction.GetById(id) is null)
            return NotFound();

        var result = _doctorAction.Update(id, dto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        if (_doctorAction.GetById(id) is null)
            return NotFound();

        _doctorAction.Delete(id);
        return NoContent();
    }
}
