using Imobiliaria.Models;
using Imobiliaria.ViewModels;
using Plugin.Geolocator;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inicio : ContentPage
    {

        private Maps Maps { get; set; }
        private ListaImoveis ListaImoveis { get; set; }

        public ItemsViewModel viewModel { get; set; }

        public Inicio ()
		{
			InitializeComponent ();
            viewModel = new ItemsViewModel();





          /*  if (PageSelected.SelectedSegment == 0)
            {
                pagina.Children.Add(Maps = new Maps(this));
            }
            else
            {
                pagina.Children.Add(ListaImoveis = new ListaImoveis(this));
            }
            */
           

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);

        }
     /*   public void Handle_ValueChanged(object o, int e)
        {
            pagina.Children.Clear();

            switch (e)
            {
                case 0:
                    pagina.Children.Add(Maps = new Maps(this));
                    break;
                case 1:
                    pagina.Children.Add(ListaImoveis = new ListaImoveis(this));
                    break;
               
            }
        }
        */




        private void SegControl_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            pagina.Children.Clear();

            switch (e.NewValue)
            {
                case 0:
                    pagina.Children.Add(Maps = new Maps(this));
                    break;
                case 1:
                    pagina.Children.Add(ListaImoveis = new ListaImoveis(this));
                    break;

            }
        }
    }
}