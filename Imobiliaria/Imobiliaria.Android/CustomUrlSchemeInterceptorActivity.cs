using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Imobiliaria.Services;
using Xamarin.Auth;

namespace Imobiliaria.Droid
{

    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
        [IntentFilter(
                new[] { Intent.ActionView },
                Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },


        DataSchemes = new[]
                    {
                        "com.googleusercontent.apps.759941497164-n4813q2uu99ij9ravbn8erl5uss3of78",
                        "com.imobstudio.Imobiliaria",
                        /*
                        "urn:ietf:wg:oauth:2.0:oob",
                        "urn:ietf:wg:oauth:2.0:oob.auto",
                        "http://localhost:PORT",
                        "https://localhost:PORT",
                        "http://127.0.0.1:PORT",
                        "https://127.0.0.1:PORT",              
                        "http://[::1]:PORT", 
                        "https://[::1]:PORT", 
                        */
                    },
            
                DataPath = "/oauth2redirect")]
        public class CustomUrlSchemeInterceptorActivity : Activity
        {
            protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                // Convert Android.Net.Url to Uri
                var uri = new Uri(Intent.Data.ToString());

            // Load redirectUrl page
                 AuthenticationState.Authenticator.OnPageLoading(uri);

            
            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);


            Finish();
            }
        }
    }