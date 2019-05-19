using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Imobiliaria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage :TabbedPage
    {
        private ContentPage mainPage;
        private ContentPage info;
        private ContentPage contact;

        public MainPage()
        {
            InitializeComponent();
            Children.Add(mainPage = new ItemsPage());

        }
    }
}



