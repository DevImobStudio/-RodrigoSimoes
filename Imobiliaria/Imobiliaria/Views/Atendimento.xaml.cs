using Imobiliaria.Services;
using Plugin.Toast;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Atendimento :MenuContainerPage
    {
        Geocoder coder { get; set; }
        public MenuSuperior menuSuperior { get; set; }
        public Atendimento ()
		{
			InitializeComponent ();
            coder = new Geocoder();
            this.SlideMenu = Services.Sistema.menuSuperior;
            Bind();
        }

        public async void Bind()
        {

            if (Services.Sistema.CONFIG != null)
            {

                    var a = await coder.GetPositionsForAddressAsync(Services.Sistema.CONFIG.endereco);
                    if (a.Any())
                    {
                        Services.Sistema.CONFIG.position = new Position(a.ElementAt(0).Latitude, a.ElementAt(0).Longitude);
                    }

               

                Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(Services.Sistema.CONFIG.position,
                                      Distance.FromMiles(0.5)));
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = (Services.Sistema.CONFIG.position),
                    Label = Services.Sistema.CONFIG.titulo,
                    Address = Services.Sistema.CONFIG.endereco,
                    Icon = BitmapDescriptorFactory.FromView(new ViewPin())
            };

                Mapa.Pins.Add(pin);
                this.BindingContext = Services.Sistema.CONFIG;
            }
        }

        private void facebook_Clicked(object sender, EventArgs e)
        {
            var urlStore = Device.OnPlatform(Services.Sistema.CONFIG.facebook, Services.Sistema.CONFIG.facebook, Services.Sistema.CONFIG.facebook); //iOS,Android,Windows Device.OpenUri(new Uri(urlStore));
            Device.OpenUri(new Uri(urlStore));
        }

        private void youtube_Clicked(object sender, EventArgs e)
        {
            var urlStore = Device.OnPlatform(Services.Sistema.CONFIG.youtube, Services.Sistema.CONFIG.youtube, Services.Sistema.CONFIG.youtube); //iOS,Android,Windows Device.OpenUri(new Uri(urlStore));
            Device.OpenUri(new Uri(urlStore));
        }

        private void instagram_Clicked(object sender, EventArgs e)
        {
            var urlStore = Device.OnPlatform(Services.Sistema.CONFIG.instagram, Services.Sistema.CONFIG.instagram, Services.Sistema.CONFIG.instagram); //iOS,Android,Windows Device.OpenUri(new Uri(urlStore));
            Device.OpenUri(new Uri(urlStore));
        }

        private void Whatsapp_Clicked(object sender, EventArgs e)
        {
            try
            {
                Sistema.WhatsApp();
            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastMessage(ex.Message, Plugin.Toast.Abstractions.ToastLength.Long);
            }
        }

        private void Telefone_Clicked(object sender, EventArgs e)
        {
            try
            {
                Sistema.Contato();
            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastMessage(ex.Message, Plugin.Toast.Abstractions.ToastLength.Long);
            }
        }
    }
}