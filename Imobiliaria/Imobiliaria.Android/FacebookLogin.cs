using Android.App;
using Android.Content;
using Imobiliaria.Droid;
using Imobiliaria.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
//[assembly: ExportRenderer(typeof(ProviderLoginPage), typeof(FacebookLogin))]
namespace Imobiliaria.Droid
{
    public class FacebookLogin : PageRenderer
    {
        public FacebookLogin(Context context) : base(context)
        {
   
            var activity = this.Context as Activity;
           

            var auth = new OAuth2Authenticator(
                clientId: "456228191618171",
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) => {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                    var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me"), null, eventArgs.Account);
                    var response = await request.GetResponseAsync();
                    var obj = JObject.Parse(response.GetResponseText());

                    var id = obj["id"].ToString().Replace("\"", "");
                    var name = obj["name"].ToString().Replace("\"", "");
                  
 
                    
                }
                else
                {
                  
                    
                }
            };

            activity.StartActivity (auth.GetUI(activity));	
        }
    }
}