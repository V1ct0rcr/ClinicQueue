using eUseControl.DataAccess.Context;
using eUseControl.Domain.Entities.Appointment;
using eUseControl.Domain.Models.Appointment;
using eUseControl.Domain.Models.Responses;

namespace eUseControl.BusinessLayer.Core;

public class AppointmentActions
{
    public AppointmentActions() { }

    internal List<AppointmentDto> GetAllExecution()
    {
        using var db = new ClinicContext();
        return db.Appointments.ToList().Select(MapToDto).ToList();
    }

    internal AppointmentDto? GetByIdExecution(int id)
    {
        using var db = new ClinicContext();
        var item = db.Appointments.FirstOrDefault(a => a.Id == id);
        return item is null ? null : MapToDto(item);
    }

    internal ActionResponse<AppointmentDto> CreateExecution(AppointmentDto dto)
    {
        var error = Validate(dto);
        if (error is not null)
            return new ActionResponse<AppointmentDto> { IsSuccess = false, Message = error };

        using var db = new ClinicContext();

        if (!db.Doctors.Any(d => d.Id == dto.DoctorId))
            return new ActionResponse<AppointmentDto> { IsSuccess = false, Message = "Doctor not found." };
        if (!db.Patients.Any(p => p.Id == dto.PatientId))
            return new ActionResponse<AppointmentDto> { IsSuccess = false, Message = "Patient not found." };

        var entity = new AppointmentData
        {
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            AppointmentDate = dto.AppointmentDate,
            Status = string.IsNullOrWhiteSpace(dto.Status) ? "Pending" : dto.Status
        };
        db.Appointments.Add(entity);
        db.SaveChanges();

        return new ActionResponse<AppointmentDto>
        {
            IsSuccess = true,
            Message = "Appointment created.",
            Data = MapToDto(entity)
        };
    }

    internal ActionResponse<AppointmentDto> UpdateExecution(int id, AppointmentDto dto)
    {
        var error = Validate(dto);
        if (error is not null)
            return new ActionResponse<AppointmentDto> { IsSuccess = false, Message = error };

        using var db = new ClinicContext();

        if (!db.Doctors.Any(d => d.Id == dto.DoctorId))
            return new ActionResponse<AppointmentDto> { IsSuccess = false, Message = "Doctor not found." };
        if (!db.Patients.Any(p => p.Id == dto.PatientId))
            return new ActionResponse<AppointmentDto> { IsSuccess = false, Message = "Patient not found." };

        var entity = db.Appointments.FirstOrDefault(a => a.Id == id);
        if (entity is null)
            return new ActionResponse<AppointmentDto>
            {
                IsSuccess = false,
                Message = "Appointment not found."
            };

        entity.PatientId = dto.PatientId;
        entity.DoctorId = dto.DoctorId;
        entity.AppointmentDate = dto.AppointmentDate;
        entity.Status = string.IsNullOrWhiteSpace(dto.Status) ? "Pending" : dto.Status;
        db.SaveChanges();

        return new ActionResponse<AppointmentDto>
        {
            IsSuccess = true,
            Message = "Appointment updated.",
            Data = MapToDto(entity)
        };
    }

    internal ActionResponse DeleteExecution(int id)
    {
        using var db = new ClinicContext();

        var entity = db.Appointments.FirstOrDefault(a => a.Id == id);
        if (entity is null)
            return new ActionResponse { IsSuccess = false, Message = "Appointment not found." };

        db.Appointments.Remove(entity);
        db.SaveChanges();
        return new ActionResponse { IsSuccess = true, Message = "Appointment deleted." };
    }

    private static string? Validate(AppointmentDto dto)
    {
        if (dto.DoctorId <= 0 || dto.PatientId <= 0)
            return "DoctorId and PatientId must be greater than 0.";
        if (dto.AppointmentDate < DateTime.Now)
            return "AppointmentDate cannot be in the past.";
        return null;
    }

    private static AppointmentDto MapToDto(AppointmentData a) => new()
    {
        Id = a.Id,
        PatientId = a.PatientId,
        DoctorId = a.DoctorId,
        AppointmentDate = a.AppointmentDate,
        Status = a.Status
    };
}
