using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Uber_Rider.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/UberTheme", MainLauncher = false)]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			// android:layout_below="@+id/circleImageView"
            SetContentView(Resource.Layout.login);
        }
    }
}