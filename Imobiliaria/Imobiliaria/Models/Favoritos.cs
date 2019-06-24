using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imobiliaria.Models
{
    [Table("Favoritos")]
    public class Favoritos
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int idUsuario { get; set; }
        public string idImovel { get; set; }
       

        [Ignore]
        public List<Imovel> imovels { get; set; }

    }
}
