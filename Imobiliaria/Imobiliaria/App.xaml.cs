using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Imobiliaria.Views;
using Imobiliaria.Views.Menu;
using Imobiliaria.Models;
using System.Collections.Generic;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Imobiliaria
{
    public partial class App : Application
    {
        MasterDetailPage1 page { get; set; }
        public App()
        {
            InitializeComponent();

            Services.Sistema.RESTAPI = new Services.Rest("http://api.rodrigosimoesimoveis.com.br/");
            GerarConfiguracao();
            page = new MasterDetailPage1();
            MainPage = page; //NavigationPage(new TabbedPage1()) ;// ;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        async void GerarConfiguracao()
        {
            List<Configuracao> l = await Services.Sistema.RESTAPI.getAsync<List<Configuracao>>("config/");
            if (l != null)
            {
                if (l.Count > 0)
                {
                    Services.Sistema.CONFIG = l[0];
                    App.Current.Resources["colorPrimary"] = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
                    App.Current.Resources["NavigationPrimary"] = Color.FromHex(Services.Sistema.CONFIG.cor_texto);
                    App.Current.Resources["colorPrimaryDark"] = Color.FromHex(Services.Sistema.CONFIG.cor_texto);
                    App.Current.Resources["CorTexto"] = Color.FromHex(Services.Sistema.CONFIG.cor_texto);
                    App.Current.Resources["BarBackgroundColor"] = Color.FromHex(Services.Sistema.CONFIG.cor_texto);
                    App.Current.Resources["navbarcolor"] = Color.FromHex(Services.Sistema.CONFIG.cor_texto);
                  
                   


                }

            }
            

        }
    }
}
