using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace AskWatson
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void ServicesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServicesListView.SelectedItem == null)
            {
                return;
            }

            switch(ServicesListView.SelectedIndex)
            {
                case 0:
                    // question and answer
                    Frame.Navigate(typeof(QuestionAnswer.MainPage));
                    break;
                default:
                    MessageDialog notAvailableDialog = new MessageDialog("This demo is not yet available");
                    await notAvailableDialog.ShowAsync();
                    break;
            }

            ServicesListView.SelectedItem = null;
        }
    }
}
