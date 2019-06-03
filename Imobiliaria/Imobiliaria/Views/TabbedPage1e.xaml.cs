using Imobiliaria.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        public Inicio Inicio;
        private ContentPage Entrar;
        private ContentPage Favoritos;
        private ContentPage Atendimento;

        public Image logo;

        public TabbedPage1 ()
        {
            InitializeComponent();
   
            Children.Add(Inicio = new Inicio());
            Children.Add(Favoritos = new Favoritos());
            Children.Add(Atendimento = new Atendimento());
            Children.Add(Entrar = new Entrar());
            logo = logotipo;
        //    logo.Source = new UriImageSource { CachingEnabled = false, Uri = new Uri("https://www.rodrigosimoesimoveis.com.br/uploads/www.rodrigosimoesimoveis.com.br/logotipo.png")};
            
            ForceLayout();
        }
       


    }
}