using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class PageEmployee : Page
    {
        private bool isDirty = true;
        public PageEmployee()
        {
            InitializeComponent();

            con.Open();

            LoadData();

            FillComboBox();
        }

        MySqlConnection con = new MySqlConnection("server = 127.0.0.1; user id = root; database = жд_вокзал");

        private void LoadData()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM таблица_2", con);
            DataTable dt = new DataTable();
            MySqlDataAdapter sdr = new MySqlDataAdapter(command);
            sdr.Fill(dt);
            DataGridEmployee.ItemsSource = dt.DefaultView;
        }

        private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Отмена");
            isDirty = true;
        }

        private void EditCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }
        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddPage());
        }

        private void FillComboBox() {
            MySqlCommand command = new MySqlCommand("SELECT `тип` FROM `titles`", con);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ComboBoxTitle.Items.Add(reader[0].ToString());
            }
            reader.Close();
        }

        private void TextBoxSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonFindSurname.IsEnabled = true;
            ButtonFindTitle.IsEnabled = false;
            ComboBoxTitle.SelectedIndex = -1;
        }

        private void ButtonFindSurname_Click(object sender, RoutedEventArgs e)
        {
            string surname = TextBoxSurname.Text;

            MySqlCommand command = new MySqlCommand("SELECT * FROM таблица_2 WHERE `тип` ="+ "'" + surname + "'", con);
            DataTable dt = new DataTable();

            MySqlDataAdapter sdr = new MySqlDataAdapter(command);

            sdr.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataGridEmployee.ItemsSource = dt.DefaultView;

                ButtonFindSurname.IsEnabled = true;
                ButtonFindTitle.IsEnabled = false;
            }
            else {
                MessageBox.Show("'" + surname + "'" + " тип не найден");
            }
        }
        private void ComboBoxTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonFindTitle.IsEnabled = true;
            ButtonFindSurname.IsEnabled = false;
            TextBoxSurname.Text = "";
        }
        private void ButtonFindTitle_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = (string)ComboBoxTitle.SelectedItem;

            MySqlCommand command = new MySqlCommand("SELECT * FROM таблица_2 WHERE `тип` =" + "'" + selectedItem + "'", con);
            DataTable dt = new DataTable();

            MySqlDataAdapter sdr = new MySqlDataAdapter(command);

            sdr.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataGridEmployee.ItemsSource = dt.DefaultView;

                ButtonFindSurname.IsEnabled = true;
                ButtonFindTitle.IsEnabled = false;

            }
            else
            {
                MessageBox.Show("'" + selectedItem + "'" + " тип не найден");
            }
        }

        private void RefreshCommandBinding_Executed(object sender,
        ExecutedRoutedEventArgs e)
        {
            LoadData();
            DataGridEmployee.IsReadOnly = false;
            isDirty = false;
        }
    }
}
