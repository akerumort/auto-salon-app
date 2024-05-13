using System;
using System.Windows;
using System.Windows.Threading;

namespace AutoSalonApp.Views;

/// <summary>
/// Окно приветствия.
/// </summary>
public partial class WelcomeWindow : Window
{
    private DispatcherTimer timer;
    private int countdownSeconds = 15;

    /// <summary>
    /// Конструктор окна приветствия.
    /// </summary>
    public WelcomeWindow()
    {
        InitializeComponent();
        StartTimer();
    }

    /// <summary>
    /// Запуск таймера.
    /// </summary>
    private void StartTimer()
    {
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    /// <summary>
    /// Обработчик события тика таймера.
    /// </summary>
    private void Timer_Tick(object sender, EventArgs e)
    {
        countdownSeconds--;
        if (countdownSeconds <= 0)
        {
            timer.Stop();
            Close();
        }
        else
        {
            countdownText.Text = $"Окно автоматически закроется через {countdownSeconds} секунд.";
        }
    }

    /// <summary>
    /// Обработчик нажатия кнопки закрытия окна.
    /// </summary>
    private void CloseWindow_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}