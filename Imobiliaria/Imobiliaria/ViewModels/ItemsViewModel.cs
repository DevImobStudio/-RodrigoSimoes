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

namespace Imobiliaria.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Imovel> Imovels { get; set; }
        public ObservableCollection<Pin> Pins { get; set; }
        public Xamarin.Forms.GoogleMaps.Map Mapa { get; set; }
        public Command LoadItemsCommand { get; set; }
        List<Imovel> LstImoveis { get; set; }
        List<Pin> LstPins { get; set; }

        public ItemsViewModel()
        {
            LstImoveis = new List<Imovel>();
            Imovels = new ObservableCollection<Imovel>(LstImoveis);

            Title = "Imovéis";
         
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

          
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
    }
}