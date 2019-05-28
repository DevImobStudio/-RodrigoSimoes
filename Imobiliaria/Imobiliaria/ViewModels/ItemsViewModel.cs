using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Imobiliaria.Models;
using Imobiliaria.Views;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;
using System.Linq;
using Imobiliaria.Services;
using Plugin.Toast;

namespace Imobiliaria.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Imovel> Imovels { get; set; }
        public ObservableCollection<Pin> Pins { get; set; }
       // public Xamarin.Forms.GoogleMaps.Map Mapa { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command LoadItemsCommandBusca { get; set; }
        public List<Imovel> LstImoveis { get; set; }
     //   List<Pin> LstPins { get; set; }
        public List<Models.Favoritos> favoritos { get; set; }
        public Pesquisa Pesquisa { get; set; }

        public ItemsViewModel()
        {
            LstImoveis = new List<Imovel>();
            Imovels = new ObservableCollection<Imovel>(LstImoveis);
            favoritos = new List<Models.Favoritos>();
            Pesquisa = new Pesquisa();
            Title = "Imovéis";
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommandBusca = new Command(async () => await ExecuteLoadItemsBuscaCommand());

        }
      

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
               
                Imovels.Clear();
                LstImoveis =  await Services.Sistema.RESTAPI.getAsync<List<Imovel>>("");
                favoritos = await Services.Sistema.DATABASE.database.Table<Models.Favoritos>().ToListAsync();
               // Imovels = new ObservableCollection<Imovel>(imo as List<Imovel>);
                Geocoder coder = new Geocoder();
                foreach (var i in LstImoveis)
                {
              
                    Location loc = new Location();
                    var a = await coder.GetPositionsForAddressAsync(i.logradouro + "," + i.bairro + "," + i.cidade + "-" + i.uf + "," + i.cep);
                    if (a.Any())
                    {
                        loc.Latitude = a.ElementAt(0).Latitude;
                        loc.Longitude = a.ElementAt(0).Longitude;
                        i.localizacao = loc;
                        i.position = new Position(a.ElementAt(0).Latitude, a.ElementAt(0).Longitude);
                        i.icon = BitmapDescriptorFactory.FromView(new ViewPin(i));
                        i.uriImage = new UriImageSource { CachingEnabled = false, Uri = new Uri(i.imagem) };
                    }

                  
                    Imovels.Add(i);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
               
            }
        }

        async Task ExecuteLoadItemsBuscaCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                Imovels.Clear();

                Geocoder coder = new Geocoder();

                List<Imovel> FiltroImoveis = LstImoveis;

                if (Pesquisa.categoria != null)
                {
                    FiltroImoveis = FiltroImoveis.Where(p => p.categoria == Pesquisa.categoria).ToList();
                }
                
                if (Pesquisa.tipo != null )
                {
                    if (Pesquisa.tipo != "Todos")
                    {
                        FiltroImoveis = FiltroImoveis.Where(p => p.tipo == Pesquisa.tipo).ToList();
                    }

                }

                if (Pesquisa.cidade != null)
                {
                    if (Pesquisa.cidade != "Todas")
                    {
                        FiltroImoveis = FiltroImoveis.Where(p => p.cidade == Pesquisa.cidade).ToList();
                    }

                }

                if (Pesquisa.bairro != null)
                {
                    if (Pesquisa.bairro != "Todos")
                    {
                        FiltroImoveis = FiltroImoveis.Where(p => p.bairro == Pesquisa.bairro).ToList();
                    }

                }

                if (Pesquisa.busca != null)
                {
                    if (Pesquisa.bairro != "")
                    {
                        FiltroImoveis = FiltroImoveis.Where(p => p.descricao.Contains(Pesquisa.busca) || p.descricao_longa.Contains(Pesquisa.busca) || p.titulo.Contains(Pesquisa.busca)).ToList();
                    }

                }

                if (Pesquisa.dormitorios != null)
                {
                    if (Pesquisa.dormitorios != "Indiferente")
                    {
                        FiltroImoveis = FiltroImoveis.Where(p => p.dormitorios == Pesquisa.dormitorios).ToList();
                    }
   
                }

                if (Pesquisa.faixa1 > 0)
                {
                    FiltroImoveis = FiltroImoveis.Where(p => p.valor >= Pesquisa.faixa1 && p.valor <= Pesquisa.faixa2).ToList();

                }

                foreach (var i in FiltroImoveis)
                {
                    Imovels.Add(i);
                }
                if (Imovels.Count <=0)
                {
                    CrossToastPopUp.Current.ShowToastMessage("Sem imóveis para esta busca",Plugin.Toast.Abstractions.ToastLength.Long);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;

            }
        }
    }
}