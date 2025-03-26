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
using System.Windows.Shapes;
using Monetov_Hospital.Windows;

namespace Monetov_Hospital
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent(); // Конструктор
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DBManager db = new DBManager();
            bool isAuth = db.Auth(login, password);

            if (isAuth)
            {
                MessageBox.Show("Вход выполнен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                switch (CurrentUser.RoleId)
                {
                    case 1:
                        new AdminWindow().Show();
                        break;
                    case 2:
                        new DoctorWindow().Show();
                        break;
                    case 3:
                        new PatientWindow().Show();
                        break;
                    case 4:
                        new RegistrarWindow().Show();
                        break;
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
