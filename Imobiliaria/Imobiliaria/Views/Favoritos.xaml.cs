using Imobiliaria.Models;
using Imobiliaria.Services;
using Imobiliaria.ViewModels;
using Plugin.Toast;
using SlideOverKit;
using System;
using System.Collections.Generic;
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
            try
            {
                List<Models.Favoritos> favoritos = await Services.Sistema.DATABASE.database.Table<Models.Favoritos>().ToListAsync();
                List<Models.Imovel> imovels = await Services.Sistema.RESTAPI.getAsync<List<Models.Imovel>>("");
                List<Models.Imovel> imovelsFavoritos = new List<Models.Imovel>();
             
                foreach(var i in favoritos)
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

                if (imovelsFavoritos.Count < 1)
                {
                    var Configuracao = new Configuracao();
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
            catch(Exception on)
            {

            }
           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            LoadFavoritos();



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

        public void Bind()
        {
            InitializeComponent();
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
                var favorito = await Sistema.DATABASE.database.Table<Models.Favoritos>().Where(p => p.idImovel == objeto.id).FirstOrDefaultAsync();
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
    }
}