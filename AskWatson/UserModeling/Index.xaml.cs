using AskWatson.Common;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace AskWatson.UserModeling
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Index : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Index()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override  void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                // we should have the current modeling data available, reload it
                Portable.Models.UserModelingResponse.Rootobject userModel = null;
                
                userModel = Portable.AskWatsonService.ModelUser(App.CurrentModelingResponse);

                if (userModel != null &&
                    userModel.tree.children != null &&
                    userModel.tree.children.Count() > 0)
                {
                    TwitterUsernameTextBox.Text = App.CurrentModelingUsername; 
                    personalitiesListView.ItemsSource = userModel.tree.children;
                    detailsScrollViewer.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }

            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void ModelUserButton_Click(object sender, RoutedEventArgs e)
        {
            updateProgressStackPanel.Visibility = Windows.UI.Xaml.Visibility.Visible;
            MessageDialog errorDialog = null;

            // retrieve twitter user statuses
            try
            {
                App.CurrentModelingUsername = TwitterUsernameTextBox.Text;
                string statusesText = await _GetTwitterStatusText();
                Portable.Models.UserModelingResponse.Rootobject userModel = null;
                
                if (!string.IsNullOrEmpty(statusesText))
                {
                    progressTextBlock.Text = "modeling user...";
                    App.CurrentModelingResponse = await Portable.AskWatsonService.GetModelUserJsonResponse(statusesText);
                    userModel = Portable.AskWatsonService.ModelUser(App.CurrentModelingResponse);

                    if (userModel != null &&
                        userModel.tree.children != null &&
                        userModel.tree.children.Count() > 0)
                    {
                        personalitiesListView.ItemsSource = userModel.tree.children;
                        detailsScrollViewer.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    }
                    else
                    {

                    }
                }
                else
                {
                    errorDialog = new MessageDialog("Unable to retrieve twitter user");
                }
            }
            catch (Exception ex)
            {
                errorDialog = new MessageDialog(string.Format("Error: {0}", ex.Message));
                errorDialog.ShowAsync();
            }
            finally
            {
                updateProgressStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            
        }

        //private async Task<string> _GetTwitterStatusText()
        private async Task<string> _GetTwitterStatusText()
        {
            try
            {
                SingleUserAuthorizer singleUserAuthorizer = new SingleUserAuthorizer()
                {
                    CredentialStore = new SingleUserInMemoryCredentialStore()
                    {
                        ConsumerKey = Application.Current.Resources["TwitterConsumerKey"].ToString(),
                        ConsumerSecret = Application.Current.Resources["TwitterConsumerSecret"].ToString(),
                        AccessToken = Application.Current.Resources["TwitterAccessToken"].ToString(),
                        AccessTokenSecret = Application.Current.Resources["TwitterAccessTokenSecret"].ToString()
                    }
                };

                TwitterContext twitterContext = new TwitterContext(singleUserAuthorizer);

                string screenName = (TwitterUsernameTextBox.Text.StartsWith("@")) ? TwitterUsernameTextBox.Text : string.Format("@{0}", TwitterUsernameTextBox.Text);

                List<Status> twitterFeed = await (from tweet in twitterContext.Status
                                                  where tweet.Type == StatusType.User && tweet.ScreenName == screenName
                                                  select tweet).ToListAsync();

                StringBuilder statusSb = new StringBuilder();

                App.CurrentModelingUserProfileImageUrl = (twitterFeed.FirstOrDefault() == null) ? null : twitterFeed.FirstOrDefault().User.ProfileImageUrl;

                foreach (Status s in twitterFeed)
                {
                    statusSb.AppendFormat(" {0}", s.Text.Replace(":", " ").Replace(".", " ").Replace(",", " ").Replace("!", " ").Replace("?", " "));
                }

                return statusSb.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void visualizeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserModeling.Visualize));
        }
    }
}
