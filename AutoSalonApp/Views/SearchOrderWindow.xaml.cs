using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutoSalonApp.Controllers;
using AutoSalonApp.Models;

namespace AutoSalonApp.Views;

/// <summary>
/// Окно поиска заказов.
/// </summary>
public partial class SearchOrderWindow : Window
{
    private readonly SearchOrderWindowController _controller;
    private readonly MainWindow _mainWindow;

    
    /// <summary>
    /// Событие завершения поиска.
    /// </summary>
    public event EventHandler<IEnumerable<Order>> SearchCompleted;

    /// <summary>
    /// Конструктор окна поиска заказов.
    /// </summary>
    public SearchOrderWindow(MainWindow mainWindow)
    {
        InitializeComponent();
        _controller = new SearchOrderWindowController();
        _mainWindow = mainWindow;
    }
    
    /// <summary>
    /// Обработчик нажатия кнопки поиска.
    /// </summary>
    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        // Получаем выбранный столбец для поиска
        if (ColumnComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string selectedColumn = selectedItem.Content.ToString();
            string searchText = (Application.Current.MainWindow as MainWindow)?.SearchTextBox.Text;

            if (searchText != null)
            {
                IEnumerable<Order> searchResults = _controller.SearchOrders(selectedColumn, searchText);
                SearchCompleted?.Invoke(this, searchResults);
                Close();
            }
            else
            {
                MessageBox.Show("Не удалось получить текст для поиска.");
            }
        }
    }
    
    /// <summary>
    /// Обработчик нажатия кнопки сброса.
    /// </summary>
    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        ResetSearch();
    }

    /// <summary>
    /// Сброс поиска.
    /// </summary>
    private void ResetSearch()
    {
        ColumnComboBox.SelectedItem = null;
        _mainWindow.RefreshOrdersDataGrid();
    }
}