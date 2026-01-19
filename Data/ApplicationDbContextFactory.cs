using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorkflowManager.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        // This gets the folder where the .exe is running
        var basePath = AppContext.BaseDirectory; 
        // This points to the DB file in that same folder
        var dbPath = Path.Combine(basePath, "WorkflowManager.db");
        
        optionsBuilder.UseSqlite($"Data Source=D:\\Projects\\WorkflowManager\\WorkflowManager.db");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}