using eUseControl.BusinessLayer.Core;
using eUseControl.BusinessLayer.Interfaces;
using eUseControl.Domain.Models.Patient;
using eUseControl.Domain.Models.Responses;

namespace eUseControl.BusinessLayer.Structure;

public class PatientActionExecution : PatientActions, IPatientAction
{
    public List<PatientDto> GetAll() => GetAllExecution();

    public PatientDto? GetById(int id) => GetByIdExecution(id);

    public ActionResponse<PatientDto> Create(PatientDto dto) => CreateExecution(dto);

    public ActionResponse<PatientDto> Update(int id, PatientDto dto) => UpdateExecution(id, dto);

    public ActionResponse Delete(int id) => DeleteExecution(id);
}
