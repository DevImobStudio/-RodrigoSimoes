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

        [Ignore]
        public List<Favoritos> favoritos { get; set; }
    }
}
