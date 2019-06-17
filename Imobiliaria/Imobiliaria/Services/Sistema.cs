using Imobiliaria.Models;
using Imobiliaria.Views;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.OpenWhatsApp;

namespace Imobiliaria.Services
{
    public class Sistema
    {
        public static Rest RESTAPI { get; set; }
        public static Configuracao CONFIG { get; set; }
        public static TabbedPage FOOTER { get; set; }
        public static DataBaseAsync DATABASE { get; set; }
        public static TabbedPage1 TABBEDPAGE { get; set; }
        public static Usuario USUARIO { get; set; }
        public static MenuSuperior menuSuperior { get; set; }






        public static void Contato(string numero)
        {
            try
            {
                PhoneDialer.Open(numero);
            }
            catch (ArgumentNullException anEx)
            {
                CrossToastPopUp.Current.ShowToastMessage(anEx.Message, Plugin.Toast.Abstractions.ToastLength.Long);
            }
            catch (FeatureNotSupportedException ex)
            {
                CrossToastPopUp.Current.ShowToastMessage(ex.Message, Plugin.Toast.Abstractions.ToastLength.Long);
            }
            catch (Exception ex)
            {
                CrossToastPopUp.Current.ShowToastMessage(ex.Message, Plugin.Toast.Abstractions.ToastLength.Long);
            }
        }


        public static void WhatsApp(Imovel imovel)
        {
            Chat.Open("+55012991734478",
                "Gostaria de mais informações sobre o " + imovel.tipo + " - "
                + imovel.enfase + " referencia: " + imovel.id + " Desde já obrigada!");
        }

    }
}
