using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            if (App.Users.RoleID == 1)
            {
                BtnAddService.Visibility = Visibility.Visible;
            }
            else
            {
                BtnAddService.Visibility = Visibility.Collapsed;
            }
            CmbSortBy.SelectedIndex = 0;
            CmbDiscount.SelectedIndex = 0;
            UpdateServices();
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddService());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentService = (sender as Button).DataContext as Entities.Service;
            NavigationService.Navigate(new AddService(currentService));
        }

        private void BtnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            var removeService = (sender as Button).DataContext as Entities.Service;
            if (MessageBox.Show($"Вы уверены, что хотите удалить услугу {removeService.Title}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) 
            {
                App.Context.Services.Remove(removeService);
                App.Context.SaveChanges();
                UpdateServices();
            }
        }

        private void CmbSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void UpdateServices()
        {
            var service = App.Context.Services.ToList();
            string countServiceMax = Convert.ToString(service.Count());
            //Сортировка по цене
            if (CmbSortBy.SelectedIndex == 0)
            {
                service = service.OrderBy(p => p.CostWithDiscount).ToList();
            }
            else
            {
                service = service.OrderByDescending(p => p.CostWithDiscount).ToList();
            }
            //Фильтрация по размеру скидки
            if (CmbDiscount.SelectedIndex == 1)
                service = service.Where(p => p.Discount >= 0 && p.Discount < 0.05).ToList();
            else if (CmbDiscount.SelectedIndex == 2)
                service = service.Where(p => p.Discount >= 0.05 && p.Discount < 0.15).ToList();
            else if (CmbDiscount.SelectedIndex == 3)
                service = service.Where(p => p.Discount >= 0.15 && p.Discount < 0.30).ToList();
            else if (CmbDiscount.SelectedIndex == 4)
                service = service.Where(p => p.Discount >= 0.30 && p.Discount < 0.70).ToList();
            else if (CmbDiscount.SelectedIndex == 5)
                service = service.Where(p => p.Discount >= 0.70 && p.Discount <= 1).ToList();
            //Поиск по названию
            service = service.Where(p => p.Title.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();


            string countService = Convert.ToString(service.Count());
            TxtCount.Text = $"{countService} из {countServiceMax}";

            LstServices.ItemsSource = service;
        }

        private void CmbDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }

        private void TxtCount_TextInput(object sender, TextCompositionEventArgs e)
        {
            UpdateServices();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void BtnAddClientService_Click(object sender, RoutedEventArgs e)
        {
            var serviceClient = (sender as Button).DataContext as Entities.Service;
            NavigationService.Navigate(new AddClientServicePage(serviceClient));
        }
    }
}
