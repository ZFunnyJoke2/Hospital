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

namespace Monetov_Hospital.Windows
{
    public partial class RegistrarWindow : Window
    {
        private Monetov_Hospital1Entities1 db = new Monetov_Hospital1Entities1();

        public RegistrarWindow()
        {
            InitializeComponent();
            LoadPatients();
        }

        private void LoadPatients()
        {
            patientGrid.ItemsSource = db.patient
                .Select(p => new
                {
                    p.patient_ID,
                    p.name,
                    p.surname,
                    p.middle_name,
                    p.age,
                    p.gender,
                    p.town,
                    p.address
                }).ToList();
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditPatientWindow();
            window.ShowDialog();
            LoadPatients();
        }

        private void EditPatient_Click(object sender, RoutedEventArgs e)
        {
            if (patientGrid.SelectedItem != null)
            {
                dynamic selected = patientGrid.SelectedItem;
                int patientId = selected.patient_ID;

                var window = new AddEditPatientWindow(patientId);
                window.ShowDialog();
                LoadPatients();
            }
            else
            {
                MessageBox.Show("Выберите пациента для редактирования.");
            }
        }

        private void DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (patientGrid.SelectedItem != null)
            {
                dynamic selected = patientGrid.SelectedItem;
                int patientId = selected.patient_ID;

                var patient = db.patient.FirstOrDefault(p => p.patient_ID == patientId);
                if (patient != null)
                {
                    if (MessageBox.Show("Удалить пациента?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        db.patient.Remove(patient);
                        db.SaveChanges();
                        LoadPatients();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пациента для удаления.");
            }
        }
    }
}
