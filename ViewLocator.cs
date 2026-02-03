using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using WorkflowManager.ViewModels;

namespace WorkflowManager;

/// <summary>
/// Given a view model, returns the corresponding view if possible.
/// </summary>
[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        var fullName = param.GetType().FullName;
        if (fullName == null)
            return null;

        // 1. Replace the namespace segment ".ViewModels." with ".Views."
        // 2. Replace the suffix "ViewModel" with "View"
        var name = fullName
            .Replace(".ViewModels.", ".Views.", StringComparison.Ordinal)
            .Replace("ViewModel", "View", StringComparison.Ordinal);

        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = $"Not Found: {name}" };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
