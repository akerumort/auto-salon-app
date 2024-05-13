using System.ComponentModel.DataAnnotations;

namespace AutoSalonApp.Models;

/// <summary>
/// Модель заказа автомобиля.
/// </summary>
public class Order
{
    /// <summary>
    /// Уникальный идентификатор заказа.
    /// </summary>
    [Key]
    public int OrderId { get; set; }
    
    /// <summary>
    /// Идентификатор автомобиля.
    /// </summary>
    public int CarId { get; set; }
    
    /// <summary>
    /// Объект автомобиля.
    /// </summary>
    public Car Car { get; set; }
    
    /// <summary>
    /// Дата заказа.
    /// </summary>
    public DateTime OrderDate { get; set; }
    
    /// <summary>
    /// Тип оплаты.
    /// </summary>
    public PaymentType PaymentType { get; set; }
    
    /// <summary>
    /// Количество месяцев кредита.
    /// </summary>
    public int? CreditMonths { get; set; }
    
    /// <summary>
    /// Свойство, представляющее марку и модель автомобиля.
    /// </summary>
    public string BrandAndModel => $"{Car.Brand} {Car.Model}";
    
    /// <summary>
    /// Количество автомобилей.
    /// </summary>
    public int Quantity { get; set; }
}