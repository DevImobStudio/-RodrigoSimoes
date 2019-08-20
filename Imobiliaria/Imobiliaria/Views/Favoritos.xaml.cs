using Imobiliaria.Models;
using Imobiliaria.Services;
using Imobiliaria.ViewModels;
using Plugin.Toast;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Favoritos : MenuContainerPage
    {
        Inicio inicio { get; set; }
        public MenuSuperior menuSuperior { get; set; }
        public ItemDetailViewModel ImovelDetail { get; set; }

        public Favoritos ()
		{
			InitializeComponent ();
            // LoadFavoritos();
            menuSuperior = new MenuSuperior();
            this.SlideMenu = menuSuperior;
            


        }
        private async void LoadFavoritos()
        {
            if  (pagina.Children.Count >= 1)
            {
                pagina.Children.Clear();
                InitializeComponent();
               
            }

            pagina.Children[0].IsVisible = true;
          
            try
            {
                Sistema.TABBEDPAGE.Inicio.viewModel.LoadItemsCommandFavoritos.Execute(null);
                /* {
                 List<Models.Favoritos> favoritos = new List<Models.Favoritos>();
                 if (Sistema.USUARIO != null)
                 {
                     favoritos = await Services.Sistema.DATABASE.database.Table<Models.Favoritos>().Where(p => p.idUsuario == Sistema.USUARIO.cod).ToListAsync();
                 }


                    List<Imovel>  imovels = Sistema.TABBEDPAGE.Inicio.viewModel.LstImoveis;
                     List<Models.Imovel> imovelsFavoritos = new List<Models.Imovel>();

                     foreach (var i in favoritos)
                     {
                         Models.Imovel imovel = imovels.Where(p => p.id == i.idImovel).FirstOrDefault();
                         if (imovel != null)
                         {
                             imovelsFavoritos.Add(imovel);
                         }
                         else
                         {
                             imovelsFavoritos.Add(new Models.Imovel()
                             {
                                 id = i.idImovel,
                                 titulo = "Indisponível"
                             });
                         }

                     }

                     ItemsListView.ItemsSource = imovelsFavoritos;
                     BindingContext = imovelsFavoritos;
             */

                //  ItemsListView.ItemsSource = Sistema.TABBEDPAGE.Inicio.viewModel.Favoritos;
                BindingContext = Sistema.TABBEDPAGE.Inicio.viewModel;
            if ( await Services.Sistema.DATABASE.database.Table<Models.Favoritos>().Where(p => p.idUsuario == Sistema.USUARIO.cod).CountAsync() < 1)
                    {
                        var LayoutVazio = new StackLayout();
                        var lblListaVazia = new Label
                        {
                            Text = "Nenhum imóvel favorito definido",
                            TextColor = Color.FromHex(Services.Sistema.CONFIG.cor_padrao),
                            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        };
                        LayoutVazio.Children.Add(lblListaVazia);
                        Content = LayoutVazio;
                    }
                }
                catch (Exception on)
                {

                }
          
           
           
           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            LoadFavoritos();

            Sistema.TABBEDPAGE.Inicio.viewModel.LoadItemsCommandFavoritos.Execute(null);

        }

        private void Detalhes_Clicked(object sender, EventArgs e)
        {

            var objeto = ((sender as Button).CommandParameter) as Imovel;
            if (objeto != null)
            {
                pagina.Children.Clear();
                this.ImovelDetail = new ItemDetailViewModel(objeto);
                this.pagina.Children.Add(new ItemDetailPage(this, ImovelDetail));
                this.ImovelDetail.LoadItemsCommand.Execute(null);
            }

        }

        private void ItemsListView_Refreshing(object sender, EventArgs e)
        {
            BindingContext = Sistema.TABBEDPAGE.Inicio.viewModel;
        }

        public void Bind()
        {
            LoadFavoritos();
            menuSuperior.Bind();
            Sistema.TABBEDPAGE.Inicio.viewModel.LoadItemsCommandFavoritos.Execute(null);
            BindingContext = Sistema.TABBEDPAGE.Inicio.viewModel;
        }


        private void WhatsApp_Clicked(object sender, EventArgs e)
        {
            try
            {
                var objeto = ((sender as Button).CommandParameter) as Imovel;
                if (objeto != null)
                {
                    Sistema.WhatsApp(objeto);
                }
            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastMessage(ex.Message, Plugin.Toast.Abstractions.ToastLength.Long);
            }
        }

        private void Contato_Clicked(object sender, EventArgs e)
        {
            Sistema.Contato();
        }

        private async void Remove_Clicked(object sender, EventArgs e)
        {
            var objeto = ((sender as Button).CommandParameter) as Imovel;
            if (objeto != null)
            {
                var favorito = await Sistema.DATABASE.database.Table<Models.Favoritos>().Where(p => p.idImovel == objeto.id && Sistema.USUARIO.cod == p.idUsuario).FirstOrDefaultAsync();
                if (favorito != null)
                {
                    try
                    {
                        await Sistema.DATABASE.database.DeleteAsync<Models.Favoritos>(favorito.id);
                        Bind();

                        CrossToastPopUp.Current.ShowToastMessage("Imóvel " + objeto.titulo + " removido com sucesso", Plugin.Toast.Abstractions.ToastLength.Long);
                    }
                    catch (Exception ex)
                    {
                        CrossToastPopUp.Current.ShowToastMessage(ex.Message, Plugin.Toast.Abstractions.ToastLength.Long);
                    }

                }
                else
                {
                    CrossToastPopUp.Current.ShowToastMessage("Imovel " + objeto.titulo + " não encontrado nos seus favoritos", Plugin.Toast.Abstractions.ToastLength.Long);
                }
            }
        }


        public void CarregarPaginaEnvioMaterial(Imovel imovel)
        {

            pagina.Children.Clear();

            pagina.Children.Add(new EnvioMaterial(imovel));


        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Imovel objeto = new Imovel();
            objeto = ((TappedEventArgs)e).Parameter as Imovel;

            
            if (objeto != null)
            {
                //   pagina.Children.Clear();
                pagina.Children[0].IsVisible = false;
                this.ImovelDetail = new ItemDetailViewModel(objeto);
                this.pagina.Children.Add(new ItemDetailPage(this, ImovelDetail));
                this.ImovelDetail.LoadItemsCommand.Execute(null);
            }
        }
    }
}