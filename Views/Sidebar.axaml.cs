using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace WorkflowManager.Views;

public partial class Sidebar : UserControl
{
    public static readonly StyledProperty<bool> IsStartupAppProperty =
        AvaloniaProperty.Register<Sidebar, bool>(nameof(IsStartupApp));

    public bool IsStartupApp
    {
        get => GetValue(IsStartupAppProperty);
        set => SetValue(IsStartupAppProperty, value);
    }
    
    public Sidebar()
    {
        InitializeComponent();
    }
}