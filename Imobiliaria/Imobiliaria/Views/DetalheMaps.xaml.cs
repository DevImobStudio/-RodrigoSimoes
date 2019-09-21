using Imobiliaria.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalheMaps : ContentPage
	{
		public DetalheMaps (Imovel imovel)
		{
			InitializeComponent ();

            Title = imovel.titulo;
            Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(imovel.position.Latitude, imovel.position.Longitude), Distance.FromMiles(0.20)));
            Mapa.IsIndoorEnabled = true;
            Mapa.MapType = MapType.Street;
     

            Mapa.Pins.Add(new Pin()
            {
                Type = PinType.Place,
                Position = (imovel.position),
                Label = imovel.titulo,
                Address = imovel.logradouro,
                Icon = BitmapDescriptorFactory.FromView(new ViewPin())
            });
            var a = Services.Sistema.CONFIG.cor_padrao.Replace("#", "");
            double r1 = int.Parse(a.Substring(0, 2), NumberStyles.HexNumber);
            double g1 = int.Parse(a.Substring(2, 2), NumberStyles.HexNumber);
            double b1 = int.Parse(a.Substring(4, 2), NumberStyles.HexNumber);

            Mapa.Circles.Add(new Circle()
            {
                FillColor = Color.FromHex("#1f3347").MultiplyAlpha(0.5),
                StrokeColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao),
                Center =imovel.position,
                StrokeWidth = 5,
                Radius = new Distance(300),
                
                
            });
        }


        private void SegControl_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case 0:
                    Mapa.MapType = MapType.Street;
                    break;
                case 1:
                    Mapa.MapType = MapType.Satellite;
                    break;

            }
        }
    }
}