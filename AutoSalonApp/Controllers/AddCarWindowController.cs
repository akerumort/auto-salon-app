using System.Windows;
using AutoSalonApp.Models;

namespace AutoSalonApp.Controllers;

/// <summary>
/// Класс-контроллер для окна добавления автомобиля.
/// </summary>
public class AddCarWindowController
{
    private readonly AutoSalonContext _context;

    /// <summary>
    /// Конструктор класса <see cref="AddCarWindowController"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public AddCarWindowController(AutoSalonContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Метод для добавления нового автомобиля.
    /// </summary>
    /// <param name="brand">Бренд автомобиля.</param>
    /// <param name="model">Модель автомобиля.</param>
    /// <param name="year">Год выпуска автомобиля.</param>
    /// <param name="price">Цена автомобиля.</param>
    /// <param name="quantity">Количество автомобилей.</param>
    public bool AddCar(string brand, string model, int year, decimal price, int quantity)
    {
    // Проверка наличия пустых значений
    if (string.IsNullOrWhiteSpace(brand.Trim()) || string.IsNullOrWhiteSpace(model.Trim()))
    {
        MessageBox.Show("Бренд и модель машины не могут быть пустыми!");
        return false;
    }

    // Проверка наличия специальных символов в бренде или модели
    if (!brand.Trim().All(c => char.IsLetterOrDigit(c) || c == ' ') || !model.Trim().All(c => char.IsLetterOrDigit(c) || c == ' '))
    {
        MessageBox.Show("Некорректный формат бренда или модели машины!");
        return false;
    }

    // Проверка корректности ввода года
    int currentYear = DateTime.Now.Year;
    if (year < 1900 || year > currentYear)
    {
        MessageBox.Show("Некорректный год выпуска машины! Введите значение от 1900 до текущего года!");
        return false;
    }

    // Проверка корректности ввода цены и количества
    if (price <= 0 || quantity <= 0)
    {
        MessageBox.Show("Цена и количество машин должны быть больше нуля!");
        return false;
    }

    // Проверка корректности ввода цены и количества (только цифры)
    if (!decimal.TryParse(price.ToString(), out _) || !int.TryParse(quantity.ToString(), out _))
    {
        MessageBox.Show("Цена и количество машин должны содержать только цифры!");
        return false;
    }

    // Проверка наличия существующего автомобиля в базе данных
    var existingCar = _context.Cars.FirstOrDefault(c => c.Brand == brand && c.Model == model && c.Year == year);
    if (existingCar != null)
    {
        existingCar.Quantity += quantity;
        existingCar.IsSold = false; 
    }
    else
    {
        // Добавление нового автомобиля в базу данных
        var car = new Car
        {
            Brand = brand,
            Model = model,
            Year = year,
            Price = price,
            IsSold = false,
            Quantity = quantity
        };
        
        _context.Cars.Add(car);
    }
    
    _context.SaveChanges();
    return true; 
    }
}