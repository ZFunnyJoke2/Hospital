using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using BCrypt.Net;

namespace Monetov_Hospital
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

private void RegisterUser(object sender, RoutedEventArgs e)
{
    string login = txtLogin.Text.Trim();
    string password = txtPassword.Password.Trim();
    string name = txtName.Text.Trim();
    string surname = txtSurname.Text.Trim();
    string middleName = txtMiddleName.Text.Trim();
    string address = txtAddress.Text.Trim();
    string town = txtTown.Text.Trim();
            bool? isMaleChecked = radioMale.IsChecked;

            if (isMaleChecked == null)
            {
                MessageBox.Show("Пожалуйста, выберите пол.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isMale = isMaleChecked == true;
            int age;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(town) ||
                !radioMale.IsChecked.Value && !radioFemale.IsChecked.Value ||
                !int.TryParse(txtAge.Text, out age))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DBManager db = new DBManager();
            bool success = db.RegisterPatient(
            login, password, name, surname, middleName, address, town, isMale, age);

            if (success)
    {
        MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        this.Close();   
    }
    else
    {
        MessageBox.Show("Ошибка регистрации. Логин уже существует или произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
    }
}
