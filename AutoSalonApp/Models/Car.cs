using System.ComponentModel.DataAnnotations;

namespace AutoSalonApp.Models;

/// <summary>
/// Модель автомобиля.
/// </summary>
public class Car
{
    /// <summary>
    /// Уникальный идентификатор автомобиля.
    /// </summary>
    [Key]
    public int CarId { get; set; }
        
    /// <summary>
    /// Марка автомобиля.
    /// </summary>
    public string Brand { get; set; }
    
    /// <summary>
    /// Модель автомобиля.
    /// </summary>
    public string Model { get; set; }
    
    /// <summary>
    /// Год выпуска автомобиля.
    /// </summary>
    public int Year { get; set; }
    
    /// <summary>
    /// Цена автомобиля.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Флаг, указывающий, продан ли автомобиль.
    /// </summary>
    public bool IsSold { get; set; }
    
    /// <summary>
    /// Количество доступных автомобилей.
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Полная информация об автомобиле (марка, модель и идентификатор).
    /// </summary>
    public string FullCarInfo => $"{Brand} {Model} ({CarId})";

}