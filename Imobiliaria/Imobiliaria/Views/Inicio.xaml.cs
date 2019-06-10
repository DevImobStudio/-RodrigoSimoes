using Imobiliaria.Models;
using Imobiliaria.Services;
using Imobiliaria.ViewModels;
using Plugin.Geolocator;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inicio : MenuContainerPage
    {

        private Maps Maps { get; set; }
        private ListaImoveis ListaImoveis { get; set; }
        public ItemsViewModel viewModel { get; set; }
        public StackLayout paginaStack { get; set; }
        public MenuPesquisa menuPesquisa { get; set; }
        public EnvioMaterial EnvioMaterial { get; set; }
        public ItemDetailViewModel ImovelDetail{ get; set; }

        public Inicio ()
		{
            InitializeComponent();
           
            viewModel = new ItemsViewModel();
            menuPesquisa = new MenuPesquisa(this);
            this.SlideMenu = menuPesquisa;
            paginaStack = pagina;
            Maps = new Maps(this);
            ListaImoveis = new ListaImoveis(this);
           

            /*  if (PageSelected.SelectedSegment == 0)
              {
                  pagina.Children.Add(Maps = new Maps(this));
              }
              else
              {
                  pagina.Children.Add(ListaImoveis = new ListaImoveis(this));
              }
              */


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);
           // ForceLayout();



        }
        public void setarCor()
        {
            PageSelected.TintColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
            PageSelected.DisabledColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
            PageSelected.DisabledColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
            searchI.TextColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
            searchL.TextColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao);
        }
     /*   public void Handle_ValueChanged(object o, int e)
        {
            pagina.Children.Clear();

            switch (e)
            {
                case 0:
                    pagina.Children.Add(Maps = new Maps(this));
                    break;
                case 1:
                    pagina.Children.Add(ListaImoveis = new ListaImoveis(this));
                    break;
               
            }
        }
        */
        public void visiblePageSelected(bool set)
        {
            if (set)
            {
                PageSelected.TintColor = (Color)Application.Current.Resources["colorPrimary"];
                PageSelected.DisabledColor = (Color)Application.Current.Resources["colorPrimary"];
                PageSelected.DisabledColor = (Color)Application.Current.Resources["colorPrimary"];
                PageSelected.SelectedTextColor = (Color)Application.Current.Resources["colorClara"];
            }
            else
            {
                PageSelected.TintColor = (Color)Application.Current.Resources["colorSecundaria"];
                PageSelected.DisabledColor = (Color)Application.Current.Resources["colorSecundaria"];
                PageSelected.DisabledColor = (Color)Application.Current.Resources["colorSecundaria"];
                PageSelected.SelectedTextColor = (Color)Application.Current.Resources["colorSecundaria"];
            }


    }



        private void SegControl_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            pagina.Children.Clear();

            switch (e.NewValue)
            {
                case 0:
                    pagina.Children.Add(Maps);
                    Maps.ForceLayout();
                    break;
                case 1:
                    pagina.Children.Add(ListaImoveis);
                    break;

            }
        }

        protected  override bool OnBackButtonPressed()
        {
            carregarPaginaInicial();
            return true;
        }

        public void carregarPaginaInicial()
        {
            var b = pagina.Children[0];
            var c = b.GetType();
            var d = b.TabIndex;
            if ((b.GetType() != Maps.GetType()) && (b.GetType() != ListaImoveis.GetType()))
            {
                CarregarPagina();
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
       //     carregarPaginaInicial();
            this.Navigation.PushAsync(new Pesquisa(this));
        //   menuPesquisa.carregarDados();
         //  menuPesquisa.ForceLayout();
        //   this.ShowMenu();
          


        }

        public async Task CarregarDetalhes(Imovel i)
        {

            this.visiblePageSelected(false);
            pagina.Children.Clear();
            this.ImovelDetail = new ItemDetailViewModel(i);
            this.paginaStack.Children.Add(new ItemDetailPage(this, ImovelDetail));
            this.ImovelDetail.LoadItemsCommand.Execute(null);


        }
        public  void CarregarPagina()
        {

            pagina.Children.Clear();
            if (PageSelected.SelectedSegment == 0)
            {
               
                pagina.Children.Add(Maps);
                Maps.Bind();

            }
            else
            {
                pagina.Children.Add(ListaImoveis);
            }

            visiblePageSelected(true);

        }
        public void CarregarPesquisa()
        {
            CarregarPagina();
            viewModel.LoadItemsCommandBusca.Execute(null);
          
        }
       
        public void CarregarPaginaEnvioMaterial(Imovel imovel)
        {

            pagina.Children.Clear();
            
            pagina.Children.Add(new EnvioMaterial(imovel));

            visiblePageSelected(false);

        }


        public void CarregarMenu()
        {

            this.ShowMenu();
        }

    }
}