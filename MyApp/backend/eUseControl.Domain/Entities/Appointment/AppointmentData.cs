using eUseControl.Domain.Entities.Doctor;
using eUseControl.Domain.Entities.Patient;

namespace eUseControl.Domain.Entities.Appointment;

public class AppointmentData
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = "Pending";

    public DoctorData? Doctor { get; set; }
    public PatientData? Patient { get; set; }
}
