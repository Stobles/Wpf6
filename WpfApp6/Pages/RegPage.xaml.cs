using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

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

        public int nerrors = 0;
        public int nums = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Page1());
        }

        MySqlConnection con = new MySqlConnection("server = 127.0.0.1; user id = root; database = auth");

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(LoginBox.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            con.Open();

            if (RegCheck(nerrors) > 3 & isValid(LoginBox.Text) & isEmailMatch(LoginBox.Text) & PasswordBox.Password == PasswordBoxRepeat.Password) {
                Registration(LoginBox.Text, PasswordBox.Password);
                NavigationService?.Navigate(new Page1());
            }
            else {
                MessageBox.Show("Пароль или почта не соотвествует");
            }
        }

        private void Registration(string login, string password) {
            MySqlCommand command = new MySqlCommand("INSERT INTO `users`(`username`, `password`) VALUES ("+ "'" + login + "'" + "," + "'" + password + "'" + ")", con);
            command.ExecuteNonQuery();
            con.Close();
        }

        private int RegCheck(int count) {

            if (PasswordBox.Password.Length >= 6)
            {
                count++;
            }

            for (int i = 0; i < PasswordBox.Password.Length; i++)
            {
                if (!char.IsLower(PasswordBox.Password[i]))
                {
                    count++;
                    break;
                }
            }

            for (int i = 0; i < PasswordBox.Password.Length; i++)
            {
                if (PasswordBox.Password[i] >= '0' && PasswordBox.Password[i] <= '9')
                {
                    count++;
                    break;
                }
            }

            for (int i = 0; i < PasswordBox.Password.Length; i++)
            {
                if (PasswordBox.Password[i] == '@' || PasswordBox.Password[i] == '!' || PasswordBox.Password[i] == '%' || PasswordBox.Password[i] == '^' || PasswordBox.Password[i] == '$' || PasswordBox.Password[i] == '#')
                {
                    count++;
                    break;
                }
            }

            return count;
        }

        bool isValid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        bool isEmailMatch(string email) {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users`", con);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetString(0) == email)
                    {
                        return false;
                    }
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
                return false;
            }

            reader.Close();

            return true;
        }
    }
}
