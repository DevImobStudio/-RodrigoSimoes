using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imobiliaria.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int cod { get; set; }
        public bool logado { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string whatsapp { get; set; }
        public string link { get; set; }
        public string imagem { get; set; }
        public string TwitterId { get; set; }
        public string ScreenName { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }

        [Ignore]
        public Picture picture { get; set; }


        public bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Token);
            }
        }


        [Ignore]
        public List<Favoritos> favoritos { get; set; }


    }

    public class Picture {
        public data data { get; set; }
    }

    public class data
    {
        public string height { get; set; }
        public string url { get; set; }
        public string width { get; set; }
    }


    public class UserGoogle
    {
        public string email { get; set; }
        public string picture { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }
}
