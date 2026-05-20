using eUseControl.Domain.Models.Doctor;
using eUseControl.Domain.Models.Responses;

namespace eUseControl.BusinessLayer.Interfaces;

public interface IDoctorAction
{
    List<DoctorDto> GetAll();
    DoctorDto? GetById(int id);
    ActionResponse<DoctorDto> Create(DoctorDto dto);
    ActionResponse<DoctorDto> Update(int id, DoctorDto dto);
    ActionResponse Delete(int id);
}
