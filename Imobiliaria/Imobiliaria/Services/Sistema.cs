using Imobiliaria.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Imobiliaria.Services
{
    public class Sistema
    {
        public static Rest RESTAPI { get; set; }
        public static Configuracao CONFIG { get; set; }
        public static TabbedPage FOOTER { get; set; }
        public static DataBaseAsync DATABASE { get; set; }
    }
}
