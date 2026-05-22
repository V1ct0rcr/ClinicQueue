using eUseControl.BusinessLayer.Structure;
using eUseControl.DataAccess.Context;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Models.Responses;
using eUseControl.Domain.Models.User;

namespace eUseControl.BusinessLayer.Core;

public class UserActions
{
    internal UserData? UserLoginDataValidationExecution(UserLoginDto udata)
    {
        var passwordHash = PasswordHasher.Hash(udata.Password);

        using var db = new UserContext();
        return db.Users.FirstOrDefault(x =>
            (x.UserName == udata.CredentialType || x.Email == udata.CredentialType) &&
            x.Password == passwordHash);
    }

    internal string UserTokenGeneration(UserData user)
    {
        var token = new TokenService();
        return token.GenerateToken(user.Id, user.UserName, user.Role.ToString());
    }

    internal ActionResponse UserRegDataValidationAction(UserRegisterDto uReg)
    {
        if (string.IsNullOrWhiteSpace(uReg.UserName) ||
            string.IsNullOrWhiteSpace(uReg.Email) ||
            string.IsNullOrWhiteSpace(uReg.Password))
        {
            return new ActionResponse { IsSuccess = false, Message = "Username, Email and Password are required." };
        }

        using var db = new UserContext();
        var exists = db.Users.FirstOrDefault(x =>
            x.Email == uReg.Email || x.UserName == uReg.UserName);

        if (exists is not null)
            return new ActionResponse { IsSuccess = false, Message = "Email or username already exists." };

        var user = new UserData
        {
            FirstName = uReg.FirstName,
            LastName = uReg.LastName,
            Email = uReg.Email,
            UserName = uReg.UserName,
            Password = PasswordHasher.Hash(uReg.Password),
            Phone = uReg.Phone,
            Role = UserRole.User,
            RegisteredOn = DateTime.Now
        };

        db.Users.Add(user);
        db.SaveChanges();

        return new ActionResponse { IsSuccess = true, Message = "Registration successful." };
    }
}
