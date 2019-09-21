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
        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Videc.IsVisible = false;
            StackVideo.IsVisible = false;
            caroussel.IsVisible = true;
            box1.IsVisible = false;
            box2.IsVisible = false;
            caroussel.SelectedIndex = 0;
            Videc.Reload();

        }

        private void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {
            Videc.IsVisible = false;
            StackVideo.IsVisible = false;
            caroussel.IsVisible = true;
            

            caroussel.SelectedIndex = 0;
            Videc.Reload();
        }

        private void Caroussel_ItemAppearing(PanCardView.CardsView view, PanCardView.EventArgs.ItemAppearingEventArgs args)
        {

            if (this.ImovelDetail.Imovel.video != null)
            {
                if (this.ImovelDetail.Imovel.video != "")
                {
                    if (view.SelectedItem.ToString().Contains("youtube"))
                    {
                        Videc.Source = view.SelectedItem.ToString();
                        caroussel.IsVisible = false;
                        StackVideo.IsVisible = true;

                        Videc.IsVisible = true;
                        
                        box1.IsVisible = true;
                        box2.IsVisible = true;
                        ForceLayout();



                    }
                     //VideoC.isVisible = true;
                    // ImagemC.isVisible = false;
                }
            }
            else
            {
               // VideoC.isVisible = false;
              //  ImagemC.isVisible = true;
            }

        }

    }











}