using Imobiliaria.Models;
using Imobiliaria.Services;
using Newtonsoft.Json;
using Plugin.Toast;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarians.FacebookLogin;
using Xamarians.GoogleLogin.Interface;
using Xamarians.Media;
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
                if (Sistema.USUARIO.imagem != null)
                {
                    Image.Source = new UriImageSource { CachingEnabled = true, Uri = new Uri(Sistema.USUARIO.imagem) };
                }
                
            }
        }

        void LoginClickFacebook(object sender, EventArgs args)
        {
            Button btncontrol = (Button)sender;
            string providername = btncontrol.Text;
            OAuthConfig.TipoOAuth = "facebook";
            var Authenticator = new OAuth2Authenticator(
                        clientId: "334399370606956",
                        scope: "",
                        authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"), 
                        redirectUrl: new Uri("fb334399370606956://authorize"),
                        isUsingNativeUI: true
                         );

            Authenticator.Completed += OnAuthCompleted;
            Authenticator.Error += OnAuthError;
            AuthenticationState.Authenticator = Authenticator;
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(Authenticator);
    }

    
    
    void LoginClick(object sender, EventArgs args)
        {
            Button btncontrol = (Button)sender;
            string providername = btncontrol.Text;
            OAuthConfig.TipoOAuth = "google";
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
            Response response = null;
            if (e.IsAuthenticated)
            {
                
                if (OAuthConfig.TipoOAuth == "facebook")
                {
                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=email,name,first_name,last_name,picture"), null, e.Account);

                    response = await request.GetResponseAsync();

                    if (response != null)
                    {

                        string userJson = await response.GetResponseTextAsync();
                        user = JsonConvert.DeserializeObject<Usuario>(userJson);
                        if (user.picture != null)
                        {
                            if (user.picture.data != null)
                            {
                                user.imagem = user.picture.data.url;
                            }
                        }
                    }
                    
                }
                if (OAuthConfig.TipoOAuth == "google")
                {
                    var request = new OAuth2Request("GET", new Uri("https://www.googleapis.com/userinfo/v2/me"), null, e.Account);
                    response = await request.GetResponseAsync();
                    if (response != null)
                    {

                        string userJson = await response.GetResponseTextAsync();
                        UserGoogle user2 = JsonConvert.DeserializeObject<UserGoogle>(userJson);
                        user = new Usuario();
                        user.name = user2.name;
                        user.imagem = user2.picture;
                        user.id = user2.id;
                        user.email = user2.email;

                    }

                }
                if (response != null)
                {
                    await Sistema.DATABASE.database.QueryAsync<Usuario>("UPDATE Usuario set logado = 0");
                    user.logado = true;
                    Usuario userCadastrados = await Sistema.DATABASE.database.Table<Usuario>().Where(p=> p.id == user.id).FirstOrDefaultAsync();
                    
                    if (userCadastrados== null)
                    {
                        await Sistema.DATABASE.database.InsertAsync(user);
                    }
                    else
                    {
                        userCadastrados.logado = true;
                        userCadastrados.name = user.name;
                        userCadastrados.email = user.email;
                        userCadastrados.imagem = user.imagem;

                        await Sistema.DATABASE.database.UpdateAsync(userCadastrados);
                    }
                   
                    Sistema.USUARIO = user;
                    Services.Sistema.TABBEDPAGE.trocarPagina();

                }

                
            }
        }



        private string GenerateFilePath()
        {
            return Path.Combine(MediaService.Instance.GetPublicDirectoryPath(), MediaService.Instance.GenerateUniqueFileName("jpg"));
        }

        private async void FbSignInClicked(object sender, EventArgs e)
        {
            var result = await DependencyService.Get<IFacebookLogin>().SignIn();
            if (result.Status == Xamarians.FacebookLogin.Platforms.FBStatus.Success)
            {
                Usuario user = new Usuario();
                user.name = result.Name;
                user.nome = "facebook";
               // user.imagem = result..picture;
                user.id = result.UserId;
                user.email = result.Email;

                await Sistema.DATABASE.database.QueryAsync<Usuario>("UPDATE Usuario set logado = 0");
                user.logado = true;
                Usuario userCadastrados = await Sistema.DATABASE.database.Table<Usuario>().Where(p => p.id == user.id).FirstOrDefaultAsync();

                if (userCadastrados == null)
                {
                    await Sistema.DATABASE.database.InsertAsync(user);
                }
                else
                {
                    userCadastrados.logado = true;
                    userCadastrados.name = user.name;
                    userCadastrados.email = user.email;
                    userCadastrados.imagem = user.imagem;

                    await Sistema.DATABASE.database.UpdateAsync(userCadastrados);
                }

                Sistema.USUARIO = user;
                Services.Sistema.TABBEDPAGE.trocarPagina();


            }
            else
            {
                await DisplayAlert("Error", result.Message, "Ok");
            }

        }

        private async void FbSignOutClicked()
        {
            var result = await DependencyService.Get<IFacebookLogin>().SignOut();
            if (result.Status == Xamarians.FacebookLogin.Platforms.FBStatus.Success)
            {
                await DisplayAlert("Success", result.Message, "Ok");
            }
            else
            {
                await DisplayAlert("Error", result.Message, "Ok");
            }
        }


        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var result = await DependencyService.Get<IGoogleLogin>().SignIn();
            if (result.IsSuccess)
            {
                //imgProfile.Source = result.Image;
                var name = result.Name;
                await DisplayAlert("", "Account Name -" + name, "Ok");
            }
        }


        private async void FbImageShareClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IFacebookLogin>().ShareImageOnFacebook("Hi, This is demo text", "image-filePath");
        }

        private void FbTextShareClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IFacebookLogin>().ShareTextOnFacebook("Hi, This is a demo text");
        }

        private void FbLinkShareClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IFacebookLogin>().ShareLinkOnFacebook("your Link", "Hi, this is demo text", "http://www.google.com");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.Bind();

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

        private async void Sair_Clicked(object sender, EventArgs e)
        {
            await Sistema.DATABASE.database.QueryAsync<Usuario>("UPDATE Usuario set logado = 0");

            if (Sistema.USUARIO.nome == "facebook")
            {
                FbSignOutClicked();
            }

            Sistema.USUARIO = null;
            Login.IsVisible = true;
            Logout.IsVisible = false;
            this.Bind();
        }
    }
    }