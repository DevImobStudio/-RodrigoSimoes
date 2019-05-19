using SlideOverKit;
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
	public partial class MenuSuperior : SlideMenuView
    {
		public MenuSuperior ()
		{
			InitializeComponent ();

            this.HeightRequest = 250;

            this.IsFullScreen = true;
            this.MenuOrientations = MenuOrientation.TopToBottom;

            this.BackgroundColor = Color.Black;
            this.BackgroundViewColor = Color.Transparent;

            if (Device.RuntimePlatform == Device.Android)
                this.HeightRequest += 50;
        }
	}
}