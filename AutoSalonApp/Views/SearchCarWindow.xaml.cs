using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AutoSalonApp.Controllers;
using AutoSalonApp.Models;

namespace AutoSalonApp.Views;

/// <summary>
/// Окно расширенного поиска автомобилей.
/// </summary>
public partial class SearchCarWindow : Window
{
    private readonly SearchCarWindowController _controller;
    private readonly MainWindow _mainWindow;

    /// <summary>
    /// Событие завершения поиска.
    /// </summary>
    public event EventHandler<IEnumerable<Car>> SearchCompleted;

    /// <summary>
    /// Конструктор окна поиска автомобилей.
    /// </summary>
    public SearchCarWindow(MainWindow mainWindow)
    {
        InitializeComponent();
        _controller = new SearchCarWindowController();
        _mainWindow = mainWindow;
    }

    /// <summary>
    /// Обработчик нажатия кнопки поиска.
    /// </summary>
    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        if (ColumnComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string selectedColumn = selectedItem.Content.ToString();
            string searchText = (Application.Current.MainWindow as MainWindow)?.SearchCarTextBox.Text;

            if (searchText != null)
            {
                IEnumerable<Car> searchResults = _controller.SearchCars(selectedColumn, searchText);
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
        _mainWindow.RefreshCarsDataGrid();
    }
}