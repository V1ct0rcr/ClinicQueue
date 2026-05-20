using eUseControl.Domain.Entities.Appointment;
using eUseControl.Domain.Entities.Doctor;
using eUseControl.Domain.Entities.Patient;
using Microsoft.EntityFrameworkCore;

namespace eUseControl.DataAccess.Context;

public class ClinicContext : DbContext
{
    public DbSet<DoctorData> Doctors { get; set; } = null!;
    public DbSet<PatientData> Patients { get; set; } = null!;
    public DbSet<AppointmentData> Appointments { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(DbSession.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppointmentData>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AppointmentData>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
