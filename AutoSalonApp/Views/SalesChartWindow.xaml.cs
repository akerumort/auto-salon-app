using System.Windows;
using AutoSalonApp.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.DataVisualization.Charting;
using AutoSalonApp.Models;

namespace AutoSalonApp.Views;

/// <summary>
/// Окно для отображения диаграммы продаж.
/// </summary>
public partial class SalesChartWindow : Window
{
    private readonly MainWindowController _controller;

    /// <summary>
    /// Конструктор окна диаграммы продаж.
    /// </summary>
    /// <param name="controller">Контроллер основного окна приложения.</param>
    public SalesChartWindow(MainWindowController controller)
    {
        InitializeComponent();
        _controller = controller;
        UpdateSalesChart();
    }

    /// <summary>
    /// Метод, который обновляет данные на диаграмме продаж.
    /// </summary>
    private void UpdateSalesChart()
    {
        List<Order> orders = _controller.GetOrders().ToList();
        var salesByYear = orders.GroupBy(order => order.OrderDate.Year)
            .Select(group => new KeyValuePair<int, int>(group.Key, group.Count()));

        SalesChart.Series.Clear();
        ColumnSeries columnSeries = new ColumnSeries
        {
            Title = "Проданные автомобили",
            ItemsSource = salesByYear,
            DependentValuePath = "Value",
            IndependentValuePath = "Key"
        };

        SalesChart.Series.Add(columnSeries);
    }
}