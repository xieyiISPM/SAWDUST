using SawDust.BusinessObjects;
using SawDust.ViewModel;
using SawDust.ViewModel.SubviewModel.Jobs;
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

namespace SawDust.Views.Subviews.JobsTab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class JobList : Page
    {
        public JobList()
        {
            this.InitializeComponent();
            this.DataContext = new JobsTabVM();
        }

        private void delteJob_Click(object sender, RoutedEventArgs e)
        {
            Button o = (Button)sender;
            Job j = (Job)o.DataContext;
            ((JobsTabVM)this.DataContext).RemoveJob(j);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // this.Del
            
        }

        private void addNewJob_Click(object sender, RoutedEventArgs e)
        {
            addJobButton.Flyout.Hide();
        }

        private void cancelAddJob_Click(object sender, RoutedEventArgs e)
        {
            addJobButton.Flyout.Hide();
        }
    }
}
