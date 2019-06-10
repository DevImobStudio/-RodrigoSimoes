using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Imobiliaria.Droid;
using Imobiliaria.Views;
using Android.Content;
using Imobiliaria.Services;
using Xamarin.Auth;
using Newtonsoft.Json.Linq;
using Xamarin.Facebook;

[assembly: ExportRenderer(typeof(ProviderLoginPage), typeof(LoginRender))]
namespace Imobiliaria.Droid
{
    public class LoginRender : PageRenderer
    {
        bool showLogin = true;

        public LoginRender(Context context) : base(context)
        {
            var activity = this.Context as Activity;
        }

           
            

            protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
            {
                base.OnElementChanged(e);

                //Get and Assign ProviderName from ProviderLoginPage
                var loginPage = Element as ProviderLoginPage;
                string providername = loginPage.ProviderName;

                var activity = this.Context as Activity;
                if (showLogin && OAuthConfig.User == null)
                {
                    showLogin = false;
                

                OAuthProviderSetting oauth = new OAuthProviderSetting();
                var auth = oauth.LoginWithProvider(providername);

                        // After facebook,google and all identity provider login completed 
                        auth.Completed += async (sender, eventArgs) =>
                        {
                            if (eventArgs.IsAuthenticated)
                            {
                                OAuthConfig.User = new Models.Usuario();
                                // Get and Save User Details 
                                OAuthConfig.User.Token = eventArgs.Account.Properties["access_token"];
                                OAuthConfig.User.TokenSecret = eventArgs.Account.Properties["expires_in"];

                                var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me"), null, eventArgs.Account);
                                var response = await request.GetResponseAsync();
                                var obj = JObject.Parse(response.GetResponseText());


                                OAuthConfig.User.TwitterId = obj["id"].ToString().Replace("\"", "");
                                OAuthConfig.User.ScreenName = obj["name"].ToString().Replace("\"", "");

                                OAuthConfig.SuccessfulLoginAction.Invoke();
                            }
                            else
                            {
                                // The user cancelled
                            }
                        };


                        activity.StartActivity(auth.GetUI(activity));
                    
                }
            }
        }
    }
