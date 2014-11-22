using AskWatson.Common;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace AskWatson.QuestionAnswer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            if (e.NavigationMode == NavigationMode.Back &&
                App.CurrentQuestionAnswerSearch != null)
            {
                QuestionTextBox.Text = App.CurrentQuestionAnswerSearch.question.questionText;
                AnswersListView.ItemsSource = App.CurrentQuestionAnswerSearch.question.evidencelist;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private async void AskWatsonButton_Click(object sender, RoutedEventArgs e)
        {
            AskWatsonButton.IsEnabled = false;
            SearchingStackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;

            try
            {
                ComboBoxItem selectedItem = (ComboBoxItem)CategoryComboBox.SelectedItem;

                var response = await Portable.AskWatsonService.AskWatson(
                    selectedItem.Content.ToString(),
                    QuestionTextBox.Text);


                App.CurrentQuestionAnswerSearch = response;
                AnswersListView.ItemsSource = response.question.evidencelist;
            }
            catch (Exception ex)
            {
                MessageDialog errorDialog = new MessageDialog(string.Format("Error: {0}", ex.Message));
                errorDialog.ShowAsync();
            }
            finally
            {
                SearchingStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                AskWatsonButton.IsEnabled = true;
            }
        }

        private void AnswersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AnswersListView.SelectedItem == null)
            {
                return;
            }

            App.SelectedEvidencelist = (Portable.Models.Evidencelist)AnswersListView.SelectedItem;
            Frame.Navigate(typeof(QuestionAnswer.AnswerDetails));

            AnswersListView.SelectedItem = null;
        }
    }
}
