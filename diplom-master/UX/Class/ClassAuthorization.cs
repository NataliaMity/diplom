using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MityaginaNP.UX.Class
{
    public class ClassAuthorization
    {
        public static int AuthorizationUser(string login, string password, out int role)
        {
            StringBuilder builder = new StringBuilder();
            var userAuthorization = App.DataBase.Users.FirstOrDefault(users => login == users.Login && users.Password == password);
            if (login.Length == 0)
            {
                builder.AppendLine("Поле логина не должно быть пустым!");
                return role = 0;
            }
            else if (password.Length == 0)
            {
                builder.AppendLine("Поле пароля не должно быть пустым!");
                return role = 0;
            }
            if (userAuthorization == null)
            {
                builder.AppendLine("Пожалуйста, проверьте данные!");
                return role = 0;
            }
            foreach(var _login in userAuthorization.Login)
            {
                login = _login.ToString();
                foreach(var _password in userAuthorization.Password)
                { 
                    password = _password.ToString();
                    return role = userAuthorization.RoleID;
                }
                return role = 0;
            }
            MessageBox.Show(builder.ToString());
            return role = 0;
        }
    }
}
