namespace Uber_Rider.Activities
{
    using Android.App;
    using Android.OS;
    using Android.Support.V7.App;

    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Thread.Sleep(2000);
            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(RegisterationActivity));
        }
    }
}