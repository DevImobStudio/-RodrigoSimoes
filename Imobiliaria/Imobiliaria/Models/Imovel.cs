﻿using System;
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
        private string preco { get; set; }

       /* public string preco
        {
            get
            {
                return Preco;
            }
            set
            {
                if (value == null)
                {
                    Preco = "Consulte!";
                }
                else
                {
                    Preco = value;
                }

            }
        }
        */

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
       

        public Location localizacao { get; set; }
        public Position position { get; set; }
        public BitmapDescriptor icon { get; set; }

        private View ViewPin(object i)
        {
            throw new NotImplementedException();
        }
        public string enfase => $"{negocio} -  {bairro} {cidade}-{uf}" ;


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
