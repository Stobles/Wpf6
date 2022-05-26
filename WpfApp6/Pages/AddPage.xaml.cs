using MySql.Data.MySqlClient;
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
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public AddPage()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("server = 127.0.0.1; user id = root; database = жд_вокзал");
        int res;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(isInt(IdBox.Text));
            if (isInt(IdBox.Text) & !isInt(TypeBox.Text) & isInt(FreeBox.Text)){
                MySqlCommand command = new MySqlCommand("INSERT INTO `таблица_2`(`№_поезда`, `Тип`, `Кол-во свободных мест в мягких`) VALUES (" + "'" + IdBox.Text + "'" + "," + "'" + TypeBox.Text + "'" + "," + "'" + FreeBox.Text + "'" + ")", con);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

                NavigationService?.Navigate(new PageEmployee());
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new PageEmployee());
        }

        private bool isInt(string str)
        {
            return Int32.TryParse(str, out res);
        }
    }
}
