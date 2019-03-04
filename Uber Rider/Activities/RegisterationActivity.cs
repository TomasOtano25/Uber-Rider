﻿using System;
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
using Firebase.Auth;
using Firebase.Database;
using Firebase;
using Uber_Rider.EventListeners;
using Java.Util;

namespace Uber_Rider.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/UberTheme", MainLauncher = false)]
    public class RegisterationActivity : AppCompatActivity
    {
        TextInputLayout fullNameText;
        TextInputLayout phoneText;
        TextInputLayout emailText;
        TextInputLayout passwordText;
        Button registerButton;
        CoordinatorLayout rootView;
        TextView clickToLoginText;

        FirebaseAuth mAuth;
        FirebaseDatabase database;

        TaskCompletionListener TaskCompletionListener = new TaskCompletionListener();

        string fullname, phone, email, password;

        ISharedPreferences preferences = Application.Context.GetSharedPreferences("userinfo", FileCreationMode.Private);
        ISharedPreferencesEditor editor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.register);

            InitializeFirebase();
            mAuth = FirebaseAuth.Instance;

            ConnectControl();
        }

        void InitializeFirebase()
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
                database = FirebaseDatabase.GetInstance(app);
            }
            else
            {
                database = FirebaseDatabase.GetInstance(app);
            }
        }

        void ConnectControl()
        {
            fullNameText = (TextInputLayout)FindViewById(Resource.Id.fullNameText);
            phoneText = (TextInputLayout)FindViewById(Resource.Id.phoneText);
            emailText = (TextInputLayout)FindViewById(Resource.Id.emailText);
            passwordText = (TextInputLayout)FindViewById(Resource.Id.passwordText);
            registerButton = (Button)FindViewById(Resource.Id.registerButton);
            rootView = (CoordinatorLayout)FindViewById(Resource.Id.rootView);
            clickToLoginText = (TextView)FindViewById(Resource.Id.clickToLoginText);

            registerButton.Click += RegisterButton_Click;
            clickToLoginText.Click += ClickToLoginText_Click;
        }

        private void ClickToLoginText_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
            Finish();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            fullname = fullNameText.EditText.Text;
            phone = phoneText.EditText.Text;
            email = emailText.EditText.Text;
            password = passwordText.EditText.Text;

            if (string.IsNullOrEmpty(fullname) || fullname.Length < 3)
            {
                Snackbar.Make(rootView, "Please enter a valid name.", Snackbar.LengthShort).Show();
                return;
            }
            else if (string.IsNullOrEmpty(phone) || phone.Length < 9)
            {
                Snackbar.Make(rootView, "Please enter a valid phone number.", Snackbar.LengthShort).Show();
                return;
            }
            else if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                Snackbar.Make(rootView, "Please enter a valid email.", Snackbar.LengthShort).Show();
                return;
            }
            else if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                Snackbar.Make(rootView, "Please enter a password upto 8 characters.", Snackbar.LengthShort).Show();
                return;
            }

            RegisterUser(fullname, phone, email, password);
        }

        void RegisterUser(string name, string phone, string email, string password)
        {
            TaskCompletionListener.Success += TaskCompletionListener_Success;
            TaskCompletionListener.Failure += TaskCompletionListener_Failure;

            mAuth.CreateUserWithEmailAndPassword(email, password)
                .AddOnSuccessListener(this, TaskCompletionListener)
                .AddOnFailureListener(this, TaskCompletionListener);
        }

        private void TaskCompletionListener_Failure(object sender, EventArgs e)
        {
            Snackbar.Make(rootView, "User registration failed.", Snackbar.LengthShort).Show();
        }

        private void TaskCompletionListener_Success(object sender, EventArgs e)
        {
            Snackbar.Make(rootView, "User registration was Successfull.", Snackbar.LengthShort).Show();

            HashMap userMap = new HashMap();
            userMap.Put("email", email);
            userMap.Put("phone", phone);
            userMap.Put("fullname", fullname);


            DatabaseReference userReference = database.GetReference($"users/{mAuth.CurrentUser.Uid}");
            userReference.SetValue(userMap);
        }

        void SaveToSharePreference()
        {
            /*ISharedPreferences preferences = Application.Context.GetSharedPreferences("userinfo", FileCreationMode.Private);
            ISharedPreferencesEditor editor;*/
            editor = preferences.Edit();

            editor.PutString("email", email);
            editor.PutString("fullname", fullname);
            editor.PutString("phone", phone);

            editor.Apply();
        }

        void RetriveData()
        {
            string email = preferences.GetString("email", "");
        }
    }
}