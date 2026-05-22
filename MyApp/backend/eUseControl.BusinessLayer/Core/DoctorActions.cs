using eUseControl.DataAccess.Context;
using eUseControl.Domain.Entities.Doctor;
using eUseControl.Domain.Models.Doctor;
using eUseControl.Domain.Models.Responses;

namespace eUseControl.BusinessLayer.Core;

public class DoctorActions
{
    public DoctorActions() { }

    internal List<DoctorDto> GetAllExecution()
    {
        using var db = new ClinicContext();
        return db.Doctors.ToList().Select(MapToDto).ToList();
    }

    internal DoctorDto? GetByIdExecution(int id)
    {
        using var db = new ClinicContext();
        var doctor = db.Doctors.FirstOrDefault(d => d.Id == id);
        return doctor is null ? null : MapToDto(doctor);
    }

    internal ActionResponse<DoctorDto> CreateExecution(DoctorDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Specialty))
            return new ActionResponse<DoctorDto>
            {
                IsSuccess = false,
                Message = "Name and Specialty are required."
            };

        using var db = new ClinicContext();

        var entity = new DoctorData
        {
            Name = dto.Name,
            Specialty = dto.Specialty,
            Rating = dto.Rating,
            Location = dto.Location
        };
        db.Doctors.Add(entity);
        db.SaveChanges();

        return new ActionResponse<DoctorDto>
        {
            IsSuccess = true,
            Message = "Doctor created.",
            Data = MapToDto(entity)
        };
    }

    internal ActionResponse<DoctorDto> UpdateExecution(int id, DoctorDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Specialty))
            return new ActionResponse<DoctorDto>
            {
                IsSuccess = false,
                Message = "Name and Specialty are required."
            };

        using var db = new ClinicContext();

        var entity = db.Doctors.FirstOrDefault(d => d.Id == id);
        if (entity is null)
            return new ActionResponse<DoctorDto>
            {
                IsSuccess = false,
                Message = "Doctor not found."
            };

        entity.Name = dto.Name;
        entity.Specialty = dto.Specialty;
        entity.Rating = dto.Rating;
        entity.Location = dto.Location;
        db.SaveChanges();

        return new ActionResponse<DoctorDto>
        {
            IsSuccess = true,
            Message = "Doctor updated.",
            Data = MapToDto(entity)
        };
    }

    internal ActionResponse DeleteExecution(int id)
    {
        using var db = new ClinicContext();

        var entity = db.Doctors.FirstOrDefault(d => d.Id == id);
        if (entity is null)
            return new ActionResponse { IsSuccess = false, Message = "Doctor not found." };

        db.Doctors.Remove(entity);
        db.SaveChanges();
        return new ActionResponse { IsSuccess = true, Message = "Doctor deleted." };
    }

    private static DoctorDto MapToDto(DoctorData d) => new()
    {
        Id = d.Id,
        Name = d.Name,
        Specialty = d.Specialty,
        Rating = d.Rating,
        Location = d.Location
    };
}
