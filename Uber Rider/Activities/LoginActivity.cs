using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Uber_Rider.EventListeners;

namespace Uber_Rider.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/UberTheme", MainLauncher = false)]
    public class LoginActivity : AppCompatActivity
    {
        TextInputLayout emailText;
        TextInputLayout passwordText;
        Button loginButton;
        CoordinatorLayout rootView;

        FirebaseAuth mAuth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // android:layout_below="@+id/circleImageView"
            SetContentView(Resource.Layout.login);

            ConnectControl();

            InitializeFirebase();
        }

        private void ConnectControl()
        {
            emailText = (TextInputLayout)FindViewById(Resource.Id.emailText);
            passwordText = (TextInputLayout)FindViewById(Resource.Id.passwordText);
            loginButton = (Button)FindViewById(Resource.Id.loginButton);
            rootView = (CoordinatorLayout)FindViewById(Resource.Id.rootView);

            loginButton.Click += LoginButton_Click;
        }

        private void InitializeFirebase()
        {
            var app = FirebaseApp.InitializeApp(this);
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetApplicationId("uber-clone-a1c9b")
                    .SetApiKey("AIzaSyA_iDmRc4MUWwvUuPxlus7NyywRSzah0IA")
                    .SetDatabaseUrl("https://uber-clone-a1c9b.firebaseio.com")
                    .SetStorageBucket("uber-clone-a1c9b.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(this, options);
                mAuth = FirebaseAuth.Instance;
            }
            else
            {
                mAuth = FirebaseAuth.Instance;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string email, password;

            email = emailText.EditText.Text;
            password = passwordText.EditText.Text;

            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                Snackbar.Make(rootView, "Please enter a valid email.", Snackbar.LengthLong).Show();
                return;
            }
            else if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                Snackbar.Make(rootView, "Please enter a valid password.", Snackbar.LengthLong).Show();
                return;
            }

            TaskCompletionListener taskCompletionListener = new TaskCompletionListener();

            taskCompletionListener.Success += TaskCompletionListener_Success;
            taskCompletionListener.Failure += TaskCompletionListener_Failure;

            mAuth.SignInWithEmailAndPassword(email, password)
                .AddOnSuccessListener(taskCompletionListener)
                .AddOnFailureListener(taskCompletionListener);
        }

        private void TaskCompletionListener_Failure(object sender, EventArgs e)
        {
            Snackbar.Make(rootView, "Login Failed.", Snackbar.LengthLong).Show();
        }

        private void TaskCompletionListener_Success(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}