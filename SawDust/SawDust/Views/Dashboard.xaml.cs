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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SawDust.Views
{
   
    public sealed partial class Dashboard : UserControl
    {
        

        public Dashboard()
        {
            this.InitializeComponent();
            this.DataContext = new DashboardVM();
        }

        private void submitNewUserInfo_Click(object sender, RoutedEventArgs e)
        {
            addUserFlyout.Flyout.Hide();
        }

        private void userCancel_Click(object sender, RoutedEventArgs e)
        {
            addUserFlyout.Flyout.Hide();
        }

        private void cancelConfirm_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void userCancelConfirm_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
