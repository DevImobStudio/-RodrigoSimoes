using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuSuperior : SlideMenuView
    {
		public MenuSuperior ()
		{
			InitializeComponent ();

            this.HeightRequest = DeviceDisplay.MainDisplayInfo.Height /6 ;

            this.IsFullScreen = true;
            this.MenuOrientations = MenuOrientation.TopToBottom;

            this.BackgroundViewColor = Color.Transparent;

            if (Device.RuntimePlatform == Device.Android)
                this.HeightRequest += 50;
        }


       
    }
}