using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imobiliaria.Models;

namespace Imobiliaria.Services
{
    public class MockDataStore : IDataStore<Imovel>
    {
        List<Imovel> items;

        public MockDataStore()
        {
            items = new List<Imovel>();
            /*  var mockItems = new List<Imovel>
              {
                  new Imovel { Id = Guid.NewGuid().ToString(), Descricao = "Sobrado com 3 suites, 1 lavabo, sala de estar e de jantar, cozinha, lavanderia, garagem 2 vagas cobertas, quintal 5x5 com area coberta. Area construida 130m2, terreno 130m2.",
                  Endereco = "Rua João Eduardo",  Imagem = new List<string>(){ "https://plantapronta.com.br/projetos/93/01.jpg" , "https://plantapronta.com.br/projetos/93/02.jpg", "https://plantapronta.com.br/projetos/93/04.jpg", "https://plantapronta.com.br/projetos/93/06.jpg"},
                  Latitude = -23.0236542,Longitude = -45.5641759, Anuncio = "Andares: 2 | características do Imóvel: Armário de Cozinha, Geminada, Lavabo, Quintal, Varanda",
                  Titulo = "Casa do Rodrigo",Preco = 500000, Tipo="venda", Cidade="Taubaté",Area="130",Quartos="4",Suites= "1",Vagas = "2"},
                  new Imovel { Id = Guid.NewGuid().ToString(), Descricao = "Sobrado com 3 suites, 1 lavabo, sala de estar e de jantar, cozinha, lavanderia, garagem 2 vagas cobertas, quintal 5x5 com area coberta. Area construida 130m2, terreno 130m2.",
                  Endereco = "Rua João Eduardo",  Imagem = new List<string>(){ " https://plantapronta.com.br/projetos/106/04.jpg", "https://plantapronta.com.br/projetos/106/01.jpg", "https://plantapronta.com.br/projetos/106/02.jpg", "https://plantapronta.com.br/projetos/106/05.jpg"},
                  Latitude = -23.0236542,Longitude = -45.5641759,
                  Titulo = "Casa do Julio",Preco = 145000 , Tipo="aluguel", Cidade="Taubaté",Area="130",Quartos="4",Suites= "1",Vagas = "2"},
                  new Imovel { Id = Guid.NewGuid().ToString(), Descricao = "Sobrado com 3 suites, 1 lavabo, sala de estar e de jantar, cozinha, lavanderia, garagem 2 vagas cobertas, quintal 5x5 com area coberta. Area construida 130m2, terreno 130m2.",
                  Endereco = "Rua João Eduardo", Imagem = new List<string>(){ " https://plantapronta.com.br/projetos/126/01.jpg", "https://plantapronta.com.br/projetos/126/02.jpg", "https://plantapronta.com.br/projetos/126/03.jpg", "https://plantapronta.com.br/projetos/126/04.jpg" },
                  Latitude = -23.0189343,Longitude = -45.5643427,
                  Titulo = "Casa do Gustavo",Preco = 183000 , Tipo="aluguel", Cidade="Taubaté",Area="130",Quartos="4",Suites= "1",Vagas = "2"},
                  new Imovel { Id = Guid.NewGuid().ToString(), Descricao = "Sobrado com 3 suites, 1 lavabo, sala de estar e de jantar, cozinha, lavanderia, garagem 2 vagas cobertas, quintal 5x5 com area coberta. Area construida 130m2, terreno 130m2.",
                  Endereco = "Rua João Eduardo",  Imagem = new List<string>(){ "https://plantapronta.com.br/projetos/115/01.jpg","https://plantapronta.com.br/projetos/115/02.jpg","https://plantapronta.com.br/projetos/115/04.jpg"},
                  Latitude = -23.0189343,Longitude = -45.5643427,
                  Titulo = "Casa do Eduardo" ,Preco = 178000, Tipo="", Cidade="Taubaté",Area="130",Quartos="4",Suites= "1",Vagas = "2"},
                  new Imovel { Id = Guid.NewGuid().ToString(), Descricao = "Sobrado com 3 suites, 1 lavabo, sala de estar e de jantar, cozinha, lavanderia, garagem 2 vagas cobertas, quintal 5x5 com area coberta. Area construida 130m2, terreno 130m2.",
                  Endereco = "Rua João Eduardo", Imagem = new List<string>(){ " https://plantapronta.com.br/projetos/116/01.jpg","https://plantapronta.com.br/projetos/116/02.jpg","https://plantapronta.com.br/projetos/116/03.jpg","https://plantapronta.com.br/projetos/116/04.jpg","https://plantapronta.com.br/projetos/116/05.jpg","https://plantapronta.com.br/projetos/116/07.jpg"},
                  Latitude = -23.0198553,Longitude = -45.5611115,
                  Titulo = "Casa da Maria" ,Preco = 583000, Tipo="venda", Cidade="Taubaté",Area="130",Quartos="4",Suites= "1",Vagas = "2"},
                  new Imovel { Id = Guid.NewGuid().ToString(), Descricao = "Sobrado com 3 suites, 1 lavabo, sala de estar e de jantar, cozinha, lavanderia, garagem 2 vagas cobertas, quintal 5x5 com area coberta. Area construida 130m2, terreno 130m2.",
                  Endereco = "Rua João Eduardo", Imagem = new List<string>(){ " https://plantapronta.com.br/projetos/120/02.jpg","https://plantapronta.com.br/projetos/120/04.jpg" },
                  Latitude = -23.0206228,Longitude = -45.5555037,
                  Titulo = "Casa da Joana",Preco = 782000 , Tipo="venda", Cidade="Taubaté",Area="130",Quartos="4",Suites= "1",Vagas = "2"},

              };


              foreach (var item in mockItems)
              {
                  items.Add(item);
              }
               */
        }

        public async Task<bool> AddItemAsync(Imovel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Imovel item)
        {
            var oldItem = items.Where((Imovel arg) => arg.id == item.id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Imovel arg) => arg.id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Imovel> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.id == id));
        }

        public async Task<IEnumerable<Imovel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}