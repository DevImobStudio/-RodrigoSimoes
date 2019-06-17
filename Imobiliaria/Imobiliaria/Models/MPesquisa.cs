using System;
using System.Collections.Generic;
using System.Text;

namespace Imobiliaria.Models
{
    public class MPesquisa
    {
        public string cidade { get; set; }
        public List<string> bairro { get; set; }
        public List<string> dormitorios { get; set; }
        public string busca { get; set; }
        public string negocio { get; set; }
        public string categoria { get; set; }
        public double faixa1 { get; set; }
        public double faixa2 { get; set; }
    }

   
}
