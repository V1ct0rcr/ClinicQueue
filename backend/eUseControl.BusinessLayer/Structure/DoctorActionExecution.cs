using eUseControl.BusinessLayer.Core;
using eUseControl.BusinessLayer.Interfaces;
using eUseControl.Domain.Models.Doctor;
using eUseControl.Domain.Models.Responses;

namespace eUseControl.BusinessLayer.Structure;

public class DoctorActionExecution : DoctorActions, IDoctorAction
{
    public List<DoctorDto> GetAll() => GetAllExecution();

    public DoctorDto? GetById(int id) => GetByIdExecution(id);

    public ActionResponse<DoctorDto> Create(DoctorDto dto) => CreateExecution(dto);

    public ActionResponse<DoctorDto> Update(int id, DoctorDto dto) => UpdateExecution(id, dto);

    public ActionResponse Delete(int id) => DeleteExecution(id);
}
