using Microsoft.EntityFrameworkCore;
using WebAPI.Z4_Domain.Entities;

namespace WebAPI.Z2_Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
