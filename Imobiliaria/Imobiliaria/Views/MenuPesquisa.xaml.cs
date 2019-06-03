using Imobiliaria.Models;
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
	public partial class MenuPesquisa : SlideMenuView
    {
        Inicio Inicio { get; set; }
        List<string> cidades { get; set; }
        List<string> tipos { get; set; }
        List<string> bairros { get; set; }
        List<string> dormitorios { get; set; }

        public MenuPesquisa (Inicio inicio)
		{
			InitializeComponent ();
            this.IsFullScreen = true;
            this.WidthRequest = 250;
            this.MenuOrientations = MenuOrientation.LeftToRight;
            this.Inicio = inicio;
            BindingContext = this.Inicio.viewModel;

            cidades = new List<string>();
            tipos = new List<string>();
            bairros = new List<string>();
            dormitorios = new List<string>();

            this.BackgroundViewColor = Color.Transparent;
            carregarDados();
        }

        public  void carregarDados()
        {
            this.ForceLayout();
            carregarDadosCidade();
            carregarDadosTipo();
            carregarDadosDormitorios();
            if (Cidade.SelectedItem == null)
            {
                Bairro.IsEnabled = false;
            }

            BindingContext = this.Inicio.viewModel;
        }
        public void carregarDadosCidade()
        {
            cidades.Clear();
            if (cidades.Count < 1)
            {
                cidades.Add("Todas");
            }
            foreach (var i in Inicio.viewModel.LstImoveis)
            {
                cidades.Add(i.cidade);
            }

            cidades = cidades.Distinct().ToList();
            Cidade.ItemsSource = cidades;
        }

        public void carregarDadosTipo()
        {
            tipos.Clear();
            if (tipos.Count < 1)
            {
                tipos.Add("Todos");
            }
            foreach (var i in Inicio.viewModel.LstImoveis)
            {
                tipos.Add(i.tipo);
            }

            tipos = tipos.Distinct().ToList();
            Tipo.ItemsSource = tipos;
        }

        public void carregarDadosDormitorios()
        {
            dormitorios.Clear();
            if (dormitorios.Count < 1)
            {
                dormitorios.Add("Indiferente");
            }
            foreach (var i in Inicio.viewModel.LstImoveis)
            {
                dormitorios.Add(i.dormitorios);
            }

            dormitorios = dormitorios.Distinct().ToList();
            Dormitorios.ItemsSource = dormitorios;
        }

        public void carregarDadosBairro(string cidade)
        {
            bairros.Clear();
            if (bairros.Count < 1)
            {
                bairros.Add("Todos");
            }
            if (cidade != "Todas")
            {
                foreach (var i in Inicio.viewModel.LstImoveis.Where(p => p.cidade == cidade).ToList())
                {
                    bairros.Add(i.bairro);
                }
            }
            else
            {
                foreach (var i in Inicio.viewModel.LstImoveis)
                {
                    bairros.Add(i.bairro);
                }
            }
           

            bairros = bairros.Distinct().ToList();
            Bairro.ItemsSource = bairros;
        }

        private void Cidade_SelectedItemChanged(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            string a = e.NewItem as string;
            Bairro.IsEnabled = true;
            carregarDadosBairro(a);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
          /*  this.Inicio.viewModel.Pesquisa.busca = Busca.Text;
            if (Tipo.SelectedItem != null)
                this.Inicio.viewModel.Pesquisa.tipo =  Tipo.SelectedItem.ToString();
            if (Cidade.SelectedItem != null)
                this.Inicio.viewModel.Pesquisa.cidade = Cidade.SelectedItem.ToString();
            if (Bairro.SelectedItem != null)
                this.Inicio.viewModel.Pesquisa.bairro = Bairro.SelectedItem.ToString();
            if (Dormitorios.SelectedItem != null)
                this.Inicio.viewModel.Pesquisa.dormitorios = Dormitorios.SelectedItem.ToString();
            if (Locacao.IsChecked)
                this.Inicio.viewModel.Pesquisa.categoria = Locacao.Text;
            if (Venda.IsChecked)
                this.Inicio.viewModel.Pesquisa.categoria = Venda.Text;
                */

       //     this.Inicio.viewModel.Pesquisa.faixa2 = Faixa.UpperValue;
         //   this.Inicio.viewModel.Pesquisa.faixa1 = Faixa.LowerValue;


            this.Inicio.viewModel.LoadItemsCommandBusca.Execute(null);

            this.Inicio.HideMenu();
            this.Inicio.CarregarPagina();

        }

        private void Categoria_Clicked(object sender, EventArgs e)
        {
            var a = sender;
            if (a == Locacao)
            {
                Venda.IsChecked = false;
            }
            if (a == Venda)
            {
                Locacao.IsChecked = false;
            }


        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            this.Inicio.viewModel.Pesquisa.busca = null;
            this.Inicio.viewModel.Pesquisa.categoria = null;
            this.Inicio.viewModel.Pesquisa.cidade = null;
            this.Inicio.viewModel.Pesquisa.dormitorios = null;
            this.Inicio.viewModel.Pesquisa.bairro = null;
            this.Inicio.viewModel.Pesquisa.negocio = null;
            this.Inicio.viewModel.LoadItemsCommand.Execute(null);
            Tipo.SelectedItem = null;
            Cidade.SelectedItem = null;
            Bairro.SelectedItem = null;
            Dormitorios.SelectedItem = null;
            Locacao.IsChecked = false;
            Venda.IsChecked = false;
            Busca.Text = "";
            Faixa.UpperValue = Faixa.MaximumValue;
            Faixa.LowerValue = Faixa.MinimumValue;
            this.Inicio.HideMenu();
        }
    }
}