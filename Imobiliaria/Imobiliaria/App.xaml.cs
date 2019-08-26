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
using Xamarin.Essentials;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Imobiliaria
{
    public partial class App 
    {
        TabbedPage1 page { get; set; }
        NavigationPage npage { get; set; }
       

        public App()
        {

            App.Current.Resources["colorPrimaryTransparente"] = Color.FromHex("#1E3246").MultiplyAlpha(0.5);
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                MainPage = new  Erro("Para usar este aplicativo é necessário estar conectado a internet.");
            //    System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {

                Services.Sistema.RESTAPI = new Services.Rest("http://www.api.rodrigosimoesimoveis.com.br/");
    
                Services.Sistema.DATABASE = new Services.DataBaseAsync();
                Dados();
               

                try
                {
                    GerarConfiguracao();
                }
                catch (Exception ex)
                {
                    MainPage = new Erro("Não foi possível acessar os dados!");
                    return;
                }
                InitializeComponent();



                // page = new MasterDetailPage1();
                page = new TabbedPage1();
                Services.Sistema.TABBEDPAGE = page;
                
                npage = new NavigationPage(page);
                
                OAuthConfig._NavigationPage = npage;
                OAuthConfig._TabbedPage = page;
    
                MainPage = npage;//page; //NavigationPage(new TabbedPage1()) ;// ;

               



            }
           

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
            Sistema.USUARIO = await Services.Sistema.DATABASE.database.Table<Models.Usuario>().Where(p=> p.logado).FirstOrDefaultAsync();

            if (l != null)
            {
                if (l.Count > 0)
                {
                    Services.Sistema.CONFIG = l[0];
                    App.Current.Resources["colorPrimary"] = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
                    App.Current.Resources["colorPrimaryTransparente"] = Color.FromHex(Services.Sistema.CONFIG.cor_padrao).MultiplyAlpha(0.5);
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
                        Services.Sistema.menuSuperior.Bind();
                       // page.Inicio.Bind();
                        page.Atendimento.Bind();
                        page.Favoritos.Bind();
                        page.Entrar.Bind();
                        if (page.logo != null)
                        {
                            page.logo.Source = new UriImageSource { CachingEnabled = false, Uri = new Uri(Services.Sistema.CONFIG.logotipo) };
                            page.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom).SetBarItemColor(Color.FromHex(Services.Sistema.CONFIG.cor_padrao));
                            page.ForceLayout();
                            if (page.Inicio != null)
                            {
                                page.Inicio.Bind();
                                page.Inicio.setarCor();
                                page.Inicio.ForceLayout();
                                
                            }

                        }
                    }




                }
                Services.Sistema.menuSuperior.Bind();

            }
            

        }
    }
}
