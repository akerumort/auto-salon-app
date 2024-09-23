using System;
using System.Globalization;
using System.Windows.Data;
using AutoSalonApp.Models;

namespace AutoSalonApp.Converters;

/// <summary>
/// Конвертер для отображения типа оплаты на русском языке.
/// </summary>
public class PaymentTypeConverter : IValueConverter
{
    
    /// <summary>
    /// Метод конвертации значения типа оплаты в строковое представление на русском языке.
    /// </summary>
    /// <param name="value">Значение типа оплаты.</param>
    /// <param name="targetType">Целевой тип.</param>
    /// <param name="parameter">Параметр.</param>
    /// <param name="culture">Культура.</param>
    /// <returns>Строковое представление типа оплаты на русском языке.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || !(value is PaymentType))
            return null;

        PaymentType paymentType = (PaymentType)value;
        switch (paymentType)
        {
            case PaymentType.Credit:
                return "Кредит";
            case PaymentType.Cash:
                return "Наличные";
            default:
                return paymentType.ToString();
        }
    }

    /// <summary>
    /// Метод обратного преобразования.
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}