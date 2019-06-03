using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Imobiliaria.Views.Menu;
using Imobiliaria.Models;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Imobiliaria.Views;
using System.Threading.Tasks;
using Imobiliaria.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Imobiliaria
{
    public partial class App 
    {
        TabbedPage1 page { get; set; }
        NavigationPage npage { get; set; }

        public App()
        {
            InitializeComponent();

            Services.Sistema.RESTAPI = new Services.Rest("http://www.api.rodrigosimoesimoveis.com.br/");

            Services.Sistema.DATABASE = new Services.DataBaseAsync();
            Dados();
            GerarConfiguracao();
            // page = new MasterDetailPage1();
            page = new TabbedPage1();
            Services.Sistema.TABBEDPAGE = page;
            npage = new NavigationPage(page);
            MainPage = npage;//page; //NavigationPage(new TabbedPage1()) ;// ;

          


        }
        public async void Dados()
        {
            Task dados = new DataBase().CriarTabelaConexao();

            await dados;
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
                    App.Current.Resources["colorAccent"] = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
                    App.Current.Resources["NavigationPrimary"] = Color.FromHex(Services.Sistema.CONFIG.cor_texto);
                    App.Current.Resources["colorPrimaryDark"] = Color.FromHex(Services.Sistema.CONFIG.cor_texto);
                    App.Current.Resources["CorTexto"] = Color.FromHex(Services.Sistema.CONFIG.cor_texto);
                    App.Current.Resources["BarBackgroundColor"] = Color.FromHex(Services.Sistema.CONFIG.cor_texto);
                    App.Current.Resources["navbarcolor"] = Color.FromHex(Services.Sistema.CONFIG.cor_texto);
                    /*  if (page != null)
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
                      */
                    
                    if (page != null)
                    {
                        npage.BarBackgroundColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);

                        if (page.logo != null)
                        {
                            page.logo.Source = new UriImageSource { CachingEnabled = false, Uri = new Uri(Services.Sistema.CONFIG.logotipo) };
                            page.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom).SetBarItemColor(Color.FromHex(Services.Sistema.CONFIG.cor_padrao));
                            page.ForceLayout();
                            if (page.Inicio != null)
                            {
                                page.Inicio.setarCor();
                                page.Inicio.ForceLayout();
                            }

                        }
                    }




                }

            }
            

        }
    }
}
