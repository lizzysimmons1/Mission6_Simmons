using Microsoft.EntityFrameworkCore;

namespace Mission6_Simmons.Models;

public class movieContext : DbContext
{
    public movieContext(DbContextOptions<movieContext> options) : base(options)
    {
    }
    
    public DbSet<movie> Movie { get; set; }
}