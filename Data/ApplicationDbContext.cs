using Microsoft.EntityFrameworkCore;
using WorkflowManager.Models;
using WorkflowManager.Models.Common;
using Process = WorkflowManager.Models.Common.Process;

namespace WorkflowManager.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Workflow> Workflows { get; set; }
    public DbSet<Process> Processes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the inheritance hierarchy for processes
        modelBuilder.Entity<Process>().ToTable("Processes");
        modelBuilder.Entity<CommandProcess>();

        modelBuilder.Entity<Process>()
            .HasDiscriminator<ProcessType>("Discriminator")
            .HasValue<CommandProcess>(ProcessType.CommandProcess); 
    }
}