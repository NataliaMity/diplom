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
            if (_clientId == 0)
            {
                try
                {
                    App.DataBase.Clients.Add(_client);
                    App.DataBase.SaveChanges();
                    MessageBox.Show("Ok...");
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
                    App.DataBase.SaveChanges();
                    MessageBox.Show("Ok...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    App.DataBase.Clients.Remove(_client);
                }
            }
            
        }
    }
}
