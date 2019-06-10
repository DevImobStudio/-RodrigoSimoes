using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imobiliaria.Models
{
    public class Atendimento
    {
        public string mensagem { get; set; }
        public string titulo { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }

        public string whats { get; set; }
        public string telefone { get; set; }
        public string horario { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string youTube { get; set; }
        public Position position { get; set; }

        public string enfase => $"{logradouro} ,  {bairro} {cidade}-{uf}";
    }
}
