using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Imobiliaria.Models;
using Imobiliaria.ViewModels;
using Imobiliaria.Services;
using Plugin.Toast;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.PlatformConfiguration;
using Xam.Forms.VideoPlayer;

namespace Imobiliaria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : StackLayout
    {
        ItemDetailViewModel ImovelDetail { get; set; }
        Inicio inicio { get; set; }
        Favoritos favoritos { get; set; }
        WebView webView { get; set; }

        public ItemDetailPage(Inicio inicio , ItemDetailViewModel ImovelDetail)
        {
            InitializeComponent();
           
            this.inicio = inicio;
            this.ImovelDetail = ImovelDetail;
            this.ImovelDetail.LoadItemsCommand.Execute(null);
            BindingContext = this.ImovelDetail;
            webView = Videc;
            if (this.ImovelDetail.Imagens != null)
            {
                caroussel.ItemsSource = this.ImovelDetail.Imagens;
            }
          

            //    CarregarDados();

        }

        public ItemDetailPage(Favoritos favoritos, ItemDetailViewModel ImovelDetail)
        {
            InitializeComponent();
            this.favoritos = favoritos;
            this.ImovelDetail = ImovelDetail;
            BindingContext = this.ImovelDetail;
            caroussel.ItemsSource = this.ImovelDetail.Imagens;
            this.ImovelDetail.LoadItemsCommand.Execute(null);
       //     CarregarDados();

        }
        public async void CarregarDados()
        {
            this.ImovelDetail.LoadItemsCommand.Execute(null);
        }

        private void BtnWhatsapp_Clicked(object sender, EventArgs e)
        {
            Sistema.WhatsApp(this.ImovelDetail.Imovel);
        }

        private async void BtnFavoritos_Clicked(object sender, EventArgs e)
        {

            if (Sistema.USUARIO != null) {
                if (this.ImovelDetail.Imovel != null)
                {

                    var favorito = await Sistema.DATABASE.database.Table<Models.Favoritos>().Where(p => p.idImovel == this.ImovelDetail.Imovel.id && p.idUsuario == Sistema.USUARIO.cod).FirstOrDefaultAsync();
                    if (favorito == null)
                    {
                        try
                        {
                            await Sistema.DATABASE.database.InsertAsync(new Models.Favoritos()
                            {
                                idUsuario = Sistema.USUARIO.cod,
                                idImovel = this.ImovelDetail.Imovel.id

                            });

                            CrossToastPopUp.Current.ShowToastMessage("Imovel " + this.ImovelDetail.Imovel.titulo + " adicionado com sucesso", Plugin.Toast.Abstractions.ToastLength.Long);
                        }
                        catch (Exception ex)
                        {
                            CrossToastPopUp.Current.ShowToastMessage(ex.Message, Plugin.Toast.Abstractions.ToastLength.Long);
                        }

                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastMessage("Imovel " + this.ImovelDetail.Imovel.titulo + " já pertence ao seus favoritos", Plugin.Toast.Abstractions.ToastLength.Long);
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
            if (this.favoritos != null)
            {
             
                this.favoritos.CarregarPaginaEnvioMaterial(this.ImovelDetail.Imovel);
            }
            else
            {
               
                this.inicio.CarregarPaginaEnvioMaterial(this.ImovelDetail.Imovel);
            }
                

           
             

        }

        private void Map_Clicked(object sender, EventArgs e)
        {
           Navigation.PushAsync(new DetalheMaps(this.ImovelDetail.Imovel));
        }

        private void CarrouselImagem_Clicked(object sender, EventArgs e)
        {
            Page p = new CarrouselImagem(this.ImovelDetail);
           
            Navigation.PushModalAsync(new CarrouselImagem(this.ImovelDetail));
        }

        private void Caroussel_ItemSwiped(PanCardView.CardsView view, PanCardView.EventArgs.ItemSwipedEventArgs args)
        {
        /*    if (this.ImovelDetail.Imovel.video != null)
            {
                if (this.ImovelDetail.Imovel.video != "")
                {
                    if (view.SelectedItem.ToString().Contains("youtube"))
                    {
                        Videc.Source = view.SelectedItem.ToString();
                        caroussel.IsVisible = false;


                        Videc.IsVisible = true;
                        box1.IsVisible = true;
                        box2.IsVisible = true;




                    }
                  //  VideoC.isVisible = true;
                   // ImagemC.isVisible = false;
                }
            }
            else
            {
                //VideoC.isVisible = false;
                //ImagemC.isVisible = true;
            }*/
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Videc.IsVisible = false;
            caroussel.IsVisible = true;
            box1.IsVisible = false;
            box2.IsVisible = false;
            caroussel.SelectedIndex = 0;
            Videc.Reload();

        }

        private void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {
            Videc.IsVisible = false;
            caroussel.IsVisible = true;

            caroussel.SelectedIndex = 0;
            Videc.Reload();
        }

        private void Caroussel_ItemAppearing(PanCardView.CardsView view, PanCardView.EventArgs.ItemAppearingEventArgs args)
        {

            if (this.ImovelDetail.Imovel.video != null)
            {
                if (this.ImovelDetail.Imovel.video != "")
                {
                    if (view.SelectedItem.ToString().Contains("youtube"))
                    {
                        Videc.Source = view.SelectedItem.ToString();
                        caroussel.IsVisible = false;


                        Videc.IsVisible = true;
                        box1.IsVisible = true;
                        box2.IsVisible = true;




                    }
                    //  VideoC.isVisible = true;
                    // ImagemC.isVisible = false;
                }
            }
            else
            {
                //VideoC.isVisible = false;
                //ImagemC.isVisible = true;
            }

        }

        private void StackLayout_Unfocused(object sender, FocusEventArgs e)
        {
            Videc.Reload();
        }







        /*  protected async override void OnAppearing()
          {
              base.OnAppearing();

              viewModel.LoadItemsCommand.Execute(null);

          }
          */





    }
}