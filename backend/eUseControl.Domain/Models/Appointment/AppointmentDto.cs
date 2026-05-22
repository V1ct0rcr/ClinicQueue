namespace eUseControl.Domain.Models.Appointment;

public class AppointmentDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = "Pending";
}
