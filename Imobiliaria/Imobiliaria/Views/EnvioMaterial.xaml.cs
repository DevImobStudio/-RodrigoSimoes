using Imobiliaria.Models;
using Imobiliaria.Services;
using Plugin.Toast;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EnvioMaterial : StackLayout
	{
        Imovel imovel { get; set; }

        public EnvioMaterial (Imovel imovel)
		{
			InitializeComponent ();            
            if (Services.Sistema.USUARIO != null)
            {
                this.imovel = imovel;
                this.Nome.Text = Services.Sistema.USUARIO.name;
                this.Email.Text = Services.Sistema.USUARIO.email;
                this.Whatsapp.Text = Services.Sistema.USUARIO.whatsapp;
            }
         //   else
        //    {
                
       //         CrossToastPopUp.Current.ShowToastMessage("Faça o login primeiro para ver seus imóveis no favoritos");
       //         Services.OAuthConfig.IndexPage = 0;
       //         Services.Sistema.TABBEDPAGE.CurrentPage = Services.Sistema.TABBEDPAGE.Entrar;
       //     }
          
        }

       

        private async void Enviar_Clicked(object sender, EventArgs e)
        {
            Services.Sistema.USUARIO.whatsapp = this.Whatsapp.Text;
            Services.Sistema.DATABASE.database.UpdateAsync(Services.Sistema.USUARIO);
       
            var myHttpClient = new HttpClient();
            var uri = new Uri("https://www.api.rodrigosimoesimoveis.com.br/post/lead/");

            //Since this sample restful api url accepts any json structure, we can structure the data like this
            HttpContent formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "api_secret", "EYTAtsANMai5U66RDgZcdyuuDGxP2szoat8r"},
                { "nome", Nome.Text },
                { "email", this.Email.Text },
                { "whatsapp", Whatsapp.Text},
                { "id_imovel", this.imovel.id },
                { "titulo_imovel", this.imovel.titulo }
            });
            formContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await myHttpClient.PostAsync(uri.ToString(), formContent);

            if (response.IsSuccessStatusCode) {
               
                string respContent = await response.Content.ReadAsStringAsync();
                if (respContent.Contains("200"))
                {
                    CrossToastPopUp.Current.ShowToastMessage("Dados enviados com sucesso!");
                  
                    await Navigation.PushPopupAsync(new ModalObrigada(this));
                    Sistema.TABBEDPAGE.CurrentPage = Sistema.TABBEDPAGE.Inicio;
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastMessage("Erro ao enviar dados, tente novamente");
                }
            }
            else
            {
                CrossToastPopUp.Current.ShowToastMessage("Erro ao enviar dados, tente novamente");
            }
            

        }
       
    }
}