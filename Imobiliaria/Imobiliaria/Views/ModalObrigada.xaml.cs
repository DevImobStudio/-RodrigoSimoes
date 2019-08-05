using Imobiliaria.Services;
using Rg.Plugins.Popup.Pages;
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
	public partial class ModalObrigada : PopupPage
    {
        EnvioMaterial envioMaterial { get; set; }
        public ModalObrigada (EnvioMaterial envioMaterial)
		{
			InitializeComponent ();
            this.Mensagem.Text = "Obrigada " + Services.Sistema.USUARIO.name.ToUpper() + " !";
            this.envioMaterial = envioMaterial;

        }

        private void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
            Sistema.TABBEDPAGE.CurrentPage = Sistema.TABBEDPAGE.Inicio;
        }
    }
}