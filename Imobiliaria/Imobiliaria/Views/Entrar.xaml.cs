using Imobiliaria.Models;
using Imobiliaria.Services;
using Newtonsoft.Json;
using Plugin.Toast;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Entrar : MenuContainerPage
    {
        static string nome { get; set; }

        public Entrar()
        {
            this.Bind();
           
            this.SlideMenu = Services.Sistema.menuSuperior;
        }

        public static void ExibirResposta(string aMensagem)
        {
            nome = aMensagem;
            Sistema.USUARIO = new Models.Usuario();
            Sistema.USUARIO.nome = nome;



        }

        public void Bind()
        {
            InitializeComponent();

            if (Sistema.USUARIO == null)
            {
                Login.IsVisible = true;
                Logout.IsVisible = false;
            }
            else
            {
                Login.IsVisible = false;
                Logout.IsVisible = true;
                Nome.Text = Sistema.USUARIO.name;
                Email.Text = Sistema.USUARIO.email;
                Image.Source = new UriImageSource { CachingEnabled = true, Uri = new Uri(Sistema.USUARIO.picture) };
            }
            InitializeComponent();
        }


        void LoginClick(object sender, EventArgs args)
        {
            Button btncontrol = (Button)sender;
            string providername = btncontrol.Text;
            /*  OAuthConfig._HomePage = this;
              if (OAuthConfig.User == null)
              {
                  Navigation.PushModalAsync(new ProviderLoginPage(providername));
              }
              */

            var Authenticator = new OAuth2Authenticator(
                                    "759941497164-n4813q2uu99ij9ravbn8erl5uss3of78.apps.googleusercontent.com",
                                   null,
                                   "https://www.googleapis.com/auth/userinfo.email",
                                   new Uri("https://accounts.google.com/o/oauth2/auth"),
                                   new Uri("com.googleusercontent.apps.759941497164-n4813q2uu99ij9ravbn8erl5uss3of78:/oauth2redirect"),
                                   new Uri("https://oauth2.googleapis.com/token"),
                                   null,
                                   isUsingNativeUI: true


                                    );

            Authenticator.Completed += OnAuthCompleted;
            Authenticator.Error += OnAuthError;
            AuthenticationState.Authenticator = Authenticator;
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(Authenticator);
        }



        private async void BtnFacebookLogin_Clicked(object sender, EventArgs e)
        {
            Button btncontrol = (Button)sender;
            string providername = btncontrol.Text;
            if (Sistema.USUARIO == null)
            {
                await Navigation.PushModalAsync(new ProviderLoginPage(providername));
                //Need to create ProviderLoginPage so follow Step 4 and Step 5  
                await Navigation.PopModalAsync();
            }
        }
        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Usuario user = null;
            if (e.IsAuthenticated)
            {
                
                var request = new OAuth2Request("GET", new Uri("https://www.googleapis.com/oauth2/v2/userinfo"), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                   
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<Usuario>(userJson);
                    await Sistema.DATABASE.database.QueryAsync<Usuario>("UPDATE Usuario set logado = 1");
                    user.logado = true;
                    await Sistema.DATABASE.database.InsertAsync(user);
                    Sistema.USUARIO = user;
                    this.Bind();

                }

                
            }
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
            {
                var authenticator = sender as OAuth2Authenticator;
                if (authenticator != null)
                {
                    authenticator.Completed -= OnAuthCompleted;
                    authenticator.Error -= OnAuthError;
                }
            CrossToastPopUp.Current.ShowToastMessage("Erro ao fazer login", Plugin.Toast.Abstractions.ToastLength.Long);
            Debug.WriteLine("Authentication error: " + e.Message);
            }

        private void Sair_Clicked(object sender, EventArgs e)
        {
            Sistema.USUARIO = null;
            Login.IsVisible = true;
            Logout.IsVisible = false;
            this.Bind();
        }
    }
    }