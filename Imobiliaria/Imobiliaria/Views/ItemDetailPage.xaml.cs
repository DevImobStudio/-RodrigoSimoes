using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Imobiliaria.Models;
using Imobiliaria.ViewModels;
using Imobiliaria.Services;
using Plugin.Toast;

namespace Imobiliaria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : StackLayout
    {
        ItemDetailViewModel viewModel { get; set; }
        Inicio inicio { get; set; }
        Favoritos favoritos { get; set; }

        public ItemDetailPage(Inicio inicio , ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            this.inicio = inicio;
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
            caroussel.ItemsSource = this.viewModel.Imagens;
            viewModel.LoadItemsCommand.Execute(null);
            CarregarDados();
           
        }

        public ItemDetailPage(Favoritos favoritos, ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            this.favoritos = favoritos;
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
            caroussel.ItemsSource = this.viewModel.Imagens;
            viewModel.LoadItemsCommand.Execute(null);
            CarregarDados();

        }
        public async void CarregarDados()
        {
            viewModel.LoadItemsCommand.Execute(null);
        }

        private void BtnWhatsapp_Clicked(object sender, EventArgs e)
        {
            Sistema.WhatsApp(this.viewModel.Imovel);
        }

        private async void BtnFavoritos_Clicked(object sender, EventArgs e)
        {
            if (Sistema.USUARIO != null) {
                if (this.viewModel.Imovel != null)
                {

                    var favorito = await Sistema.DATABASE.database.Table<Models.Favoritos>().Where(p => p.idImovel == this.viewModel.Imovel.id && p.idUsuario == Sistema.USUARIO.cod).FirstOrDefaultAsync();
                    if (favorito == null)
                    {
                        try
                        {
                            await Sistema.DATABASE.database.InsertAsync(new Models.Favoritos()
                            {
                                idUsuario = Sistema.USUARIO.cod,
                                idImovel = this.viewModel.Imovel.id

                            });

                            CrossToastPopUp.Current.ShowToastMessage("Imovel " + this.viewModel.Imovel.titulo + " adicionado com sucesso", Plugin.Toast.Abstractions.ToastLength.Long);
                        }
                        catch (Exception ex)
                        {
                            CrossToastPopUp.Current.ShowToastMessage(ex.Message, Plugin.Toast.Abstractions.ToastLength.Long);
                        }

                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastMessage("Imovel " + this.viewModel.Imovel.titulo + "já pertence ao seus favoritos", Plugin.Toast.Abstractions.ToastLength.Long);
                    }
                }
               

            }
            else
            {
                OAuthConfig.IndexPage = 0;
                CrossToastPopUp.Current.ShowToastMessage("Faça o login primeiro para adicionar o imóvel ao seus favoritos");
                Sistema.TABBEDPAGE.CurrentPage = Sistema.TABBEDPAGE.Entrar;

            }
        }
        private void BtnTelefone_Clicked(object sender, EventArgs e)
        {
            Sistema.Contato();
        }

        private async void EnvioMaterial_Clicked(object sender, EventArgs e)
        {
             this.inicio.CarregarPaginaEnvioMaterial(this.viewModel.Imovel);

        }

        private void Map_Clicked(object sender, EventArgs e)
        {
           Navigation.PushAsync(new DetalheMaps(this.viewModel.Imovel));
        }


        /*  protected async override void OnAppearing()
          {
              base.OnAppearing();

              viewModel.LoadItemsCommand.Execute(null);

          }
          */





    }
}