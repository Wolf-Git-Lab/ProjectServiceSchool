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
    /// Логика взаимодействия для AddClientServicePage.xaml
    /// </summary>
    public partial class AddClientServicePage : Page
    {
        private Entities.Service currentService = null;
        public AddClientServicePage()
        {
            InitializeComponent();
            CmbClient.ItemsSource = App.Context.Clients.ToList();
            TxtTimeOrigins.Text = string.Format("00:00:00");
        }

        public AddClientServicePage(Entities.Service service)
        {
            InitializeComponent();
            currentService = service;
            TxtServiceTitle.Text = currentService.Title;
            TxtTime.Text = (currentService.DurationInSeconds / 60).ToString();
            CmbClient.ItemsSource = App.Context.Clients.ToList();
            TxtTimeOrigins.Text = string.Format("00:00:00");
        }

        private void BtnAddClientService_Click(object sender, RoutedEventArgs e)
        {
            string fio = CmbClient.Text;
            var clientID = App.Context.Clients.FirstOrDefault(p => p.ClientFIO == fio);
            var serviceID = App.Context.Services.FirstOrDefault(p => p.Title == TxtServiceTitle.Text);
            try
            {
                if (DtService.DataContext == null)
                {
                    MessageBox.Show("Поле дата не должно быть пустым", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CmbClient.Text == null)
                {
                    MessageBox.Show("Поле ФИО не должно быть пустым", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var currentClient = new Entities.ClientService
                    {
                        ClientID = clientID.ID,
                        ServiceID = serviceID.ID,
                        StartTime = (DateTime)DtService.DataContext
                    };
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так :(", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
