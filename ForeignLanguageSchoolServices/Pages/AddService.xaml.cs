using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ForeignLanguageSchoolServices.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddService.xaml
    /// </summary>
    public partial class AddService : Page
    {
        private byte[] imageService;
        private Entities.Service currentService = null;

        public AddService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор в котором передаются данные в элементы
        /// </summary>
        /// <param name="service"></param>
        public AddService(Entities.Service service)
        {
            InitializeComponent();
            currentService = service;
            Title = "Редактировать услугу";
            TxtName.Text = currentService.Title;
            TxtCost.Text = currentService.Cost.ToString("N2");
            TxtTime.Text = (currentService.DurationInSeconds / 60).ToString();
            TxtDescription.Text = currentService.Description;
            if (currentService.Discount > 0)
            {
                TxtDiscount.Text = (currentService.Discount.Value * 100).ToString();
            }
            else if (currentService.MainImagePath != null)
            {
                ImgService.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(currentService.MainImagePath);
            }

        }
        /// <summary>
        /// Метод который при нажатии на кнопку добавляет изображение
        /// </summary>
        /// <param name="sender">Вызывает событие</param>
        /// <param name="e">Отправляет событие</param>
        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                imageService = File.ReadAllBytes(ofd.FileName);
                ImgService.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(imageService);
            }
        }


        
        /// <summary>
        /// Метод который при нажатии на кнопку проверяет правильность введённых данных и записывает в бд
        /// </summary>
        /// <param name="sender">Вызывает событие</param>
        /// <param name="e">Отправляет событие</param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            decimal cost = 0;
            int durationMinutes = 0;
            int discount = 0;
            var serviceName = App.Context.Services.FirstOrDefault(p => p.Title.ToLower() == TxtName.Text.ToLower());
            try
            {
                if (string.IsNullOrWhiteSpace(TxtName.Text))
                    MessageBox.Show("Название услуги обязательно для заполнения!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (decimal.TryParse(TxtCost.Text, out cost) == false || cost <= 0)
                    MessageBox.Show("Стоимость услуги должна быть положительным числом", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (int.TryParse(TxtTime.Text, out durationMinutes) == false || durationMinutes > 240 || durationMinutes <= 0)
                    MessageBox.Show("Длительность оказание услуги должна быть (не больше, чем 4 часа)");
                else if (int.TryParse(TxtDiscount.Text, out discount) == false || discount < 0 || discount > 100)
                {
                    MessageBox.Show("Размер скидки - цело число в диапазоне от 0 до 100%", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else 
                {
                    if (currentService == null)
                    {
                        var service = new Entities.Service
                        {
                            Title = TxtName.Text,
                            Cost = decimal.Parse(TxtCost.Text),
                            DurationInSeconds = int.Parse(TxtTime.Text) * 60,
                            Description = TxtDescription.Text,
                            Discount = string.IsNullOrWhiteSpace(TxtDiscount.Text) ? 0 : float.Parse(TxtDiscount.Text) / 100,
                            MainImagePath = imageService

                        };
                        if (serviceName != null)
                        {
                            MessageBox.Show($"Такая услуга: {TxtName.Text} уже есть в базе данных!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            App.Context.Services.Add(service);
                            App.Context.SaveChanges();
                            MessageBox.Show($"Услуга: {TxtName.Text} успешна добавлена", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService.GoBack();
                        }
                    }
                    else
                    {
                        currentService.Title = TxtName.Text;
                        currentService.Cost = decimal.Parse(TxtCost.Text);
                        currentService.DurationInSeconds = int.Parse(TxtTime.Text) * 60;
                        currentService.Description = TxtDescription.Text;
                        currentService.Discount = string.IsNullOrWhiteSpace(TxtDiscount.Text) ? 0 : (float.Parse(TxtDiscount.Text) / 100);
                        currentService.MainImagePath = imageService;
                        App.Context.SaveChanges();
                        MessageBox.Show($"Услуга: {TxtName.Text} успешно редактирована", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.GoBack();
                    }
                    
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так :(", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
