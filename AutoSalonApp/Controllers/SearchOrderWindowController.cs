using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AutoSalonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoSalonApp.Controllers;

/// <summary>
/// Класс-контроллер для окна поиска заказов.
/// </summary>
public class SearchOrderWindowController
{
    private readonly AutoSalonContext _context;

    /// <summary>
    /// Конструктор класса <see cref="SearchOrderWindowController"/>.
    /// </summary>
    public SearchOrderWindowController()
    {
        _context = new AutoSalonContext();
    }

    /// <summary>
    /// Метод для поиска заказов по заданным критериям.
    /// </summary>
    /// <param name="columnName">Имя столбца для поиска.</param>
    /// <param name="searchText">Текст для поиска.</param>
    /// <returns>Список найденных заказов.</returns>
    public IEnumerable<Order> SearchOrders(string columnName, string searchText)
    {
        // Проверяем, не пустая ли строка для поиска
        if (string.IsNullOrWhiteSpace(searchText))
        {
            // Возвращаем пустой список заказов, если строка поиска пуста
            MessageBox.Show("Не удалось получить текст для поиска.");
            return new List<Order>(); // Возвращаем пустой список
        }

        // Получаем DbSet для заказов, включая связанные машины
        IQueryable<Order> query = _context.Orders.Include(o => o.Car);

        // Выполняем поиск в выбранном столбце
        switch (columnName)
        {
            case "ID заказа":
                query = query.Where(o => o.OrderId.ToString() == searchText);
                break;
            case "ID машины":
                query = query.Where(o => o.CarId.ToString() == searchText);
                break;
            case "Машина":
                query = query.Where(o => o.Car.Brand.Contains(searchText) || o.Car.Model.Contains(searchText));
                break;
            case "Дата заказа":
                query = query.Where(o => o.OrderDate.ToString().Contains(searchText));
                break;
            case "Оплата":
                // Фильтрация на стороне клиента после загрузки данных из базы данных
                query = query.ToList().Where(o => o.PaymentType != null && o.PaymentType.ToString().Contains(searchText)).AsQueryable();
                break;
            case "Срок кредита":
                query = query.Where(o => o.CreditMonths != null && o.CreditMonths.ToString().Contains(searchText));
                break;
        }
        
        return query.ToList();
    }

    /// <summary>
    /// Метод для поиска всех заказов по заданному тексту.
    /// </summary>
    /// <param name="searchText">Текст для поиска.</param>
    /// <returns>Список найденных заказов.</returns>
    public IEnumerable<Order> SearchAllOrders(string searchText)
    {
        // Выполняем запрос на получение всех заказов
        IQueryable<Order> query = _context.Orders.Include(o => o.Car);

        // Выполняем фильтрацию локально
        IEnumerable<Order> localResults = query.AsEnumerable().Where(o =>
            o.OrderId.ToString().Contains(searchText) ||
            o.CarId.ToString().Contains(searchText) ||
            o.Car.Brand.Contains(searchText) ||
            o.Car.Model.Contains(searchText) ||
            o.OrderDate.ToString().Contains(searchText) ||
            (o.PaymentType != null && o.PaymentType.ToString().Contains(searchText)) ||
            (o.CreditMonths != null && o.CreditMonths.ToString().Contains(searchText))
        );
        
        return localResults.ToList();
    }
}
