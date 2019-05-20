using Imobiliaria.Models;
using Imobiliaria.Services;
using Imobiliaria.ViewModels;
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
                this.Inicio.paginaStack.Children.Clear();
                this.Inicio.visiblePageSelected(false);
                this.Inicio.paginaStack.Children.Add(new ItemDetailPage(new ItemDetailViewModel(objeto)));
            }
               
        }

        private void WhatsApp_Clicked(object sender, EventArgs e)
        {

        }

        private void Contato_Clicked(object sender, EventArgs e)
        {

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
                    }
                    catch (Exception ex)
                    {

                    }
                   
                }
                
            }
           
        }
       
    }
}