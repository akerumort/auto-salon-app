using AutoSalonApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;

namespace AutoSalonApp.Controllers;

/// <summary>
/// Класс-контроллер для окна редактирования заказа.
/// </summary>
public class EditOrderWindowController
{
    private readonly AutoSalonContext _context;

    /// <summary>
    /// Конструктор класса <see cref="EditOrderWindowController"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public EditOrderWindowController(AutoSalonContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Метод для сохранения изменений в заказе.
    /// </summary>
    /// <param name="order">Измененный объект заказа.</param>
    public bool SaveChanges(Order order)
    {
        try
        {
            // Проверка года выпуска машины
            Car car = _context.Cars.Find(order.CarId);
            int currentYear = DateTime.Now.Year;
            if (car == null || car.Year < 1900 || car.Year > currentYear)
            {
                MessageBox.Show($"Некорректные данные для заказа.");
                return false;
            }

            // Проверка даты заказа
            if (order.OrderDate.Year < car.Year)
            {
                MessageBox.Show($"Некорректные данные для заказа.");
                return false;
            }

            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            return false;
        }
    }
    
    /// <summary>
    /// Метод для удаления заказа.
    /// </summary>
    /// <param name="order">Удаляемый объект заказа.</param>
    public void DeleteOrder(Order order)
    {
        try
        {
            // Получаем информацию о машине из заказа
            Car car = _context.Cars.Find(order.CarId);

            if (car != null)
            {
                _context.Orders.Remove(order);
                car.Quantity++;

                // Если количество машин в наличии стало больше 0, устанавливаем флаг IsSold в false
                if (car.Quantity > 0)
                {
                    car.IsSold = false;
                }

                _context.SaveChanges();
            }
            else
            {
                MessageBox.Show($"Машина с ID {order.CarId} не найдена.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при удалении заказа: {ex.Message}");
        }
    }
}
