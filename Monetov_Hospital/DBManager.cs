using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Windows;

namespace Monetov_Hospital
{
    public class DBManager
    {
        public Monetov_Hospital1Entities db = new Monetov_Hospital1Entities();

        public static int Role_id;
        public static string Login;
        public static string Password;
        public static int UserId;

        // Авторизация
        public bool Auth(string login, string password)
        {
            var user = db.users.FirstOrDefault(u => u.login == login && u.password_hash == password);
            if (user != null)
            {
                var role = db.roles.FirstOrDefault(r => r.role_id == user.role_id);
                if (role == null) return false;

                // Сохраняем текущего пользователя
                CurrentUser.UserId = user.user_id;
                CurrentUser.Login = user.login;
                CurrentUser.RoleId = user.role_id;
                CurrentUser.RoleName = role.role_name;

                return true;
            }

            return false;
        }

        // Регистрация пациента (только пациента!)
        public bool RegisterPatient(string login, string password, string name, string surname,
                                    string middleName, string address, string town, bool gender, int age)
        {
            bool reg = false;

            var exists = db.users.FirstOrDefault(u => u.login == login);
            if (exists != null) return false;

            var patientRole = db.roles.FirstOrDefault(r => r.role_name == "Patient");
            if (patientRole == null) return false;

            var newUser = new users
            {
                login = login,
                password_hash = password,
                role_id = patientRole.role_id
            };

            db.users.Add(newUser);
            db.SaveChanges();

            var newPatient = new patient
            {
                user_id = newUser.user_id,
                name = name,
                surname = surname,
                middle_name = string.IsNullOrEmpty(middleName) ? null : middleName,
                address = address,
                town = town,
                gender = gender,
                age = age,
                role_id = patientRole.role_id,
                login = login,
                password = password
            };

            try
            {
                db.patient.Add(newPatient);
                db.SaveChanges();
                reg = true;
            }
            catch (DbEntityValidationException ex)
            {
                ShowValidationErrors(ex);
            }

            return reg;
        }

        public static void ShowValidationErrors(DbEntityValidationException ex)
        {
            string errorMessage = "Ошибка при сохранении:\n";
            foreach (var e in ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors))
            {
                errorMessage += $"\n• {e.PropertyName}: {e.ErrorMessage}";
            }

            MessageBox.Show(errorMessage, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

