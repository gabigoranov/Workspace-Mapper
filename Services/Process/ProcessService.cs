using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using StudyPlatform.Data.Common;

namespace WorkflowManager.Services.Process;

public class ProcessService(IRepository repository) : IProcessService
{
    public async Task ExecuteProcessAsync(Models.Process workflowProcess)
    {
        await Task.Run(() =>
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    // -NoProfile: prevents loading user profiles for speed
                    // -Command: tells PS to execute the following string and exit
                    Arguments = $"-NoProfile -Command \"{workflowProcess.Command}\"",
                    WorkingDirectory = workflowProcess.Directory,
                    UseShellExecute = false,
                    CreateNoWindow = true, // Set to false if you want to see the popup
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(startInfo)!)
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        Debug.WriteLine($"Error: {error}");
                    }
                    else
                    {
                        Debug.WriteLine($"Output: {output}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to run PowerShell: {ex.Message}");
            }
        });
        
    }

    public async Task<Models.Process> EditProcessAsync(int id, Models.Process workflowProcess)
    {
        var process = await repository.GetByIdAsync<Models.Process>(id);
        if (process == null)
            throw new KeyNotFoundException("Workflow not found");

        process.Title = workflowProcess.Title;
        process.Directory = workflowProcess.Directory;
        process.Command = workflowProcess.Command;

        await repository.SaveChangesAsync();
        return process;
    }
}