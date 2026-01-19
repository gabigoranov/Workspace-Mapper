using Microsoft.EntityFrameworkCore;
using WorkflowManager.Models;

namespace WorkflowManager.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Workflow> Workflows { get; set; }
    public DbSet<Process> Processes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional model configuration
    }
}