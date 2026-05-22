using eUseControl.Domain.Models.Responses;
using eUseControl.Domain.Models.User;

namespace eUseControl.BusinessLayer.Interfaces;

public interface IUserLoginAction
{
    ActionResponse UserLoginDataValidation(UserLoginDto udata);
}
