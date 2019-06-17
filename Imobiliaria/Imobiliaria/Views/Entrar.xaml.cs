using Imobiliaria.Services;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Entrar : MenuContainerPage
    {
        static string nome { get; set; }

        public Entrar ()
		{
			InitializeComponent ();
            this.SlideMenu = Services.Sistema.menuSuperior;
        }

        public static void  ExibirResposta(string aMensagem)
        {
            nome = aMensagem;
            Sistema.USUARIO = new Models.Usuario();
            Sistema.USUARIO.nome = nome;



        }

        public void Bind()
        {
            InitializeComponent();
        }

      
        void LoginClick(object sender, EventArgs args)
        {
            Button btncontrol = (Button)sender;
            string providername = btncontrol.Text;
            OAuthConfig._HomePage = this;
            if (OAuthConfig.User == null)
            {
                Navigation.PushModalAsync(new ProviderLoginPage(providername));
            }
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
    }
}