using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Imobiliaria.Models;
using Imobiliaria.ViewModels;

namespace Imobiliaria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : StackLayout
    {
        ItemDetailViewModel viewModel { get; set; }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
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


            /*  protected async override void OnAppearing()
              {
                  base.OnAppearing();

                  viewModel.LoadItemsCommand.Execute(null);

              }
              */



        }
}