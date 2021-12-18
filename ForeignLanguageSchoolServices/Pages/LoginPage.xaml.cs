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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод который при нажатии кнопки проверяет введённость данных на правильность и переносит на другую страницу если введённые данные введены правильно
        /// </summary>
        /// <param name="sender">Отправляет событие</param>
        /// <param name="e">Вызывает событие</param>
        private void BtnAvtorisation_Click(object sender, RoutedEventArgs e)
        {
            var users = App.Context.Users.FirstOrDefault(p => p.Login == TxtLogin.Text && p.Password == PwdPassword.Password);
            if (users != null)
            {
                App.Users = users;
                NavigationService.Navigate(new ServicePage());
            }
            else
            {
                MessageBox.Show("Не верный ввод логина или пароля!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
