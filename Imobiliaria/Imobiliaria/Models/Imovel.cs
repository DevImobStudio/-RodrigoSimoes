using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Imobiliaria.Models
{
    public class Imovel
    {
        public string id { get; set; }
        public string slug { get; set; }
        public string titulo { get; set; }
        public string tipo { get; set; }
        public string descricao { get; set; }
        public string descricao_longa { get; set; }
        public string video { get; set; }
        public string categoria { get; set; }
        public string negocio { get; set; }
        private string Preco { get; set; }

        public string preco_promocional { get; set; }
        public string preco_iptu { get; set; }
        public string condominio { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string imagem { get; set; }
        public string dormitorios { get; set; }
        public string suites { get; set; }
        public string vagas { get; set; }
        public string areautil { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public int quartos
        {
            get
            {
                try
                {
                    int a = (Convert.ToInt32(this.dormitorios));
                    return a;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                this.quartos = value;
            }
        }

        public double valor
        {
            get
            {
                try
                {
                    double a = (Convert.ToDouble(this.Preco.Split('.')[0]));
                    return a;
                }
                catch
                {
                    return 0;
                }

            }
            set
            {
                this.valor = value;
            }
        }

        public string preco
            {
                get
                {
                    try
                    {
                        double a = (Convert.ToDouble(this.Preco.Split('.')[0]));
                        if (a > 0)
                            return  a.ToString("C2");
                        else
                            return "Consulte!";
                }
                    catch
                    {
                        return "Consulte!";
                    }

                }
                set
                {
                    this.Preco = value;
                }
            }
            


        public Location localizacao { get; set; }
        public Position position { get; set; }
        public BitmapDescriptor icon { get; set; }

      
        public string enfase => $"{categoria} - {negocio} -  {bairro} {cidade}-{uf}" ;

        public UriImageSource uriImage { get; set; }
        /*
        public string Id { get; set; }

        public List<string> Imagem { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public string Anuncio { get; set; }
        public string Cidade { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Endereco { get; set; }
        public double Preco { get; set; }
        public string Quartos { get; set; }
        public string Suites { get; set; }
        public string Vagas { get; set; }
        public string Area { get; set; }
        public string Comercial { get; set; }
        public string Icon
        {
            get
            {

                if (Tipo.Equals("aluguel"))
                {
                    return "fas-dollar-sign";
                }
                else
                {
                    if (Tipo.Equals("venda"))
                    {
                        return "fas-home";
                    }
                    else
                    {

                        return "fas-building";

                    }

                }
            }
            set { }

        }
        */
    }


}
