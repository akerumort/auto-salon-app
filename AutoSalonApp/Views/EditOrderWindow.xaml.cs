using AutoSalonApp.Controllers;
using AutoSalonApp.Models;
using System.Windows;
using System.Windows.Controls;

namespace AutoSalonApp.Views;

/// <summary>
/// Окно редактирования заказа.
/// </summary>
public partial class EditOrderWindow : Window
{
    private readonly EditOrderWindowController _controller;
    private Order _selectedOrder;
    
    /// <summary>
    /// Событие, которое происходит при сохранении заказа.
    /// </summary>
    public event EventHandler OrderSaved;

    /// <summary>
    /// Конструктор окна редактирования заказа.
    /// </summary>
    /// <param name="selectedOrder">Выбранный заказ для редактирования.</param>
    /// <param name="context">Контекст базы данных.</param>
    public EditOrderWindow(Order selectedOrder, AutoSalonContext context)
    {
        InitializeComponent();
        _selectedOrder = selectedOrder;
        _controller = new EditOrderWindowController(context);
        DataContext = _selectedOrder;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        DateTime selectedDateTime = OrderDateTimePicker.Value ?? DateTime.Now;
        ComboBoxItem selectedItem = (ComboBoxItem)PaymentComboBox.SelectedItem;

        if (selectedItem != null)
        {
            // Получаем строку с типом оплаты
            string paymentTypeString = selectedItem.Content.ToString();

            try
            {
                // Преобразуем строку в соответствующий PaymentType enum
                PaymentType paymentType = ParsePaymentType(paymentTypeString);
            
                _selectedOrder.PaymentType = paymentType;
                _selectedOrder.OrderDate = selectedDateTime;
            
                if (paymentType == PaymentType.Credit)
                {
                    if (!int.TryParse(CreditMonthsTextBox.Text, out int creditMonths) || creditMonths <= 0)
                    {
                        MessageBox.Show("Срок кредита должен быть положительным числом.");
                        return;
                    }
                    else
                    {
                        _selectedOrder.CreditMonths = creditMonths;
                    }
                }

                if (paymentType == PaymentType.Cash)
                {
                    _selectedOrder.CreditMonths = 0;
                }
            
                if (_controller.SaveChanges(_selectedOrder))
                {
                    MessageBox.Show("Изменения успешно сохранены.");
                    OrderSaved?.Invoke(this, EventArgs.Empty);
                    Close();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        else
        {
            MessageBox.Show("Выберите тип оплаты.");
        }
    }

    private void PaymentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBoxItem selectedItem = (ComboBoxItem)PaymentComboBox.SelectedItem;

        if (selectedItem != null)
        {
            string paymentTypeString = selectedItem.Content.ToString();

            try
            {
                PaymentType paymentType = ParsePaymentType(paymentTypeString);

                if (paymentType == PaymentType.Credit)
                {
                    // Отображаем дополнительные поля для кредита
                    CreditMonthsLabel.Visibility = Visibility.Visible;
                    CreditMonthsTextBox.Visibility = Visibility.Visible;
                }
                else
                {
                    // Скрываем дополнительные поля для кредита
                    CreditMonthsLabel.Visibility = Visibility.Collapsed;
                    CreditMonthsTextBox.Visibility = Visibility.Collapsed;
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        else
        {
            MessageBox.Show("Выберите тип оплаты.");
        }
    }
    
    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот заказ?",
            "Подтверждение удаления", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            try
            {
                _controller.DeleteOrder(_selectedOrder);
                MessageBox.Show("Заказ успешно удален.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении заказа: {ex.Message}");
            }
        }
    }
    
    /// <summary>
    /// Преобразует строку в тип оплаты.
    /// </summary>
    /// <param name="paymentTypeString">Строка с типом оплаты.</param>
    /// <returns>Тип оплаты.</returns>
    private PaymentType ParsePaymentType(string paymentTypeString)
    {
        switch (paymentTypeString)
        {
            case "Кредит":
                return PaymentType.Credit;
            case "Наличные":
                return PaymentType.Cash;
            default:
                throw new ArgumentException("Некорректный тип оплаты.", nameof(paymentTypeString));
        }
    }
}