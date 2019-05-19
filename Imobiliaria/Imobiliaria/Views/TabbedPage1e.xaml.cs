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
        private ContentPage Inicio;
        private ContentPage Entrar;
        private ContentPage Favoritos;
        private ContentPage Atendimento;

        public TabbedPage1 ()
        {
            InitializeComponent();
            
            Children.Add(Inicio = new Inicio());
            Children.Add(Favoritos = new Favoritos());
            Children.Add(Atendimento = new Atendimento());
            Children.Add(Entrar = new Entrar());
          
        }

       
    }
}