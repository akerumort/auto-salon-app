using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;
using AutoSalonApp.Controllers;
using AutoSalonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoSalonApp.Views;

/// <summary>
/// Основное окно приложения.
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowController _controller;
    private readonly SearchOrderWindowController _searchOrderController;
    private readonly SearchCarWindowController _searchCarController;
    private readonly PieSeries pieSeries;

    /// <summary>
    /// Конструктор основного окна.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        _controller = new MainWindowController();
        _searchOrderController = new SearchOrderWindowController();
        _searchCarController = new SearchCarWindowController();
        
        // Создание экземпляра PieSeries
        pieSeries = new PieSeries
        {
            Title = "Проданные автомобили",
            DependentValuePath = "Value",
            IndependentValuePath = "Key"
        };
        
        SalesChart.Series.Add(pieSeries);
        UpdateSalesChart();
    }

    private void CreateDatabaseButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_controller.IsDatabaseCreated())
        {
            _controller.CreateDatabase();
            MessageBox.Show("База данных успешно создана!");
            RefreshCarsDataGrid();
        }
        else
        {
            MessageBox.Show("База данных уже существует!");
        }
    }

    private void DeleteDatabaseButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_controller.IsDatabaseCreated())
        {
            MessageBox.Show("Базы данных не существует, поэтому удаление невозможно.");
            return;
        }
        
        MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить базу данных?",
            "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            _controller.DeleteDatabase(); 
            OrdersDataGrid.ItemsSource = null;
            CarsDataGrid.ItemsSource = null;
            SalesChart.Series.Clear();
        }
    }
    
    private void AddCarButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_controller.CheckDatabaseExistence()) return;
        AddCarWindow addCarWindow = new AddCarWindow(_controller.GetContext());
        addCarWindow.ShowDialog();
        RefreshCarsDataGrid();
    }
    
    private void PlaceOrderButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_controller.CheckDatabaseExistence()) return;
        _controller.PlaceOrder();
        RefreshCarsDataGrid();
        UpdateSalesChart();
    }
    
    private void EditCarButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_controller.CheckDatabaseExistence()) return;

        // Проверяем, была ли выбрана строка в таблице
        if (CarsDataGrid.SelectedItem != null)
        {
            // Проверяем тип выбранного объекта
            if (CarsDataGrid.SelectedItem is Car selectedCar)
            {
                EditCarWindow editCarWindow = new EditCarWindow(selectedCar, _controller.GetContext());

                editCarWindow.ShowDialog();

                RefreshCarsDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите корректную машину для редактирования.");
            }
        }
        else
        {
            MessageBox.Show("Пожалуйста, выберите машину для редактирования.");
        }
    }

    private void EditOrderButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_controller.CheckDatabaseExistence()) return;

        // Проверяем, была ли выбрана строка в таблице
        if (OrdersDataGrid.SelectedItem != null)
        {
            // Проверяем тип выбранного объекта
            if (OrdersDataGrid.SelectedItem is Order selectedOrder)
            {
                EditOrderWindow editOrderWindow = new EditOrderWindow(selectedOrder, _controller.GetContext());

                // Обработчик события сохранения изменений
                editOrderWindow.OrderSaved += (o, args) =>
                {
                    RefreshOrdersDataGrid(); 
                    RefreshCarsDataGrid(); 
                };

                editOrderWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите корректный заказ для редактирования.");
            }
        }
        else
        {
            MessageBox.Show("Пожалуйста, выберите заказ для редактирования.");
        }
    }
    
    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_controller.CheckDatabaseExistence()) return;
        // Создаем экземпляр окна поиска, передавая ссылку на главное окно
        SearchOrderWindow searchOrderWindow = new SearchOrderWindow(this);
        searchOrderWindow.SearchCompleted += SearchOrderWindow_SearchCompleted; // Добавляем обработчик события
        searchOrderWindow.ShowDialog();
    }
    
    private void SearchAllButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_controller.CheckDatabaseExistence()) return;
        // Получаем текст для поиска из TextBox в MainWindow
        string searchText = SearchTextBox.Text;

        if (!string.IsNullOrEmpty(searchText))
        {
            // Вызываем метод поиска из контроллера SearchOrderWindowController
            IEnumerable<Order> searchResults = _searchOrderController.SearchAllOrders(searchText);

            // Обновляем DataGrid в MainWindow с использованием результатов поиска
            OrdersDataGrid.ItemsSource = searchResults.ToList();
        }
        else
        {
            MessageBox.Show("Не удалось получить текст для поиска.");
        }
    }
    
    private void SearchAllCarsButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_controller.CheckDatabaseExistence()) return;
        // Получаем текст для поиска из TextBox в MainWindow
        string searchText = SearchCarTextBox.Text;

        if (!string.IsNullOrEmpty(searchText))
        {
            // Вызываем метод поиска из контроллера SearchCarWindowController
            IEnumerable<Car> searchResults = _searchCarController.SearchAllCars(searchText);

            // Обновляем DataGrid в MainWindow с использованием результатов поиска
            CarsDataGrid.ItemsSource = searchResults.ToList();
        }
        else
        {
            MessageBox.Show("Не удалось получить текст для поиска.");
        }
    }
    
    private void SaveToJSONButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!_controller.CheckDatabaseExistence()) return;
            _controller.SaveDatabaseToJson();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении базы данных в файл JSON: {ex.Message}");
        }
    }
    
    private void SearchCarButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_controller.CheckDatabaseExistence()) return;
        // Создаем экземпляр окна расширенного поиска машин, передавая ссылку на главное окно
        SearchCarWindow searchCarWindow = new SearchCarWindow(this);
        searchCarWindow.SearchCompleted += SearchCarWindow_SearchCompleted; // Добавляем обработчик события
        searchCarWindow.ShowDialog();
    }
    
    public void RefreshOrdersDataGrid()
    {
        OrdersDataGrid.ItemsSource = _controller.GetOrders();
        UpdateSalesChart(); 
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        RefreshCarsDataGrid();
    }

    public void RefreshCarsDataGrid()
    {
        OrdersDataGrid.ItemsSource = _controller.GetOrders();
        CarsDataGrid.ItemsSource = _controller.GetCars();
    }
    
    private void SearchOrderWindow_SearchCompleted(object sender, IEnumerable<Order> searchResults)
    {
        // Обновляем DataGrid в MainWindow с использованием результатов поиска
        OrdersDataGrid.ItemsSource = searchResults.ToList();
    }

    private void SearchCarWindow_SearchCompleted(object sender, IEnumerable<Car> searchResults)
    {
        // Обновляем DataGrid в MainWindow с использованием результатов поиска
        CarsDataGrid.ItemsSource = searchResults.ToList();
    }
    private void UpdateSalesChart()
    {
        List<Order> orders = _controller.GetOrders().ToList();
        // Группируем заказы по годам оформления и считаем количество заказов в каждом году
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
