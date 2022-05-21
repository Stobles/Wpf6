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

namespace WpfApp4.Pages
{
    /// <summary>
    /// Логика взаимодействия для CustomMenu.xaml
    /// </summary>
    public partial class CustomMenu : Page
    {
        public CustomMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Page1());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
