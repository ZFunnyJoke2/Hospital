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
using System.Xml.Linq;

namespace Monetov_Hospital.Windows
{
    public partial class AddEditPatientWindow : Window
    {
        private Monetov_Hospital1Entities1 db = new Monetov_Hospital1Entities1();
        private patient editingPatient;

        public AddEditPatientWindow(int? patientId = null)
        {
            InitializeComponent();

            if (patientId != null)
            {
                editingPatient = db.patient.FirstOrDefault(p => p.patient_ID == patientId);
                if (editingPatient != null)
                {
                    txtName.Text = editingPatient.name;
                    txtSurname.Text = editingPatient.surname;
                    txtMiddleName.Text = editingPatient.middle_name;
                    txtAge.Text = editingPatient.age.ToString();
                    cmbGender.SelectedIndex = editingPatient.gender ? 0 : 1;
                    txtTown.Text = editingPatient.town;
                    txtAddress.Text = editingPatient.address;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Введите корректный возраст.");
                return;
            }

            bool isMale = cmbGender.SelectedIndex == 0;

            if (editingPatient == null)
            {
                // Добавление
                patient newPatient = new patient
                {
                    name = txtName.Text,
                    surname = txtSurname.Text,
                    middle_name = txtMiddleName.Text,
                    age = age,
                    gender = isMale,
                    town = txtTown.Text,
                    address = txtAddress.Text,
                    role_id = 3 // Пациент
                };

                db.patient.Add(newPatient);
            }
            else
            {
                // Редактирование
                editingPatient.name = txtName.Text;
                editingPatient.surname = txtSurname.Text;
                editingPatient.middle_name = txtMiddleName.Text;
                editingPatient.age = age;
                editingPatient.gender = isMale;
                editingPatient.town = txtTown.Text;
                editingPatient.address = txtAddress.Text;
            }

            try
            {
                db.SaveChanges();
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }
    }
}
