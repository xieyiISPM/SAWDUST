﻿using SawDust.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SawDust.Views
{
    public sealed partial class DashboardTab : UserControl
    {

        public DashboardTab()
        {
            this.InitializeComponent();
            this.DataContext = new DashboardTabVM();
        }

        private void submitNewUserInfo_Click(object sender, RoutedEventArgs e)
        {
            addUserFlyout.Flyout.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewUserFlyout.Flyout.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NewUserFlyout.Flyout.Hide();
        }
    }
}
