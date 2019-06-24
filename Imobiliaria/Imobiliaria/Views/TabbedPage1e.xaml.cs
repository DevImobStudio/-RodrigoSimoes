using Imobiliaria.Services;
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
        public Image logo;
        bool show = false;

        public TabbedPage1()
        {
            InitializeComponent();
            menuContainerPage = new MenuContainerPage();
            Services.Sistema.menuSuperior = new MenuSuperior();
            this.menuContainerPage.SlideMenu = Services.Sistema.menuSuperior;
            Children.Add(Inicio = new Inicio());
            Children.Add(Favoritos = new Favoritos());
            Children.Add(Atendimento = new Atendimento());
            Children.Add(Entrar = new Entrar());

            logo = logotipo;
            
            //    logo.Source = new UriImageSource { CachingEnabled = false, Uri = new Uri("https://www.rodrigosimoesimoveis.com.br/uploads/www.rodrigosimoesimoveis.com.br/logotipo.png")};

            ForceLayout();
            this.CurrentPageChanged += (object sender, EventArgs e) => {

                var i = this.Children.IndexOf(this.CurrentPage);
                switch (i)
                {
                    case 0:
                        this.Inicio.carregarPaginaInicial();
                        break;
                    case 1:
                        this.Favoritos.Bind();
                        break;
                    case 2:
                        this.Atendimento.Bind();
                        break;
                    case 3:
                        this.Entrar.Bind();
                        break;
                }

            };

        }

        public void Bind()
        {
            InitializeComponent();
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
