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
	public partial class Entrar : ContentPage
	{
		public Entrar ()
		{
			InitializeComponent ();
		}

        public static Task ExibirResposta(string aMensagem)
        {
            throw new NotImplementedException(aMensagem);
        }

        private async void BtnFacebookLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
    }
}