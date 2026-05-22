using eUseControl.Domain.Models.Appointment;
using eUseControl.Domain.Models.Responses;

namespace eUseControl.BusinessLayer.Interfaces;

public interface IAppointmentAction
{
    List<AppointmentDto> GetAll();
    AppointmentDto? GetById(int id);
    ActionResponse<AppointmentDto> Create(AppointmentDto dto);
    ActionResponse<AppointmentDto> Update(int id, AppointmentDto dto);
    ActionResponse Delete(int id);
}
