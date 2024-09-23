using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using AutoSalonApp.Controllers;
using AutoSalonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoSalonApp.Views;

/// <summary>
/// Окно оформления заказа.
/// </summary>
public partial class OrderWindow : Window
{
    private readonly OrderWindowController _controller;
    
    /// <summary>
    /// Выбранная машина для заказа.
    /// </summary>
    public Car SelectedCar { get; set; }
    
    /// <summary>
    /// Метод оплаты.
    /// </summary>
    public PaymentType PaymentMethod { get; set; }
    
    /// <summary>
    /// Дата заказа.
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Конструктор окна заказа.
    /// </summary>
    public OrderWindow(AutoSalonContext context)
    {
        InitializeComponent();
        _controller = new OrderWindowController(context);
        PaymentComboBox.SelectionChanged += PaymentComboBox_SelectionChanged;
        CarComboBox.ItemsSource = context.Cars.Local.ToObservableCollection();
    }

    private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedCar = (Car)CarComboBox.SelectedItem;
        OrderDate = OrderDateTimePicker.Value ?? DateTime.Now;

        if (SelectedCar != null)
        {
            if (PaymentMethod == PaymentType.Credit)
            {
                if (int.TryParse(CreditMonthsTextBox.Text, out int creditMonths))
                {
                    if (!IsValidInput(CreditMonthsTextBox.Text))
                    {
                        MessageBox.Show("Пожалуйста, введите корректное количество месяцев для кредита.");
                        return;
                    }
                    if (_controller.PlaceOrder(SelectedCar, PaymentMethod, OrderDate, 1, creditMonths))
                    {
                        MessageBox.Show("Заказ успешно оформлен.");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите корректное количество месяцев для кредита.");
                    return;
                }
            }
            else
            {
                if (_controller.PlaceOrder(SelectedCar, PaymentMethod, OrderDate, 1, 0))
                {
                    MessageBox.Show("Заказ успешно оформлен.");
                }
            }

            DialogResult = true;
        }
        else
        {
            MessageBox.Show("Пожалуйста, выберите машину перед подтверждением заказа.");
        }
    }
    
    // Метод для получения ежемесячного платежа по кредиту
    private decimal CalculateMonthlyPayment(decimal carPrice, int creditMonths)
    {
        double annualInterestRate = 0.052;
        double monthlyInterestRate = annualInterestRate / 12;
        double monthlyPayment = (double)carPrice * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, 
            creditMonths)) / (Math.Pow(1 + monthlyInterestRate, creditMonths) - 1);

        return (decimal)monthlyPayment;
    }
    private void CalculatePaymentButton_Click(object sender, RoutedEventArgs e)
    {
        if (CarComboBox.SelectedItem != null)
        {
            SelectedCar = (Car)CarComboBox.SelectedItem;
    
            if (int.TryParse(CreditMonthsTextBox.Text, out int creditMonths))
            {
                decimal monthlyPayment = CalculateMonthlyPayment(SelectedCar.Price, creditMonths);
                MonthlyPaymentTextBox.Text = monthlyPayment.ToString("c");
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное количество месяцев для кредита.");
                return;
            }
        }
        else
        {
            MessageBox.Show("Пожалуйста, выберите машину.");
        }
    }
    
    private void PaymentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBoxItem selectedItem = (ComboBoxItem)PaymentComboBox.SelectedItem;
        string paymentMethod = selectedItem.Content.ToString();

        if (paymentMethod == "Кредит")
        {
            // Показать поле для ввода количества месяцев и кнопку "Рассчитать"
            CreditMonthsLabel.Visibility = Visibility.Visible;
            CreditMonthsTextBox.Visibility = Visibility.Visible;
            CalculatePaymentButton.Visibility = Visibility.Visible;
            MonthlyPaymentLabel.Visibility = Visibility.Visible;
            MonthlyPaymentTextBox.Visibility = Visibility.Visible;
            
            PaymentMethod = PaymentType.Credit;
        }
        else
        {
            // Скрыть поле для ввода количества месяцев и кнопку "Рассчитать"
            CreditMonthsLabel.Visibility = Visibility.Collapsed;
            CreditMonthsTextBox.Visibility = Visibility.Collapsed;
            CalculatePaymentButton.Visibility = Visibility.Collapsed;
            MonthlyPaymentLabel.Visibility = Visibility.Collapsed;
            MonthlyPaymentTextBox.Visibility = Visibility.Collapsed;
            
            PaymentMethod = PaymentType.Cash;
        }
    }
    
    private bool IsValidInput(string input)
    {
        Regex regex = new Regex("^[0-9]+$");
        return regex.IsMatch(input);
    }
}

