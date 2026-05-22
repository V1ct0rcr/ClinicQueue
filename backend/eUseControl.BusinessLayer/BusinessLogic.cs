using eUseControl.BusinessLayer.Interfaces;
using eUseControl.BusinessLayer.Structure;

namespace eUseControl.BusinessLayer;

public class BusinessLogic
{
    public BusinessLogic() { }

    public IDoctorAction DoctorAction() => new DoctorActionExecution();

    public IPatientAction PatientAction() => new PatientActionExecution();

    public IAppointmentAction AppointmentAction() => new AppointmentActionExecution();

    // Etapa 5 - Auth & RBAC
    public IUserLoginAction UserLoginAction() => new UserAuthAction();

    public IUserRegAction UserRegAction() => new UserRegActionExecution();
}
