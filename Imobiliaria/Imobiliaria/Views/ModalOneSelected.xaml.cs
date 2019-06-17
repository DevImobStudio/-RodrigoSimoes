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
	public partial class ModalOneSelected : PopupPage
    {
        string tipo { get; set; }
        public List<string> opcao { get; set; }
        Pesquisa pesquisa { get; set; }


        public ModalOneSelected (List<string> Opcoes, string titulo, Pesquisa pesquisa, string tipo)
		{
            InitializeComponent();
            this.opcao = Opcoes;
            this.pesquisa = pesquisa;
            BindingContext = pesquisa;
            this.tipo = tipo;
            Lista.ItemsSource = this.opcao;
            Titulo.Text = titulo;
            if (tipo.Equals("CIDADE"))
            {
                Lista.SelectedItem = this.pesquisa.Inicio.viewModel.mPesquisa.cidade;
            }
            if (tipo.Equals("TIPO"))
            {
                Lista.SelectedItem = this.pesquisa.Inicio.viewModel.mPesquisa.categoria;
            }
            
        }

        private async void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string a = this.Lista.SelectedItem.ToString();
            if (a != null)
            {
                if (tipo.Equals("CIDADE"))
                {
                    this.pesquisa.Inicio.viewModel.mPesquisa.cidade = a;
                    

                }
                if (tipo.Equals("TIPO"))
                {
                     this.pesquisa.Inicio.viewModel.mPesquisa.categoria = a;
                }
            }
            this.pesquisa.AtualizarPesquisa();
            await Navigation.PopPopupAsync();

        }
    }
}