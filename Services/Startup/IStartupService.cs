namespace WorkflowManager.Services.Startup;

public interface IStartupService
{
    /// <summary>
    /// Whether the startup function is enabled.
    /// </summary>
    /// <returns>The status.</returns>
    bool IsEnabled();
    
    /// <summary>
    /// Enables the startup functionally.
    /// </summary>
    void Enable();
    
    /// <summary>
    /// Disables the startup functionality.
    /// </summary>
    void Disable();
}