using Imobiliaria.Models;
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
	public partial class ViewPin : StackLayout
    {
        private Imovel Imovel;

        public ViewPin(Imovel Imovel)
        {
            InitializeComponent();
            this.Imovel = Imovel;
            BindingContext = this.Imovel;
        }


    }

}