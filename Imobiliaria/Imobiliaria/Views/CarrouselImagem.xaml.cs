using Imobiliaria.Models;
using Imobiliaria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarrouselImagem : ContentPage
	{
        ItemDetailViewModel ImovelDetail { get; set; }
        public CarrouselImagem (ItemDetailViewModel imovel)
		{
			InitializeComponent ();
            ImovelDetail = imovel;
            BindingContext = this.ImovelDetail;
            if (this.ImovelDetail.Imagens != null)
            {
                caroussel.ItemsSource = this.ImovelDetail.Imagens;
            }

          
        }
	}
}