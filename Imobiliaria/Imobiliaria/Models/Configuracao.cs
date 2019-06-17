using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace Imobiliaria.Models
{
    public class Configuracao
    {
        public string titulo { get; set; }
        public string logotipo { get; set; }
        public string cor_padrao { get; set; }
        public string cor_texto { get; set; }
        public string mensagem { get; set; }
      
        public string whatsapp { get; set; }
        public string telefone { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string youtube { get; set; }
        public Position position { get; set; }

    }
}
