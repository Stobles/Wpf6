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

namespace WpfApp6.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        public int errors = 0;
        public int nums = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Page1());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginBox.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            if (PasswordBox.Password.Length < 6) {
                errors++;
            }

            for (int i = 0; i < PasswordBox.Password.Length; i++) {
                if (PasswordBox.Password[i] >= '0' && PasswordBox.Password[i] <= '9')
                {
                    nums++;
                    break;
                }

                if (PasswordBox.Password[i] >= '0' && PasswordBox.Password[i] <= '9')
                {
                    nums++;
                    break;
                }
            }

            if (nums <= 0) {
                errors++;
            }

            
        }
    }
}
