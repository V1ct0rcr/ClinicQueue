using eUseControl.DataAccess.Context;
using eUseControl.Domain.Entities.Patient;
using eUseControl.Domain.Models.Patient;
using eUseControl.Domain.Models.Responses;

namespace eUseControl.BusinessLayer.Core;

public class PatientActions
{
    public PatientActions() { }

    internal List<PatientDto> GetAllExecution()
    {
        using var db = new ClinicContext();
        return db.Patients.ToList().Select(MapToDto).ToList();
    }

    internal PatientDto? GetByIdExecution(int id)
    {
        using var db = new ClinicContext();
        var patient = db.Patients.FirstOrDefault(p => p.Id == id);
        return patient is null ? null : MapToDto(patient);
    }

    internal ActionResponse<PatientDto> CreateExecution(PatientDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.Phone))
            return new ActionResponse<PatientDto>
            {
                IsSuccess = false,
                Message = "FirstName and Phone are required."
            };

        using var db = new ClinicContext();

        var entity = new PatientData
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Phone = dto.Phone,
            Email = dto.Email
        };
        db.Patients.Add(entity);
        db.SaveChanges();

        return new ActionResponse<PatientDto>
        {
            IsSuccess = true,
            Message = "Patient created.",
            Data = MapToDto(entity)
        };
    }

    internal ActionResponse<PatientDto> UpdateExecution(int id, PatientDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.Phone))
            return new ActionResponse<PatientDto>
            {
                IsSuccess = false,
                Message = "FirstName and Phone are required."
            };

        using var db = new ClinicContext();

        var entity = db.Patients.FirstOrDefault(p => p.Id == id);
        if (entity is null)
            return new ActionResponse<PatientDto>
            {
                IsSuccess = false,
                Message = "Patient not found."
            };

        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.Phone = dto.Phone;
        entity.Email = dto.Email;
        db.SaveChanges();

        return new ActionResponse<PatientDto>
        {
            IsSuccess = true,
            Message = "Patient updated.",
            Data = MapToDto(entity)
        };
    }

    internal ActionResponse DeleteExecution(int id)
    {
        using var db = new ClinicContext();

        var entity = db.Patients.FirstOrDefault(p => p.Id == id);
        if (entity is null)
            return new ActionResponse { IsSuccess = false, Message = "Patient not found." };

        db.Patients.Remove(entity);
        db.SaveChanges();
        return new ActionResponse { IsSuccess = true, Message = "Patient deleted." };
    }

    private static PatientDto MapToDto(PatientData p) => new()
    {
        Id = p.Id,
        FirstName = p.FirstName,
        LastName = p.LastName,
        Phone = p.Phone,
        Email = p.Email
    };
}
