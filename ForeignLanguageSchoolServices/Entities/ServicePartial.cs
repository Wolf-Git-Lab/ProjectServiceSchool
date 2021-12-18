using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ForeignLanguageSchoolServices.Entities
{
    public partial class Service
    {
        /// <summary>
        /// Свойство для определения текста со скидкой
        /// </summary>
        public string DiscountText
        {
            get
            {
                if (Discount == 0 || Discount == null)
                {
                    return "";
                }
                else
                {
                    return $"* скидка {Discount * 100} %";
                }
            }
        }
        /// <summary>
        /// Свойсто для вывода стоимости и перевода секунды в минуты
        /// </summary>
        public string TotalCost
        {
            get
            {
                if (Discount == 0 || Discount == null)
                {
                    return $" {Cost:N2} рублей за {DurationInSeconds / 60} минут";
                }
                else
                {
                    return $" {CostWithDiscount:N2} рублей за {DurationInSeconds / 60} минут";
                }
            }
        }
        /// <summary>
        /// Итоговая стоимость услуги
        /// </summary>
        public double CostWithDiscount
        {
            get
            {
                if (Discount == 0 || Discount == null)
                {
                    return (double)Cost;
                }
                else
                {
                    var costWithDiscount = (double)Cost * (1.00 - Discount);
                    return costWithDiscount.Value;
                }
            }
        }
        /// <summary>
        /// Свойство для видимости элементов
        /// </summary>
        public Visibility DiscountVisibility
        {
            get
            {
                if (Discount == 0 || Discount == null)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }
        /// <summary>
        /// Свойство для закраски 
        /// </summary>
        public string BackCollor
        {
            get
            {
                if (Discount == 0 || Discount == null)
                {
                    return "#FFFFE1";
                }
                else
                {
                    return "#D1FFD1";
                }
            }
        }
        /// <summary>
        /// Свойство для видимости
        /// </summary>
        public string Admin
        {
            get
            {
                if (App.Users.RoleID == 1)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
    }
}
