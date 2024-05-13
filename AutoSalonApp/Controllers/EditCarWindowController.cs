using AutoSalonApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;

namespace AutoSalonApp.Controllers;

/// <summary>
/// Класс-контроллер для окна редактирования автомобиля.
/// </summary>
public class EditCarWindowController
{
    private readonly AutoSalonContext _context;

    /// <summary>
    /// Конструктор класса <see cref="EditCarWindowController"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public EditCarWindowController(AutoSalonContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Метод для сохранения изменений в автомобиле.
    /// </summary>
    /// <param name="car">Измененный объект автомобиля.</param>
    public void SaveChanges(Car car)
    {
        try
        {
            // Проверяем, изменилось ли количество машин
            if (_context.Entry(car).Property("Quantity").IsModified)
            {
                // Если количество стало равным 0, устанавливаем флаг IsSold в true
                if (car.Quantity == 0)
                {
                    car.IsSold = true;
                }
                // Если количество стало больше 0, устанавливаем флаг IsSold в false
                else
                {
                    car.IsSold = false;
                }
            }
            _context.Entry(car).State = EntityState.Modified;
            _context.SaveChanges();
            MessageBox.Show("Изменения успешно сохранены.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
        }
    }
    
    /// <summary>
    /// Метод для удаления автомобиля.
    /// </summary>
    /// <param name="car">Удаляемый объект автомобиля.</param>
    public void DeleteCar(Car car)
    {
        try
        {
            _context.Cars.Remove(car);
            _context.SaveChanges();
            MessageBox.Show("Машина успешно удалена.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при удалении машины: {ex.Message}");
        }
    }
}
