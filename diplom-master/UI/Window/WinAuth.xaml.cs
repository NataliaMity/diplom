﻿using MityaginaNP.UX.Class;
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
        private int roleId;
        public WinAuth()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            ClassAuthorization.AuthorizationUser(textLogin.Text, textPass.Password, out roleId);
            if(roleId == 1)
            {
                WinGIP winGIP = new WinGIP(App.DataBase.Users.Where(p => p.Login == textLogin.Text).First());
                winGIP.Show();
                this.Close();
            }
            else if (roleId == 2)
            {
                WinChief winChief = new WinChief(App.DataBase.Users.Where(p => p.Login == textLogin.Text).First());
                winChief.Show();
                this.Close();
            }
            else if (roleId == 3)
            {
                WinStaff winStaff = new WinStaff(App.DataBase.Users.Where(p => p.Login == textLogin.Text).First());
                winStaff.Show();
                this.Close();
            }
            else
                MessageBox.Show("Неверные данные для входа!");
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            textLogin.Text = "";
            textPass.Password = "";
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (textPass.Password.Length != 0)
                pass.Visibility = Visibility.Hidden;
            else
                pass.Visibility = Visibility.Visible;
        }
    }
}
