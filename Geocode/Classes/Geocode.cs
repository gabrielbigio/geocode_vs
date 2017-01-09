using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Geocode.Classes
{
    class Geocode
    {

        private string fonte;

        public Geocode()
        {
            this.fonte = string.Empty;
        }
        public string getFonte()
        {
            return this.fonte;
        }
        public string getFonte(int fonte)
        {
            string fonteText="";
            switch(fonte)
            {
                case 1:
                    fonteText = "Google";
                    break;
                case 2:
                    fonteText = "Nominatim";
                    break;
                case 3:
                    fonteText = "Bing";
                    break;
                case 4:
                    fonteText = "Yahoo";
                    break;
                default:
                    fonteText = "Não definido";
                    break;
            }
            return fonteText;
        }

       
        //função para fazer geocodereverso late long e transforma em endereco
        public string geocodeReverso(int fonte,string lat,string lng,string email)
        {
            //inicializa variavel vazia
            string url=string.Empty;
            //inicializa um conjuto de tabela
            DataSet dsResult = new DataSet();
            //chama a função que retorna a url de acordo com os parametros informados
            url = getURL(fonte, lat, lng,email);
            //chama a função para requisição da url informada e retorna um conjunto de tabela arquivo XML
           

            return requestURL(url);
        }

        private string getURL(int fonte,string lat,string lng,string email)
        {
            //inicializa variavel vazia
            string url = string.Empty;

            ///verificar qual fonte para definir url
            switch (fonte)
            {
                case 1:
                    //google
                    url = "http://maps.google.com/maps/api/geocode/json?latlng=" + lat + "," + lng;
                    this.fonte = "Google";
                    break;

                case 2:                   
                    //nominatim
                    url = "http://nominatim.openstreetmap.org/reverse?format=xml&lat=" + lat + "&lon=" + lng + "&email="+email;
                    this.fonte = "Nominatim";

                    break;
                case 3:
                    //Bing
                    string key = "iQ598x9Ach0U3IlXHzs3~KbAwH5K6fe972oe2EEP_zg~AulzAZzOfcyXLxUTM94njWp8ZONS1LNTECMylnAmNXtRYtKkvY5RFVgwdBkAfFKt";
                    url = "http://dev.virtualearth.net/REST/v1/Locations/" + lat + "," + lng + "?o=json&key=" + key;
                    this.fonte = "Bing";

                    break;

            }

            //retorna a url de acordo com a fonte
            return url;
        }
        public string DownloadString(string url)
        {
            WebClient client = new WebClient();
            string reply = client.DownloadString(url);

            return reply;
        }
        private string requestURL(string url)
        {
            try
            {
                //inicializa um conjuto de tabela
                DataSet dsResult = new DataSet();
                ///cria um objeto para requisição com a url informada
                //WebRequest request = WebRequest.Create(url);

                
                XmlTextReader readerxml = new XmlTextReader(url);

                //string txtxml = DownloadString(url);
                string txtjson = DownloadString(url);
               


                
                
                //XmlDocument docxml = new XmlDocument();
                //docxml.LoadXml(txtxml);
                //XmlNodeList listxml;
               

                //dsResult.ReadXml(new XmlNodeReader(docxml));


                
                
                ////realiza a requisição
                //using (WebResponse response = (HttpWebResponse)request.GetResponse())
                //{

                //    //retorna os dados em reader
                //    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                //    {
                //     //ler o xml e coloca dentro de conjuto de tabelas  
                //        dsResult.ReadXml(reader);
                //       // XmlDocument Xml = new XmlDocument();
                       
                //    }
                //}
                ////retorna o conjunto de tabelas
                return txtjson;

            }catch(Exception ex)
            {
                ////inicializa um conjunto de tabela
                //DataSet dsErro = new DataSet();
                ////adiciona uma tabela vazia
                //dsErro.Tables.Add("Erro");
                ////adiciona um coluna chamada Erro
                //dsErro.Tables["Erro"].Columns.Add(new DataColumn("Erro"));
                ////adiciona uma linha  com indice 1 e a informação da excesão
                //dsErro.Tables["Erro"].Rows.Add(new object[] { 1, ex.Message }); 
                ////retornar o conjunto de tabela com erro
                return "Erro(0x00101) : "+ex.Message;
                
            }
             
        }




    }
}
