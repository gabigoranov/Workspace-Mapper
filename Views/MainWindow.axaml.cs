using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace WorkflowManager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    public void DragWindow(object? sender, PointerPressedEventArgs e)
    {
        BeginMoveDrag(e);
    }

    private void MinimizeWindow(object? sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
    
    private void CloseWindow(object? sender, RoutedEventArgs e)
    {
        Hide();
    }

}