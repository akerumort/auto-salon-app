using System.Text.RegularExpressions;
using System.Windows;
using AutoSalonApp.Controllers;
using AutoSalonApp.Models;

namespace AutoSalonApp.Views;

/// <summary>
/// Окно добавления новой машины.
/// </summary>
public partial class AddCarWindow : Window
{
    private readonly AddCarWindowController _controller;

    /// <summary>
    /// Конструктор окна добавления новой машины.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public AddCarWindow(AutoSalonContext context)
    {
        InitializeComponent();
        _controller = new AddCarWindowController(context);
    }

    private void AddCarButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(QuantityTextBox.Text, out int quantity))
        {
            // Проверка на недопустимые символы в бренде или модели
            if (!IsValidInput(BrandTextBox.Text) || !IsValidInput(ModelTextBox.Text))
            {
                MessageBox.Show("Бренд и модель машины могут содержать только буквы, цифры и пробелы!");
                return;
            }

            if (int.TryParse(YearTextBox.Text, out int year) && decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                if (_controller.AddCar(BrandTextBox.Text, ModelTextBox.Text, year, price, quantity))
                {
                    MessageBox.Show($"Машина {BrandTextBox.Text} {ModelTextBox.Text} успешно добавлена.");
                    Close();
                }
            }
        }
        else
        {
            MessageBox.Show("Некорректное значение для количества машин!");
        }
    }
    
    private bool IsValidInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input[0] == ' ')
        {
            return false;
        }

        // Убираем пробел в начале строки, если он есть
        if (input[0] == ' ')
        {
            input = input.Substring(1);
        }

        // Регулярное выражение для проверки наличия только букв, цифр и пробелов
        Regex regex = new Regex(@"^[a-zA-Z0-9][a-zA-Z0-9\s]*$");
        return regex.IsMatch(input);
    }
}