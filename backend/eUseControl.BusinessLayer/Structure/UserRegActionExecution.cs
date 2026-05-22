using eUseControl.BusinessLayer.Core;
using eUseControl.BusinessLayer.Interfaces;
using eUseControl.Domain.Models.Responses;
using eUseControl.Domain.Models.User;

namespace eUseControl.BusinessLayer.Structure;

public class UserRegActionExecution : UserActions, IUserRegAction
{
    public ActionResponse UserRegDataValidation(UserRegisterDto uReg)
    {
        return UserRegDataValidationAction(uReg);
    }
}
