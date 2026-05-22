namespace eUseControl.Domain.Models.Doctor;

public class DoctorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string Location { get; set; } = string.Empty;
}
