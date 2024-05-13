using System.Text.RegularExpressions;
using AutoSalonApp.Controllers;
using AutoSalonApp.Models;
using System.Windows;

namespace AutoSalonApp.Views;

/// <summary>
/// Окно редактирования информации о машине.
/// </summary>
public partial class EditCarWindow : Window
{
    private Car _selectedCar;
    private readonly EditCarWindowController _controller;

    /// <summary>
    /// Конструктор окна редактирования информации о машине.
    /// </summary>
    /// <param name="selectedCar">Выбранная машина для редактирования.</param>
    /// <param name="context">Контекст базы данных.</param>
    public EditCarWindow(Car selectedCar, AutoSalonContext context)
    {
        InitializeComponent();
        _selectedCar = selectedCar;
        _controller = new EditCarWindowController(context);
        
        BrandTextBox.Text = selectedCar.Brand;
        ModelTextBox.Text = selectedCar.Model;
        YearTextBox.Text = selectedCar.Year.ToString();
        PriceTextBox.Text = selectedCar.Price.ToString();
        QuantityTextBox.Text = selectedCar.Quantity.ToString();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        // Проверяем корректность введенных данных
        if (string.IsNullOrEmpty(BrandTextBox.Text) ||
            string.IsNullOrEmpty(ModelTextBox.Text) ||
            string.IsNullOrEmpty(YearTextBox.Text) ||
            string.IsNullOrEmpty(PriceTextBox.Text) ||
            string.IsNullOrEmpty(QuantityTextBox.Text))
        {
            MessageBox.Show("Пожалуйста, заполните все поля.");
            return;
        }
        
        if (!IsValidInput(BrandTextBox.Text) || !IsValidInput(ModelTextBox.Text))
        {
            MessageBox.Show("Бренд или модель машины содержат недопустимые символы.");
            return;
        }

        if (!int.TryParse(YearTextBox.Text, out int year) || year < 1900 || year > DateTime.Now.Year)
        {
            MessageBox.Show("Год выпуска должен быть числом от 1900 до текущего года.");
            return;
        }

        if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price <= 0)
        {
            MessageBox.Show("Цена должна быть положительным числом.");
            return;
        }

        if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity < 0)
        {
            MessageBox.Show("Количество должно быть положительным числом.");
            return;
        }
        
        _selectedCar.Brand = BrandTextBox.Text;
        _selectedCar.Model = ModelTextBox.Text;
        _selectedCar.Year = year;
        _selectedCar.Price = price;
        _selectedCar.Quantity = quantity;
        
        _controller.SaveChanges(_selectedCar);
        Close();
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

        // Проверяем, что в строке есть только допустимые символы (буквы и пробелы), без пробелов в начале строки
        Regex regex = new Regex(@"^[a-zA-Z0-9][a-zA-Z0-9\s]*$");
        return regex.IsMatch(input);
    }
    
    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту машину?", 
            "Подтверждение удаления", MessageBoxButton.YesNo);
        
        if (result == MessageBoxResult.Yes)
        {
            _controller.DeleteCar(_selectedCar);
            Close();
        }
    }
}
