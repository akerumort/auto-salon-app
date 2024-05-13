using System.IO;
using System.Windows;
using AutoSalonApp.Models;
using AutoSalonApp.Views;
using Microsoft.Win32;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace AutoSalonApp.Controllers;

/// <summary>
/// Класс-контроллер для главного окна приложения.
/// </summary>
public class MainWindowController
{
    private readonly AutoSalonContext _context;

    /// <summary>
    /// Конструктор класса <see cref="MainWindowController"/>.
    /// </summary>
    public MainWindowController()
    {
        _context = new AutoSalonContext();
        _context.Database.EnsureCreated();
    }

    /// <summary>
    /// Метод для создания базы данных, если она не существует.
    /// </summary>
    public void CreateDatabase()
    {
        _context.Database.EnsureCreated();
    }

    /// <summary>
    /// Метод для удаления базы данных.
    /// </summary>
    public void DeleteDatabase()
    {
        _context.Cars.RemoveRange(_context.Cars);
        _context.Orders.RemoveRange(_context.Orders);
        _context.SaveChanges();
        using (AutoSalonContext db = new AutoSalonContext())
        {
            bool isDeleted = db.Database.EnsureDeleted();
            MessageBox.Show(isDeleted ? "База данных успешно удалена!" : "Невозможно удалить базу данных, т.к. она не существует!");
        }
    }

    /// <summary>
    /// Метод для получения списка всех машин.
    /// </summary>
    /// <returns>Список машин.</returns>
    public IEnumerable<Car> GetCars()
    {
        return _context.Cars.ToList();
    }

    /// <summary>
    /// Метод для отображения окна оформления заказа.
    /// </summary>
    public void PlaceOrder()
    {
        OrderWindow orderWindow = new OrderWindow(_context);
        orderWindow.ShowDialog();
    }
    
    /// <summary>
    /// Метод для получения списка всех заказов.
    /// </summary>
    /// <returns>Список заказов.</returns>
    public IEnumerable<Order> GetOrders()
    {
        return _context.Orders.ToList();
    }
    
    /// <summary>
    /// Метод для проверки существования базы данных.
    /// </summary>
    /// <returns>True, если база данных существует, иначе false.</returns>
    public bool IsDatabaseCreated()
    {
        return _context.Database.CanConnect();
    }
    
    /// <summary>
    /// Метод для получения контекста базы данных.
    /// </summary>
    /// <returns>Контекст базы данных.</returns>
    public AutoSalonContext GetContext()
    {
        return _context;
    }

    /// <summary>
    /// Метод для сохранения базы данных в формате JSON.
    /// </summary>
    public void SaveDatabaseToJson()
    {
        var saveFileDialog = new SaveFileDialog
        {
            Title = "Сохранить JSON файл",
            DefaultExt = ".json",
            Filter = "JSON файл (*.json)|*.json|Все файлы (*.*)|*.*"
        };

        // Показываем диалоговое окно и проверяем результат
        if (saveFileDialog.ShowDialog() == true)
        {
            // Получаем путь к файлу
            string filePath = saveFileDialog.FileName;

            // Получаем все данные для сохранения
            var dataToSave = new
            {
                Cars = GetCars(),
                Orders = GetOrders()
            };

            // Сериализуем данные в JSON и записываем в файл
            string jsonData = JsonConvert.SerializeObject(dataToSave, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, jsonData);

            MessageBox.Show("Файл успешно сохранен: " + filePath);
        }
    }
    
    
}