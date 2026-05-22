using eUseControl.BusinessLayer.Core;
using eUseControl.BusinessLayer.Interfaces;
using eUseControl.Domain.Models.Responses;
using eUseControl.Domain.Models.User;

namespace eUseControl.BusinessLayer.Structure;

public class UserAuthAction : UserActions, IUserLoginAction
{
    public ActionResponse UserLoginDataValidation(UserLoginDto udata)
    {
        var user = UserLoginDataValidationExecution(udata);
        if (user is null)
            return new ActionResponse { IsSuccess = false, Message = "Invalid username or password." };

        var token = UserTokenGeneration(user);
        return new ActionResponse { IsSuccess = true, Message = token };
    }
}
