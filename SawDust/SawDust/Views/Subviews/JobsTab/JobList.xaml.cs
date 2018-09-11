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
            try
            {
                Button b = sender as Button;
                Grid g = b.Parent as Grid;
                Grid g2 = g.Parent as Grid;
                FlyoutPresenter fp = g2.Parent as FlyoutPresenter;
                Popup f = fp.Parent as Popup;
                f.IsOpen = false;
            }
            catch { }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Job j = (Job) e.ClickedItem;
            ((JobsTabVM)this.DataContext).SelectedJob = j;
        }

        private void ListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            object o = sender;
            // place holder for double click behavior. 
            // single click selects item
            // double click should open the next view in the flow
        }
    }
}
