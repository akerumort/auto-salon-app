using System.Windows;
using AutoSalonApp.Models;

namespace AutoSalonApp.Controllers;

/// <summary>
/// Класс-контроллер для окна оформления заказа.
/// </summary>
public class OrderWindowController
{
    private readonly AutoSalonContext _context;

    /// <summary>
    /// Конструктор класса <see cref="OrderWindowController"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public OrderWindowController(AutoSalonContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Метод для оформления заказа.
    /// </summary>
    /// <param name="selectedCar">Выбранная машина.</param>
    /// <param name="paymentMethod">Способ оплаты.</param>
    /// <param name="orderDate">Дата заказа.</param>
    /// <param name="quantity">Количество заказанных машин.</param>
    /// <param name="creditMonths">Количество месяцев кредита.</param>
    /// <returns>True, если заказ был успешно оформлен, иначе false.</returns>
    public bool PlaceOrder(Car selectedCar, PaymentType paymentMethod, DateTime orderDate, int quantity, int creditMonths)
    {
        if (selectedCar.IsSold || selectedCar.Quantity == 0)
        {
            MessageBox.Show("Нельзя оформить заказ на эту машину, так как все экземпляры уже проданы.");
            return false;
        }

        // Проверка года выпуска машины
        int currentYear = DateTime.Now.Year;
        if (selectedCar.Year < 1900 || selectedCar.Year > currentYear)
        {
            MessageBox.Show($"Некорректный год выпуска машины: {selectedCar.Year}. Выберите машину " +
                            $"с годом выпуска от 1900 до {currentYear} года.");
            return false;
        }

        // Проверка даты заказа
        if (orderDate.Year < selectedCar.Year)
        {
            MessageBox.Show($"Некорректная дата заказа. Дата заказа не может быть раньше" +
                            $" года выпуска машины: {selectedCar.Year}.");
            return false;
        }

        // Уменьшаем количество машин на количество заказанных
        selectedCar.Quantity -= quantity;

        if (selectedCar.Quantity == 0)
        {
            selectedCar.IsSold = true;
        }
        _context.SaveChanges();

        var order = new Order
        {
            CarId = selectedCar.CarId,
            OrderDate = orderDate,
            PaymentType = paymentMethod,
            CreditMonths = creditMonths
        };
        _context.Orders.Add(order);
        _context.SaveChanges();

        return true;
    }
}