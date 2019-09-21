using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Imobiliaria.Models;
using Xamarin.Forms;

namespace Imobiliaria.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Imovel Imovel { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ObservableCollection<string> Imagens { get; set; }



       
        public List<string> lstImagens { get; set; }


        public ItemDetailViewModel(Imovel item = null)
        {
            Imovel = new Imovel(); 
            this.Imovel = item;
            lstImagens = new List<string>();
            Imagens = new ObservableCollection<string>(lstImagens);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
          

        }
      

        


        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Imagens.Clear();

              

                lstImagens = await Services.Sistema.RESTAPI.getAsync<List<string>>("content/itens/"+Imovel.id);
                if (lstImagens.Count > 1)
                {
                    //Imagens.Clear();
                    foreach (var i in lstImagens)
                    {

                        Imagens.Add(i);
                    }
                }
                else
                {
                    Imagens.Add(Imovel.imagem);
                }
                List<Imovel> p  = await Services.Sistema.RESTAPI.getAsync<List<Imovel>>("content/" + Imovel.id);
                if (p.Count > 0)
                {
                  
                    if (p[0].video != null)
                    {
                        if (p[0].video != "")
                        {
                            Imagens.Add("http://www.youtube.com/embed/" + p[0].video + "?rel=0&autoplay=1");
                            Imovel.video = p[0].video;
                        }
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
    }
}
