using Imobiliaria.Models;
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

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}