using Imobiliaria.Models;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Imobiliaria.ViewModels
{
    public class AtendimentoViewModel : BaseViewModel
    {
        public Atendimento atendimento { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ObservableCollection<Atendimento> Atendimento { get; set; }


        public AtendimentoViewModel()
        {
            Atendimento = new ObservableCollection<Atendimento>();
            atendimento = new Atendimento();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());


        }



        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Atendimento.Clear();


              //  atendimento = await Services.Sistema.RESTAPI.getAsync<Imovel>("content/" + Imovel.id);
               if (atendimento != null)
               {
                    Atendimento.Add(atendimento);
               }   


            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastMessage(ex.Message);
            }
            finally
            {
                IsBusy = false;

            }
        }
    }
}