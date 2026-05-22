using eUseControl.BusinessLayer.Core;
using eUseControl.BusinessLayer.Interfaces;
using eUseControl.Domain.Models.Appointment;
using eUseControl.Domain.Models.Responses;

namespace eUseControl.BusinessLayer.Structure;

public class AppointmentActionExecution : AppointmentActions, IAppointmentAction
{
    public List<AppointmentDto> GetAll() => GetAllExecution();

    public AppointmentDto? GetById(int id) => GetByIdExecution(id);

    public ActionResponse<AppointmentDto> Create(AppointmentDto dto) => CreateExecution(dto);

    public ActionResponse<AppointmentDto> Update(int id, AppointmentDto dto) => UpdateExecution(id, dto);

    public ActionResponse Delete(int id) => DeleteExecution(id);
}
