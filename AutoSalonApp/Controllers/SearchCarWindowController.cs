using System.Windows;
using AutoSalonApp.Models;

namespace AutoSalonApp.Controllers;

/// <summary>
/// Класс-контроллер для окна поиска автомобилей.
/// </summary>
public class SearchCarWindowController
{
    private readonly AutoSalonContext _context;

    /// <summary>
    /// Конструктор класса <see cref="SearchCarWindowController"/>.
    /// </summary>
    public SearchCarWindowController()
    {
        _context = new AutoSalonContext();
    }

    /// <summary>
    /// Метод для поиска автомобилей по заданным критериям.
    /// </summary>
    /// <param name="columnName">Имя столбца для поиска.</param>
    /// <param name="searchText">Текст для поиска.</param>
    /// <returns>Список найденных автомобилей.</returns>
    public IEnumerable<Car> SearchCars(string columnName, string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            MessageBox.Show("Не удалось получить текст для поиска.");
            return new List<Car>();
        }

        IQueryable<Car> query = _context.Cars;

        switch (columnName)
        {
            case "ID":
                query = query.Where(c => c.CarId.ToString() == searchText);
                break;
            case "Бренд":
                query = query.Where(c => c.Brand.Contains(searchText));
                break;
            case "Модель":
                query = query.Where(c => c.Model.Contains(searchText));
                break;
            case "Год выпуска":
                query = query.Where(c => c.Year.ToString() == searchText);
                break;
            case "Цена":
                query = query.Where(c => c.Price.ToString() == searchText);
                break;
            case "Количество":
                query = query.Where(c => c.Quantity.ToString() == searchText);
                break;
            case "Продана":
                bool isSold = searchText.ToLower() == "да" || searchText.ToLower() == "true";
                query = query.Where(c => c.IsSold == isSold);
                break;
        }

        return query.ToList();
    }
    
    /// <summary>
    /// Метод для поиска автомобилей по всем полям.
    /// </summary>
    /// <param name="searchText">Текст для поиска.</param>
    /// <returns>Список найденных автомобилей.</returns>
    public IEnumerable<Car> SearchAllCars(string searchText)
    {
        IQueryable<Car> query = _context.Cars;

        IEnumerable<Car> localResults = query.AsEnumerable().Where(c =>
            c.CarId.ToString().Contains(searchText) ||
            c.Brand.Contains(searchText) ||
            c.Model.Contains(searchText) ||
            c.Year.ToString().Contains(searchText) ||
            c.Price.ToString().Contains(searchText) ||
            c.Quantity.ToString().Contains(searchText) ||
            c.IsSold.ToString().Contains(searchText)
        );
        
        return localResults.ToList();
    }
}