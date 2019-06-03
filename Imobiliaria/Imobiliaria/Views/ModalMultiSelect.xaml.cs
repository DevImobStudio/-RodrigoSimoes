using Imobiliaria.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
	public partial class ModalMultiSelect : PopupPage
    {
        string tipo { get; set; }
        public List<Opcao> opcao { get; set; }
        Pesquisa pesquisa { get; set; }
		public ModalMultiSelect (List<Opcao> Opcoes, string titulo, Pesquisa pesquisa)
		{
			InitializeComponent ();
            this.opcao = Opcoes;
            this.pesquisa = pesquisa;
            this.ItemsListView.HeightRequest = this.opcao.Count * 40;
            this.ItemsListView.ItemsSource = this.opcao;
            Titulo.Text = titulo;

        }

        private async void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (this.opcao.FindAll(p => p.check).Count > 0)
            {
                this.pesquisa.SetarBairro(this.opcao);
            }
            this.pesquisa.AtualizarPesquisa();
            await Navigation.PopPopupAsync();
        }
    }
}