using eUseControl.BusinessLayer;
using eUseControl.BusinessLayer.Interfaces;
using eUseControl.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controllers;

[Route("api/reg")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IUserRegAction _userReg;

    public RegisterController()
    {
        var bl = new BusinessLogic();
        _userReg = bl.UserRegAction();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Register([FromBody] UserRegisterDto uRegData)
    {
        var result = _userReg.UserRegDataValidation(uRegData);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }
}
