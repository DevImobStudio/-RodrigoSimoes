using Imobiliaria.Models;
using Imobiliaria.Services;
using Imobiliaria.ViewModels;
using Plugin.Toast;
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
	public partial class Favoritos : ContentPage
	{
        Inicio inicio { get; set; }
		public Favoritos ()
		{
			InitializeComponent ();
           // LoadFavoritos();


        }
        private async void LoadFavoritos()
        {
            try
            {
                List<Models.Favoritos> favoritos = await Services.Sistema.DATABASE.database.Table<Models.Favoritos>().ToListAsync();
                List<Models.Imovel> imovels = await Services.Sistema.RESTAPI.getAsync<List<Models.Imovel>>("");
                List<Models.Imovel> imovelsFavoritos = new List<Models.Imovel>();
             
                foreach(var i in favoritos)
                {
                    Models.Imovel imovel = imovels.Where(p => p.id == i.idImovel).FirstOrDefault();
                    if (imovel != null)
                    {
                        imovelsFavoritos.Add(imovel);
                    }
                    else
                    {
                        imovelsFavoritos.Add(new Models.Imovel()
                        {
                            id = i.idImovel,
                            titulo = "Indisponível"
                        });
                    }
                   
                }
                ItemsListView.ItemsSource = imovelsFavoritos;
                BindingContext = imovelsFavoritos;
            }
            catch(Exception on)
            {

            }
           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            LoadFavoritos();



        }

        private void Detalhes_Clicked(object sender, EventArgs e)
        {

            var objeto = ((sender as Button).CommandParameter) as Imovel;
            if (objeto != null)
            {
              
            }

        }

        private void WhatsApp_Clicked(object sender, EventArgs e)
        {
            try
            {
                var objeto = ((sender as Button).CommandParameter) as Imovel;
                if (objeto != null)
                {
                    Sistema.WhatsApp(objeto);
                }
            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastMessage(ex.Message, Plugin.Toast.Abstractions.ToastLength.Long);
            }
        }

        private void Contato_Clicked(object sender, EventArgs e)
        {
            Sistema.Contato("+552112991734478");
        }

        private void Remove_Clicked(object sender, EventArgs e)
        {

        }
    }
}