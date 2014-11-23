using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace AskWatson.UserControls
{
    public sealed partial class AdBannerUserControl : UserControl
    {
        public AdBannerUserControl()
        {
            this.InitializeComponent();
#if DEBUG
            adControl.ApplicationId = "test_client";
            adControl.AdUnitId = "Image480_80";
#else
            adControl.ApplicationId = "9b706f1c-89a4-4b86-8915-9a734dde9a3e";
            adControl.AdUnitId = "196602";
#endif
        }

        private void adControl_ErrorOccurred(object sender, Microsoft.Advertising.Mobile.Common.AdErrorEventArgs e)
        {
            Debug.WriteLine("adControl error: " + e.Error.Message);
        }
    }
}
