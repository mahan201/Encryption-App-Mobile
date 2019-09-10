#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SyncfusionXamarinApp1
{
    public partial class App : Application
    {
        

        public App()
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTIzMzYyQDMxMzcyZTMxMmUzMEo4Tk9JazhWeDZvWEtRZHMzWHUyL2NTRW9pRUM2Qi9Pam5abU4wOHVxOWM9");

            InitializeComponent();

            MainPage = new NavigationPage(new SyncfusionXamarinApp1.MainPage())
            {

                BarBackgroundColor = Color.FromHex("#292929"),
                BarTextColor = Color.White
              
                
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
