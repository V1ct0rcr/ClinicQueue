using eUseControl.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace eUseControl.DataAccess.Context;

public class UserContext : DbContext
{
    public DbSet<UserData> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(DbSession.ConnectionString);
    }
}
