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
        public ObservableCollection<Imovel> FavoritosLista { get; set; }
        // public Xamarin.Forms.GoogleMaps.Map Mapa { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command LoadItemsCommandBusca { get; set; }
        public Command LoadItemsCommandFavoritos { get; set; }
        public List<Imovel> LstImoveis { get; set; }
     //   List<Pin> LstPins { get; set; }
        public List<Models.Favoritos> favoritos { get; set; }
        public Models.MPesquisa mPesquisa { get; set; }

        public ItemsViewModel()
        {
            LstImoveis = new List<Imovel>();
            Imovels = new ObservableCollection<Imovel>(LstImoveis);
            favoritos = new List<Models.Favoritos>();
            FavoritosLista = new ObservableCollection<Imovel>();
            mPesquisa = new Models.MPesquisa();
            Title = "Imovéis";
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommandBusca = new Command(async () => await ExecuteLoadItemsBuscaCommand());
            LoadItemsCommandFavoritos = new Command(async () => await ExecuteLoadItemsCommandFavoritos());
        }

        async Task ExecuteLoadItemsCommandFavoritos()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                FavoritosLista.Clear();
                Sistema.USUARIO = await Services.Sistema.DATABASE.database.Table<Models.Usuario>().Where(p => p.logado).FirstOrDefaultAsync();
                if (Sistema.USUARIO != null)
                {
                    favoritos = await Services.Sistema.DATABASE.database.Table<Models.Favoritos>().Where(p => p.idUsuario == Sistema.USUARIO.cod).ToListAsync();
                }

                foreach (var i in favoritos)
                {
                    Models.Imovel imovel = Imovels.Where(p => p.id == i.idImovel).FirstOrDefault();
                    if (imovel != null)
                    {
                        FavoritosLista.Add(imovel);
                    }
                    else
                    {
                        FavoritosLista.Add(new Models.Imovel()
                        {
                            id = i.idImovel,
                            titulo = "Indisponível"
                        });
                    }

                }

            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;

            }
        }


        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
               
                Imovels.Clear();
                if (LstImoveis.Count < 1)
                {
                    LstImoveis = await Services.Sistema.RESTAPI.getAsync<List<Imovel>>("");
                }
               
                Sistema.USUARIO = await Services.Sistema.DATABASE.database.Table<Models.Usuario>().Where(p => p.logado).FirstOrDefaultAsync();
                favoritos.Clear();
                if (Sistema.USUARIO != null)
                {
                    favoritos = await Services.Sistema.DATABASE.database.Table<Models.Favoritos>().Where(p => p.idUsuario == Sistema.USUARIO.cod).ToListAsync();
                }
               
               // Imovels = new ObservableCollection<Imovel>(imo as List<Imovel>);
                Geocoder coder = new Geocoder();
                foreach (var i in LstImoveis)
                {
                    if (favoritos != null)
                    {
                        if (favoritos.Where(p=> p.idImovel.Equals(i.id)).Count() > 0)
                        {
                            i.favorito = "Red";
                        }
                        else
                        {
                            i.favorito = "White";
                        }
                    }
                    if(i.position != null)
                    {
                        Location loc = new Location();
                        var a = await coder.GetPositionsForAddressAsync(i.logradouro + "," + i.bairro + "," + i.cidade + "-" + i.uf + "," + i.cep);
                        if (a.Any())
                        {
                            loc.Latitude = a.ElementAt(0).Latitude;
                            loc.Longitude = a.ElementAt(0).Longitude;
                            i.localizacao = loc;
                            i.position = new Position(a.ElementAt(0).Latitude, a.ElementAt(0).Longitude);
                            i.icon = BitmapDescriptorFactory.FromView(new ViewPin());
                            i.uriImage = new UriImageSource { CachingEnabled = false, Uri = new Uri(i.imagem) };
                        }
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

                if (mPesquisa.categoria != null)
                {
                    FiltroImoveis = FiltroImoveis.Where(p => p.categoria == mPesquisa.categoria).ToList();
                }
                
                if (mPesquisa.negocio != null )
                {
                    FiltroImoveis = FiltroImoveis.Where(p => p.negocio == mPesquisa.negocio).ToList();

                }

                if (mPesquisa.cidade != null)
                {
                    FiltroImoveis = FiltroImoveis.Where(p => p.cidade == mPesquisa.cidade).ToList();
                }

                if (mPesquisa.bairro != null)
                {
                    if (mPesquisa.bairro.Count > 0)
                    {
                        FiltroImoveis = FiltroImoveis.Where(p => mPesquisa.bairro.Contains(p.bairro)).ToList();
                    }

                }

                if (mPesquisa.busca != null)
                {
                    if (mPesquisa.busca != "")
                    {
                        FiltroImoveis = FiltroImoveis.Where(p => (p.descricao.Contains(mPesquisa.busca)) || (p.titulo.Contains(mPesquisa.busca))).ToList();
                    }

                }

                if (mPesquisa.dormitorios != null)
                {
                    if (mPesquisa.dormitorios.Count > 0)
                    {
                        
                        FiltroImoveis = FiltroImoveis.Where(p => mPesquisa.dormitorios.Contains(p.dormitorios)).ToList();

                    }
   
                }

                if (mPesquisa.faixa1 > 0 && mPesquisa.faixa2 >0)
                {
                    FiltroImoveis = FiltroImoveis.Where(p => p.valor >= mPesquisa.faixa1 && p.valor <= mPesquisa.faixa2).ToList();

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
                foreach (var i in LstImoveis)
                {
                    Imovels.Add(i);
                }
                CrossToastPopUp.Current.ShowToastMessage("Erro ao gerar busca", Plugin.Toast.Abstractions.ToastLength.Long);
            }
            finally
            {
                IsBusy = false;

            }
        }
    }
}