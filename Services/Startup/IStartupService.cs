namespace WorkflowManager.Services.Startup;

public interface IStartupService
{
    bool IsEnabled();
    void Enable();
    void Disable();
}