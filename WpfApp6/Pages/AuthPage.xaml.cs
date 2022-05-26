using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp6.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("server = 127.0.0.1; user id = root; database = auth");

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginBox.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            con.Open();

            if (isMatchAcc(LoginBox.Text, PasswordBox.Password)) {
                con.Close();
                NavigationService?.Navigate(new MainPage());
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Page2());
        }

        private bool isMatchAcc(string email, string pass) {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users`", con);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetString(0) == email & reader.GetString(1) == pass) {
                        return true;
                    }
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
                return false;
            }
            reader.Close();

            con.Close();

            return false;
        }
    }
}
