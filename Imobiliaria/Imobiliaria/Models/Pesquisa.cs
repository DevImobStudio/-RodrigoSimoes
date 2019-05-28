using System;
using System.Collections.Generic;
using System.Text;

namespace Imobiliaria.Models
{
    public class Pesquisa
    {
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string dormitorios { get; set; }
        public string busca { get; set; }
        public string tipo { get; set; }
        public string categoria { get; set; }
        public double faixa1 { get; set; }
        public double faixa2 { get; set; }
    }
}
