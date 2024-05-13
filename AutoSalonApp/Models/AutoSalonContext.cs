using System.Windows;
using Microsoft.EntityFrameworkCore;
namespace AutoSalonApp.Models;

/// <summary>
/// Контекст базы данных для приложения AutoSalonApp.
/// </summary>
public class AutoSalonContext : DbContext
{
    /// <summary>
    /// Коллекция автомобилей в базе данных.
    /// </summary>
    public DbSet<Car> Cars { get; set; }
    
    /// <summary>
    /// Коллекция заказов в базе данных.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Настройка подключения к базе данных.
    /// </summary>
    /// <param name="optionsBuilder">Построитель параметров подключения.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("DataSource=autosalon.db");
        }
    }
}