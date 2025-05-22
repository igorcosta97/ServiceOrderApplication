using Microsoft.EntityFrameworkCore;
using ServiceOrderApplication.Models;

namespace ServiceOrderApplication.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<ClientModel> Clients { get; set; }
}