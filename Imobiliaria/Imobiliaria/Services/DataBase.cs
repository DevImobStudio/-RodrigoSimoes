using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imobiliaria.Services
{
    public class DataBase
    {
        public async Task<bool> CriarTabelaConexao()
        {
            try
            {
                 await Sistema.DATABASE.database.CreateTableAsync<Models.Favoritos>();
                 await Sistema.DATABASE.database.CreateTableAsync<Models.Usuario>();

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
