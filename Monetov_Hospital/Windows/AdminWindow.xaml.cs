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
    public partial class AdminWindow : Window
    {
        Monetov_Hospital1Entities1 db = new Monetov_Hospital1Entities1();

        public AdminWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            var doctors = db.worker
                .Where(w => w.role_id == 1)
                .Select(d => new
                {
                    d.worker_ID,
                    d.login,
                    d.name,
                    d.surname,
                    d.post
                })
                .ToList();

            var registrars = db.worker
                .Where(w => w.role_id == 4)
                .Select(r => new
                {
                    r.worker_ID,
                    r.login,
                    r.name,
                    r.surname
                })
                .ToList();

            doctorGrid.ItemsSource = doctors;
            registrarGrid.ItemsSource = registrars;
        }

        private void AddDoctor_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddUserWindow("Doctor");
            window.ShowDialog();
            LoadUsers();
        }

        private void AddRegistrar_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddUserWindow("Registrar");
            window.ShowDialog();
            LoadUsers();
        }
    }
}

