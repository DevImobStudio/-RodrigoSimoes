using Imobiliaria.Models;
using Imobiliaria.Services;
using Imobiliaria.ViewModels;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.OpenWhatsApp;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaImoveis : StackLayout
	{

        Inicio Inicio { get; set; }
		public ListaImoveis(Inicio Inicio)
		{


			InitializeComponent ();
            
            this.Inicio = Inicio;
            BindingContext = this.Inicio.viewModel;

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Imovel;
            if (item == null)
                return;

           // await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        private async void Detalhes_Clicked(object sender, EventArgs e)
        {
            
            var objeto =  ((sender as Button).CommandParameter) as Imovel;
            if (objeto != null)
            {
                await this.Inicio.CarregarDetalhes(objeto);
            }
               
        }

        private async void WhatsApp_Clicked(object sender, EventArgs e)
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
            Sistema.Contato();
        }

        private async void Favoritos_Clicked(object sender, EventArgs e)
        {
            var objeto = ((sender as Button).CommandParameter) as Imovel;
            if (objeto != null)
            {
                
                var favorito = Inicio.viewModel.favoritos.Where(p => p.idImovel == objeto.id).FirstOrDefault();
                if (favorito == null)
                {
                    try
                    {
                        await Sistema.DATABASE.database.InsertAsync(new Models.Favoritos()
                        {

                            idImovel = objeto.id

                        });
                        CrossToastPopUp.Current.ShowToastMessage("Imovel "+ objeto.titulo + " adicionado com sucesso", Plugin.Toast.Abstractions.ToastLength.Long);
                    }
                    catch (Exception ex)
                    {
                        CrossToastPopUp.Current.ShowToastMessage(ex.Message, Plugin.Toast.Abstractions.ToastLength.Long);
                    }
                   
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastMessage("Imovel " + objeto.titulo + "já pertence ao seus favoritos", Plugin.Toast.Abstractions.ToastLength.Long);
                }
                
            }
           
        }
       
    }
}