using Imobiliaria.Models;
using Imobiliaria.Services;
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
        public StackLayout paginaStack { get; set; }
        public Inicio ()
		{
            InitializeComponent();

            viewModel = new ItemsViewModel();
            paginaStack = pagina;
            Maps = new Maps(this);
            ListaImoveis = new ListaImoveis(this);
           

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
            ForceLayout();



        }
        public void setarCor()
        {
            PageSelected.TintColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
            PageSelected.DisabledColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
            PageSelected.DisabledColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
            searchI.TextColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
            searchL.TextColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
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
        public void visiblePageSelected(bool set)
        {
            this.PageSelected.IsVisible = set;

        }



        private void SegControl_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            pagina.Children.Clear();

            switch (e.NewValue)
            {
                case 0:
                    pagina.Children.Add(Maps);
                    break;
                case 1:
                    pagina.Children.Add(ListaImoveis);
                    break;

            }
        }

        protected  override bool OnBackButtonPressed()
        {
            var b = pagina.Children[0];
            var c = b.GetType();
            var d = b.TabIndex;
            if ((b.GetType() != Maps.GetType()) && (b.GetType() != ListaImoveis.GetType()))
            {
                pagina.Children.Clear();
                pagina.Children.Add(ListaImoveis);
                visiblePageSelected(true);
            }
            return true;
        }
    }
}