using Imobiliaria.Models;
using Imobiliaria.ViewModels;
using Plugin.Geolocator;
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
	public partial class Maps : StackLayout
	{
        public ItemsViewModel viewModel { get; set; }
        Inicio Inicio { get; set; }

        public Maps (Inicio Inicio)
		{
			InitializeComponent ();
            this.Inicio = Inicio;
            this.viewModel = this.Inicio.viewModel;
            BindingContext = this.viewModel;
           
             viewModel.Mapa = this.Mapa;
            // CarregarDados();
           
         
            Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                                              new Position(-23.0361979, -45.5570624),

                                              Distance.FromMiles(0.5)));


            Mapa.MyLocationEnabled = true;

  

            try
            {
                GetUserPosition();
            }
            catch
            {

            }
        }
        public async  void CarregarDados()
        {

           
            if (viewModel.Imovels.Count > 0)
            {
               
                LoadLocalizacoes();
            }

        }


            public async void LoadLocalizacoes()
            {
           
                foreach (Imovel i in viewModel.Imovels)
                {

                    Pin pin = new Pin
                    {
                        Type = PinType.Generic,

                        Position = new Position(i.localizacao.Latitude, i.localizacao.Longitude),
                        Label = i.titulo,
                        Tag = i.id,
                        Address = i.logradouro,
                        Flat = true,
                        Icon = BitmapDescriptorFactory.FromView(new ViewPin(i)),
                        
                        

                    };
                    if (pin != null)
                    {
                        pin.Clicked += (sender, e) => {
                            //  DisplayAlert(i.Titulo, i.Descricao, "Ver Imóvel");
                            //caroussel.PositionSelected = pin.
                            // ItemDetalhe(i);
                        };

                    }
                Mapa.Pins.Add(pin);
            }



              

            
              
               
            }



        private async void GetUserPosition()
    {

        try
        {
            var Location = CrossGeolocator.Current;
            Location.DesiredAccuracy = 50;

            var position = await Location.GetPositionAsync(TimeSpan.FromSeconds(10));



            Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));

        }
        catch
        {

        }


    }

        private void Caroussel_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            if (!viewModel.IsBusy)
            {
                if (viewModel.Imovels != null)
                {
                    if (viewModel.Imovels.Count > 0)
                    {
                        Imovel imovel = viewModel.Imovels[e.NewValue];
                        if (imovel == null)
                            return;
                         

                        Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                                       new Position(imovel.localizacao.Latitude, imovel.localizacao.Longitude),
                                       Distance.FromMiles(0.5)));
                    }
                }
            }
            
           
          
        }

        private void Caroussel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void Pin_Clicked(object sender, EventArgs e)
        {
            if (e == null)
                return;
            caroussel.Position = Mapa.SelectedPin.ZIndex;

        }
    }
}