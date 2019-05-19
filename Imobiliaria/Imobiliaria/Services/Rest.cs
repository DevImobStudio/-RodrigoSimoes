using ModernHttpClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Imobiliaria.Services
{
    public class Rest
    {

        private string baseUrl { get; set; }

        private HttpClient client;

        public const string urlConfig = "config/";
        public const string urlDetalhe = "content/";
        public const string urlImagens = "content/itens/";

        public void setToken(string token)
        {
            this.client.DefaultRequestHeaders.Add("token", token);
        }

        public Rest(string urlBase)
        {
            try
            {
                this.baseUrl = urlBase;
                this.client = new HttpClient(new NativeMessageHandler());
                this.client.Timeout = new TimeSpan(0, 0, 10);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> tratarRetornoAsync<TEntity>(HttpResponseMessage msg) where TEntity : class
        {
            try
            {
                if (msg.IsSuccessStatusCode)
                {
                    string json = await msg.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<TEntity>(json);
                }
                else
                {
                    throw new Exception("Falha REST, código: " + msg.StatusCode);
                }
            }
            catch
            {
                throw;
            }
        }


        public async Task<TEntity> getAsync<TEntity>(string url) where TEntity : class
        {
            try
            {
                HttpResponseMessage msg = await this.client.GetAsync(baseUrl + url);
                return await this.tratarRetornoAsync<TEntity>(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<TEntity> putAsync<TEntity>(string url, object obj) where TEntity : class
        {
            try
            {
                HttpResponseMessage msg = await this.client.PutAsync(baseUrl + url, new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj)));

                return await this.tratarRetornoAsync<TEntity>(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> postAsync<TEntity>(string url, object obj) where TEntity : class
        {
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                HttpResponseMessage msg = await this.client.PostAsync(baseUrl + url, new StringContent(json));

                return await this.tratarRetornoAsync<TEntity>(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual async Task<TEntity> deleteAsync<TEntity>(string url, int id) where TEntity : class
        {
            try
            {
                HttpResponseMessage msg = await this.client.DeleteAsync(baseUrl + url + id.ToString());

                return await this.tratarRetornoAsync<TEntity>(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
