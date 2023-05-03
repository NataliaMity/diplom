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

namespace MityaginaNP.UI.UserControl
{
    /// <summary>
    /// Логика взаимодействия для UCPageBtn.xaml
    /// </summary>
    public partial class UCPageBtn 
    {
        private int _page = 1;
        public event RoutedEventHandler ButtonClick;
        public UCPageBtn()
        {
            InitializeComponent();
        }
       
        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DataContext = e.NewValue.ToString();
            var page = DataContext;
            btnPage.Content = page;
            _page = Convert.ToInt32(page);
        }
        private void btnPage_Click(object sender, RoutedEventArgs e)
        {
            var _curpage = _page;
            ButtonClick?.Invoke(_curpage, e);
        }

    }
}
