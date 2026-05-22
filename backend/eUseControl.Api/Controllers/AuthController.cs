using eUseControl.BusinessLayer;
using eUseControl.BusinessLayer.Interfaces;
using eUseControl.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controllers;

[Route("api/session")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserLoginAction _userAction;

    public AuthController()
    {
        var bl = new BusinessLogic();
        _userAction = bl.UserLoginAction();
    }

    [HttpPost("auth")]
    [AllowAnonymous]
    public IActionResult Auth([FromBody] UserLoginDto udata)
    {
        var result = _userAction.UserLoginDataValidation(udata);

        if (!result.IsSuccess)
            return Unauthorized(result.Message);

        return Ok(new { token = result.Message });
    }
}
