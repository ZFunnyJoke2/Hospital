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

namespace Monetov_Hospital
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        Monetov_Hospital1Entities db = new Monetov_Hospital1Entities();

        public AdminWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            var registrars = db.registrar
                .Select(r => new
                {
                    r.user_id,
                    r.login,
                    r.name,
                    r.surname
                })
                .ToList();

            var doctors = db.doctor
                .Select(d => new
                {
                    d.user_id,
                    d.login,
                    d.name,
                    d.surname,
                    d.post
                })
                .ToList();

            registrarGrid.ItemsSource = registrars;
            doctorGrid.ItemsSource = doctors;
        }

        private void AddRegistrar_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddUserWindow("Registrar");
            window.ShowDialog();
            LoadUsers(); // Обновить список
        }

        private void AddDoctor_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddUserWindow("Doctor");
            window.ShowDialog();
            LoadUsers(); // Обновить список
        }
    }
}
