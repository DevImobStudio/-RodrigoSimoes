using Imobiliaria.Services;
using Imobiliaria.ViewModels;
using Plugin.Toast;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        public Inicio Inicio;
        public Entrar Entrar;
        public Favoritos Favoritos;
        public Atendimento Atendimento;
        public MenuContainerPage menuContainerPage { get; set; }
        public ItemsViewModel viewModel { get; set; }
        public Image logo;
        bool show = false;

        public TabbedPage1()
        {
            InitializeComponent();
            viewModel = new ItemsViewModel();
            viewModel.LoadItemsCommand.Execute(null);
            menuContainerPage = new MenuContainerPage();
            Services.Sistema.menuSuperior = new MenuSuperior();
            this.menuContainerPage.SlideMenu = Services.Sistema.menuSuperior;
            Children.Add(Inicio = new Inicio(viewModel));
            Children.Add(Favoritos = new Favoritos());
            Children.Add(Atendimento = new Atendimento());
            Children.Add(Entrar = new Entrar());

            logo = logotipo;
            
            //    logo.Source = new UriImageSource { CachingEnabled = false, Uri = new Uri("https://www.rodrigosimoesimoveis.com.br/uploads/www.rodrigosimoesimoveis.com.br/logotipo.png")};

            ForceLayout();
            this.CurrentPageChanged += async (object sender, EventArgs e) => {

                var i = this.Children.IndexOf(this.CurrentPage);
                TrocarDePagina(i);

            };

           


        }

        public async void  TrocarDePagina(int page)
        {
            switch (page)
            {
                case 0:
                    this.Inicio.carregarPaginaInicial();
                    break;
                case 1:
                    Sistema.USUARIO = await Services.Sistema.DATABASE.database.Table<Models.Usuario>().Where(p => p.logado).FirstOrDefaultAsync();
                    if (Sistema.USUARIO != null)
                    {
                        this.Favoritos.Bind();
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastMessage("Faça o login primeiro para ver seus imóveis no favoritos");
                        OAuthConfig.IndexPage = 1;
                        Sistema.TABBEDPAGE.CurrentPage = Sistema.TABBEDPAGE.Entrar;
                    }
                    break;
                case 2:
                    this.Atendimento.Bind();
                    break;
                case 3:
                    this.Entrar.Bind();
                    this.Inicio.viewModel.LoadItemsCommand.Execute(null);
                    break;
            }
        }
       

        public void Bind()
        {
            InitializeComponent();
        }

        private void ButtonContentPage_OnClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Teste");
        }


        public  async void trocarPagina()
        {
            switch (OAuthConfig.IndexPage)
            {
                case 0:
                    Sistema.TABBEDPAGE.CurrentPage = Sistema.TABBEDPAGE.Inicio;
                    this.Inicio.carregarPaginaInicial();
                    break;
                case 1:
                    if (Sistema.USUARIO == null)
                    {
                        Sistema.USUARIO = await Services.Sistema.DATABASE.database.Table<Models.Usuario>().Where(p => p.logado).FirstOrDefaultAsync();
                    }
                    
                    if (Sistema.USUARIO != null)
                    {
                        this.Favoritos.Bind();
                        Sistema.TABBEDPAGE.CurrentPage = Sistema.TABBEDPAGE.Favoritos;
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastMessage("Faça o login primeiro para ver seus imóveis no favoritos");
                        OAuthConfig.IndexPage = 1;
                        Sistema.TABBEDPAGE.CurrentPage = Sistema.TABBEDPAGE.Entrar;
                    }
                    break;
                case 2:
                    this.Atendimento.Bind();
                    Sistema.TABBEDPAGE.CurrentPage = Sistema.TABBEDPAGE.Atendimento;
                    break;
                case 3:
                    this.Entrar.Bind();
                    Sistema.TABBEDPAGE.CurrentPage = Sistema.TABBEDPAGE.Entrar;
                    this.Inicio.viewModel.LoadItemsCommand.Execute(null);
                    break;
            }
        }



        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
           
            this.menuContainerPage = this.CurrentPage as MenuContainerPage;
            if (show)
            {
                this.menuContainerPage.HideMenu();
                this.show = false;
            }
            else
            {
                this.menuContainerPage.ShowMenu();
                this.show = true;
                ForceLayout();
            }
            
           
        }

        protected async override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            
            this.menuContainerPage.HideMenu();
            this.show = false;
        }


        
        



    }
}
