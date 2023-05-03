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

namespace MityaginaNP.UI.Window
{
    /// <summary>
    /// Логика взаимодействия для WinAuth.xaml
    /// </summary>
    public partial class WinAuth
    {
        public WinAuth()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            WinGIP winGIP = new WinGIP();
            winGIP.Show();
            this.Close();
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            textLogin.Text = "123";
            textPass.Password = "";
        }
    }
}
