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
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

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