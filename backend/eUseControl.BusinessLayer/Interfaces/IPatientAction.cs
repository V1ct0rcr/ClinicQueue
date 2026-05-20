using eUseControl.Domain.Models.Patient;
using eUseControl.Domain.Models.Responses;

namespace eUseControl.BusinessLayer.Interfaces;

public interface IPatientAction
{
    List<PatientDto> GetAll();
    PatientDto? GetById(int id);
    ActionResponse<PatientDto> Create(PatientDto dto);
    ActionResponse<PatientDto> Update(int id, PatientDto dto);
    ActionResponse Delete(int id);
}
