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
    public partial class Informacoes : TabbedPage
    {
        public Informacoes()
        {
            InitializeComponent();
            this.BindingContext = Services.Sistema.CONFIG;

            logotipo.Source = new UriImageSource { CachingEnabled = false, Uri = new Uri(Services.Sistema.CONFIG.logotipo) };
        }
    }
}