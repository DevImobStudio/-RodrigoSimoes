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
    public partial class ProviderLoginPage : ContentPage
    {
        public Label label { get; set; }
        public string ProviderName
        {
            get;
            set;
        }
        public ProviderLoginPage(string _providername)
        {
            InitializeComponent();
            ProviderName = _providername;
        }


    }
}