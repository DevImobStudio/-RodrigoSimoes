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
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }


        public string TwitterId { get; set; }
        public string Name { get; set; }
        public string ScreenName { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }
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
}
