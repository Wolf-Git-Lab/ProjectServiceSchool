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

namespace ForeignLanguageSchoolServices
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор в котором вызывается страница авторизации
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MyFrame.Navigate(new Pages.LoginPage());
        }
        
        /// <summary>
        /// Метод который при нажатии на кнопку возращает назад страницы
        /// </summary>
        /// <param name="sender">Отправляет событие</param>
        /// <param name="e">Вызывает событие</param>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                MyFrame.GoBack();
            }
        }
    }
}
