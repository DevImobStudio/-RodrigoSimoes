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
	public partial class Atendimento :MenuContainerPage
    {

        public Atendimento ()
		{
			InitializeComponent ();
            this.SlideMenu = Services.Sistema.menuSuperior;
            Bind();
        }

        public void Bind()
        {
            if (Services.Sistema.CONFIG != null)
            {
                InitializeComponent();
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
    }
}