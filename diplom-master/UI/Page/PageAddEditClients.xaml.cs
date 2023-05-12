using MityaginaNP.UX.Class;
using MityaginaNP.UX.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MityaginaNP.UI.Page
{
    /// <summary>
    /// Логика взаимодействия для PageAddEditClients.xaml
    /// </summary>
    public partial class PageAddEditClients
    {
        private Client _client = new Client();
        private int _clientId = 0;
        public PageAddEditClients(Client curClient)
        {
            InitializeComponent();
            if(curClient != null)
            {
                _client = curClient;
                _clientId = curClient.ClientID;
            }
            DataContext = _client;
        }

        private void btnSaveClient_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (txtDirectorClient.Text.Length == 0)
                errors.AppendLine("Необходимо указать руководителя организации");
            if (txtDirectorPostClient.Text.Length == 0)
                errors.AppendLine("Необходимо указать должность руководителя");
            if (txtEmailClient.Text.Length == 0)
                errors.AppendLine("Необходимо указать электронную почту заказчика");
            if (txtNameClient.Text.Length == 0)
                errors.AppendLine("Необходимо указать название организации");
            if (txtINN.Text.Length != 10 || txtINN.Text.Any(Char.IsLetter))
                errors.AppendLine("ИНН должен быть составлен из 10 цифр");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_clientId == 0)
            {
                try
                {
                    if(errors.Length == 0)
                    {
                        App.DataBase.Clients.Add(_client);
                        App.DataBase.SaveChanges();
                        MessageBox.Show("Успешно сохранено!");
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    App.DataBase.Clients.Remove(_client);
                }
            }
            else
            {
                try
                {
                    if (errors.Length == 0)
                    {
                        App.DataBase.SaveChanges();
                        MessageBox.Show("Успешно сохранено!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    App.DataBase.Clients.Remove(_client);
                }
            }
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.GoBack();
        }
    }
}
