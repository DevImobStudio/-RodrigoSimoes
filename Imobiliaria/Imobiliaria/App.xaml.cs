using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Imobiliaria.Views.Menu;
using Imobiliaria.Models;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Imobiliaria
{
    public partial class App 
    {
        MasterDetailPage1 page { get; set; }
       
        public App()
        {
            InitializeComponent();

            Services.Sistema.RESTAPI = new Services.Rest("http://api.rodrigosimoesimoveis.com.br/");

            Services.Sistema.DATABASE = new Services.DataBaseAsync();

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
                    if (page != null)
                    {
                        page.navi.BarBackgroundColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
                        page.ForceLayout();
                    }
                    if (page.pagina != null)
                    {
                        if (page.pagina.logo != null)
                        {
                            page.pagina.logo.Source = new UriImageSource { CachingEnabled = false, Uri = new Uri(Services.Sistema.CONFIG.logotipo) };
                            page.pagina.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom).SetBarItemColor(Color.FromHex(Services.Sistema.CONFIG.cor_padrao));
                            page.pagina.ForceLayout();
                            if (page.pagina.Inicio != null)
                            {
                                page.pagina.Inicio.setarCor();
                                page.pagina.Inicio.ForceLayout();
                            }
                           
                            
                        }


                        
                    }

                      
                 
                    


                }

            }
            

        }
    }
}
