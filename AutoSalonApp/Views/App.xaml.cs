using System;
using System.Windows;
using AutoSalonApp.Views;

namespace AutoSalonApp;

/// <summary>
/// Класс приложения AutoSalonApp.
/// </summary>
public partial class App : Application
{
    private WelcomeWindow _welcomeWindow;
    private MainWindow _mainWindow;

    /// <summary>
    /// Метод, вызываемый при запуске приложения.
    /// </summary>
    /// <param name="e">Аргументы запуска.</param>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _welcomeWindow = new WelcomeWindow();
        _welcomeWindow.Closed += WelcomeWindow_Closed;
        _welcomeWindow.Show();
        ShutdownMode = ShutdownMode.OnExplicitShutdown;
    }

    private void WelcomeWindow_Closed(object sender, EventArgs e)
    {
        _mainWindow = new MainWindow();
        _mainWindow.Show();
        ShutdownMode = ShutdownMode.OnMainWindowClose;
    }
}
