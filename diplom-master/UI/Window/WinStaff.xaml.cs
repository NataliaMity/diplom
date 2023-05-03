﻿using MityaginaNP.UI.Page;
using MityaginaNP.UX.Class;
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
using System.Windows.Threading;
using System.Xml.Linq;

namespace MityaginaNP.UI.Window
{
    /// <summary>
    /// Логика взаимодействия для WinStaff.xaml
    /// </summary>
    public partial class WinStaff 
    {
        bool hidden;

        public WinStaff()
        {
            InitializeComponent();
            ProjectFrame.Navigate(new PageProjects());
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (hidden)
            {
                sideBar.Width = 200;
                hidden = false;
                txtDepPost.Visibility = Visibility.Visible;
                txtName.Visibility = Visibility.Visible;
            }
            else
            {
                sideBar.Width = 60;
                hidden = true;
                txtDepPost.Visibility = Visibility.Hidden;
                txtName.Visibility = Visibility.Hidden;
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            dispatcherTimer.Start();
            dispatcherTimer.Tick += new EventHandler((object c, EventArgs eventArgs) =>
            {
                ClassNotification.CheckDateNotif("User");
            });
        }
    }
}
