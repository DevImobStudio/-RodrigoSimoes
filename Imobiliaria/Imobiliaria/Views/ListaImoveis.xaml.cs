using Imobiliaria.Models;
using Imobiliaria.Services;
using Imobiliaria.ViewModels;
using Plugin.Iconize;
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

        private void Detalhes_Clicked(object sender, EventArgs e)
        {
            
            var objeto =  ((sender as Button).CommandParameter) as Imovel;
            if (objeto != null)
            {
                this.Inicio.CarregarDetalhes(objeto);
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
            IconButton icon = sender as IconButton;
            if (Sistema.USUARIO != null)
            {
                var objeto = (icon.CommandParameter) as Imovel;
                if (objeto != null)
                {
                    var favorito = await Sistema.DATABASE.database.Table<Models.Favoritos>().Where(p => p.idImovel == objeto.id && p.idUsuario == Sistema.USUARIO.cod).FirstOrDefaultAsync();
                    if (favorito == null)
                    {
                        try
                        {
                            await Sistema.DATABASE.database.InsertAsync(new Models.Favoritos()
                            {
                                idUsuario = Sistema.USUARIO.cod,
                                idImovel = objeto.id

                            });
                            
                            this.Inicio.viewModel.Imovels[this.Inicio.viewModel.Imovels.IndexOf(objeto)].favorito = "Red";
                            BindingContext = this.Inicio.viewModel;
                            this.ItemsListView.ItemsSource = this.Inicio.viewModel.Imovels;
                            CrossToastPopUp.Current.ShowToastMessage("Imovel " + objeto.titulo + " adicionado com sucesso", Plugin.Toast.Abstractions.ToastLength.Long);
                            




                        }
                        catch (Exception ex)
                        {
                            CrossToastPopUp.Current.ShowToastMessage(ex.Message, Plugin.Toast.Abstractions.ToastLength.Long);
                        }

                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastMessage("Imovel " + objeto.titulo + " já pertence ao seus favoritos", Plugin.Toast.Abstractions.ToastLength.Long);
                    }

                }
               
            }
            else
            {
                OAuthConfig.IndexPage = 0;
                CrossToastPopUp.Current.ShowToastMessage("Faça o login primeiro para adicionar o imóvel ao seus favoritos");
                Sistema.TABBEDPAGE.CurrentPage = Sistema.TABBEDPAGE.Entrar;

            }


        }

        private void ItemsListView_Refreshing(object sender, EventArgs e)
        {
            BindingContext = this.Inicio.viewModel;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Imovel objeto = new Imovel();
            objeto = ((TappedEventArgs)e).Parameter as Imovel;

            if (objeto != null)
            {
                Inicio.CarregarDetalhes(objeto);
            }
        }
    }
}