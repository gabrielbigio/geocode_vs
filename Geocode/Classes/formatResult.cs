using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using Newtonsoft.Json.Linq;
namespace Geocode.Classes
{
    class formatResult
    {


        validacao validar;
              

        public formatResult()
       {
            
       }
        public string extractFromAdress(dynamic components, dynamic type) 
        {
            for (var i = 0; i < components.length; i++)
                for (var j = 0; j < components[i].types.length; j++)
                    if (components[i].types[j] == type) 
                        return components[i].long_name;


             return "";
        }
        public string [] resultJsonReversoGeo(string txtreturn,int fonte,string pk ,string fk)
        {
            string numero = "NULL";
            string logradouro = "NULL";
            string bairro = "NULL";
            string cidade = "NULL";
            string estado = "NULL";
            string pais = "NULL";
            string cep = "NULL";
            string lat = "NULL";
            string lng = "NULL";
            string precisao = "NULL";
            string complemento = "NULL";            
            string[] row = new string[13];
            dynamic json;
            string type = "";
           
            validar = new validacao();

            try
            {
                switch (fonte)
                {

                    case 1://GOOGLE
                        json = JValue.Parse(txtreturn);
                        if (json.status == "OK")
                        {
                            precisao = json.results[0].geometry.location_type;
                            lat = json.results[0].geometry.location.lat;
                            lng = json.results[0].geometry.location.lng;
                            for (int i = 0; i < json.results.Count; i++)
                            {
                                for (int j = 0; j < json.results[i].address_components.Count; j++)
                                {
                                    type = json.results[i].address_components[j].types[0];

                                    if (type == "premise")
                                    {
                                        complemento = json.results[i].address_components[j].long_name;
                                        complemento = "NULL";
                                    }
                                    else if (type == "street_number")
                                    {
                                        numero = json.results[i].address_components[j].long_name;
                                        if (numero.Contains("-"))
                                        {
                                            numero = validar.calcMedia(numero.Split('-')).ToString();
                                        }

                                        numero = validar.removeCaracter(numero);


                                    }
                                    else if (type == "route")
                                    {
                                        logradouro = json.results[i].address_components[j].long_name;
                                    }
                                    else if (type == "sublocality_level_1")
                                    {
                                        bairro = json.results[i].address_components[j].long_name;
                                    }
                                    else if (type == "locality")
                                    {
                                        cidade = json.results[i].address_components[j].long_name;
                                    }
                                    else if (type == "administrative_area_level_1")
                                    {
                                        estado = json.results[i].address_components[j].long_name;
                                    }
                                    else if (type == "country")
                                    {
                                        pais = json.results[i].address_components[j].long_name;
                                    }
                                    else if (type == "postal_code")
                                    {
                                        cep = json.results[i].address_components[j].long_name;
                                    }


                                }





                                if (numero != "NULL" && logradouro != "NULL" && bairro != "NULL" && cidade != "NULL" && estado != "NULL" && pais != "NULL" && cep != "NULL" )//se tudo completado break
                                {
                                    break;
                                }

                            }

                            row = new string[] { bairro, logradouro, numero, cidade, complemento, cep, estado, pais, lat, lng, "Google", precisao, pk };
                        }
                        else//fim status
                        {
                            row = new string[] { "Erro(0x00101):", "Erro de requisição", "Messagem original:" + json.status, "", "", "", "", "", "", "", "", "", pk};
                        }


                        break;//fim case 1

                    case 2://nominatim
                        break;

                    case 3://bing
                        json = JValue.Parse(txtreturn);
                        //DataSet ds = new DataSet();
                        // XmlDocument docxml = new XmlDocument();
                        // docxml.LoadXml(txtreturn);        
                        // ds.ReadXml(new XmlNodeReader(docxml));
                        //if (ds.Tables["Response"].Rows[0]["StatusDescription"].ToString() == "OK")
                        //{
                        //    if (ds.Tables["Address"].Rows[0]["AddressLine"].ToString().Contains(","))
                        //    {
                        //        logradouro = ds.Tables["Address"].Rows[0]["AddressLine"].ToString().Split(',')[0];
                        //        numero = ds.Tables["Address"].Rows[0]["AddressLine"].ToString().Split(',')[1].Trim();
                        //    }
                        //    else
                        //    {
                        //        logradouro = ds.Tables["Address"].Rows[0]["AddressLine"].ToString();
                        //    }
                        //    row = new string[] { "NULL", logradouro, numero, ds.Tables["Address"].Rows[0]["Locality"].ToString(), "NULL", ds.Tables["Address"].Rows[0]["PostalCode"].ToString(), ds.Tables["Address"].Rows[0]["AdminDistrict"].ToString(),"Brasil",ds.Tables["Point"].Rows[0]["Latitude"].ToString(), ds.Tables["Point"].Rows[0]["Longitude"].ToString(), "Bing", ds.Tables["GeocodePoint"].Rows[0]["CalculationMethod"].ToString(), pk };
                        //}
                        //else
                        //{
                        //    row = new string[] { "Erro", "Não possui resultados", "", "", "", "", "", "", "", "", "", pk };
                        //}

                        if (json.statusDescription == "OK")
                        {
                            //precisao = json.results[0].geometry.location_type;
                            //lat = json.results[0].geometry.location.lat;
                            //lng = json.results[0].geometry.location.lng;
                          
                           
                            for (int i = 0; i < json.resourceSets.Count; i++)
                            {
                                
                                for (int j = 0; j < json.resourceSets[i].resources.Count; j++)
                                {
                                    dynamic teste1 = json.resourceSets[i].resources[j].o.SelectToken("$.Manufacturers[?(@.Name == 'address')]");
                                    dynamic teste2 = json.resourceSets[i].resources.Children()["address"];
                                    dynamic teste3 = json.resourceSets[i].resources.SelectToken("address");
                                    dynamic teste = json.resourceSets[i].resources.Contains("address");
                                    


                                   
                                   
                                    

                                    

                                    type =json.resourceSets[i].resources[j].address.addressLine;//////address line
                                    if(type!="null" && type!="NULL" || type!="")
                                    {
                                        if(logradouro=="NULL" || numero=="NULL")
                                        {
                                            if (type.Contains(","))
                                            {
                                                logradouro = type.Split(',')[0];
                                                numero = type.Split(',')[1];
                                            }
                                            else if (type.Contains('-'))
                                            {
                                                
                                            }
                                            else
                                            {
                                                logradouro = type;
                                            }

                                        }
                                       
                                    }
                                    //////////////////////////fim addres line

                                    estado = json.resourceSets[i].resources[j].address.adminDistrict;//estado

                                    pais = json.resourceSets[i].resources[j].address.countryRegion;//pais

                                    cep = json.resourceSets[i].resources[j].address.postalCode;//cep

                                    cidade = json.resourceSets[i].resources[j].address.locality;//cidade

                                    type = json.resourceSets[i].resources[j].geocodePoints[0].type;
                                    if(type=="Point")
                                    {
                                        lat = json.resourceSets[i].resources[j].geocodePoints[0].coordinates[0];
                                        lng = json.resourceSets[i].resources[j].geocodePoints[0].coordinates[1];
                                        precisao = json.resourceSets[i].resources[j].geocodePoints[0].calculationMethod;
                                    }

                               
                                      
                                  
                                }
                                if (numero != "NULL" && logradouro != "NULL" && bairro != "NULL" && cidade != "NULL" && estado != "NULL" && pais != "NULL" && cep != "NULL" && complemento != "NULL")//se tudo completado break
                                {
                                    break;
                                }

                            }


                            row = new string[] { bairro, logradouro, numero, cidade, complemento, cep, estado, pais, lat, lng, "Bing", precisao, pk };

                        }
                        else//fim status
                        {
                            row = new string[] { "Erro(0x00101):", "Erro de requisição", "Messagem original:" + json.status, "", "", "", "", "", "", "", "", "", pk };
                        }


                        break;

                    default:

                        row = new string[] { "Erro", "Fonte não encontrada", "", "", "", "", "", "", "", "", "", pk };
                        break;

                        

                       
                }




                return row;
            }
            catch(Exception ex)
            {
                string erro = ex.Message;
                return row = new string[] { "Erro(0x00103):", "Erro de formatação", "Messagem original:" + erro, "", "", "", "", "", "", "", "", "", pk};

            }
            
        }
        public string[] resultJsonReversoGeo(string txtreturn, int fonte)
        {
            string numero = "NULL";
            string logradouro = "NULL";
            string bairro = "NULL";
            string cidade = "NULL";
            string estado = "NULL";
            string pais = "NULL";
            string cep = "NULL";
            string lat = "NULL";
            string lng = "NULL";
            string precisao = "NULL";
            string complemento = "NULL";
            string[] row = new string[12];
            dynamic json;
            string type = "";

            validar = new validacao();

            try
            {
                switch (fonte)
                {

                    case 1://GOOGLE
                        json = JValue.Parse(txtreturn);
                        if (json.status == "OK")
                        {
                            precisao = json.results[0].geometry.location_type;
                            lat = json.results[0].geometry.location.lat;
                            lng = json.results[0].geometry.location.lng;
                            for (int i = 0; i < json.results.Count; i++)
                            {
                                for (int j = 0; j < json.results[i].address_components.Count; j++)
                                {
                                    type = json.results[i].address_components[j].types[0];

                                    if (type == "premise")
                                    {
                                        complemento = json.results[i].address_components[j].long_name;
                                        complemento = "NULL";
                                    }
                                    else if (type == "street_number")
                                    {
                                        numero = json.results[i].address_components[j].long_name;
                                        if (numero.Contains("-"))
                                        {
                                            numero = validar.calcMedia(numero.Split('-')).ToString();
                                        }

                                        numero = validar.removeCaracter(numero);


                                    }
                                    else if (type == "route")
                                    {
                                        logradouro = json.results[i].address_components[j].long_name;
                                    }
                                    else if (type == "sublocality_level_1")
                                    {
                                        bairro = json.results[i].address_components[j].long_name;
                                    }
                                    else if (type == "locality")
                                    {
                                        cidade = json.results[i].address_components[j].long_name;
                                    }
                                    else if (type == "administrative_area_level_1")
                                    {
                                        estado = json.results[i].address_components[j].long_name;
                                    }
                                    else if (type == "country")
                                    {
                                        pais = json.results[i].address_components[j].long_name;
                                    }
                                    else if (type == "postal_code")
                                    {
                                        cep = json.results[i].address_components[j].long_name;
                                    }


                                }





                                if (numero != "NULL" && logradouro != "NULL" && bairro != "NULL" && cidade != "NULL" && estado != "NULL" && pais != "NULL" && cep != "NULL")//se tudo completado break
                                {
                                    break;
                                }

                            }

                            row = new string[] { bairro, logradouro, numero, cidade, complemento, cep, estado, pais, lat, lng, "Google", precisao };
                        }
                        else//fim status
                        {
                            row = new string[] { "Erro(0x00101):", "Erro de requisição", "Messagem original:" + json.status, "", "", "", "", "", "", "", "", "" };
                        }


                        break;//fim case 1

                    case 2://nominatim
                        break;

                    case 3://bing
                        json = JValue.Parse(txtreturn);
                        //DataSet ds = new DataSet();
                        // XmlDocument docxml = new XmlDocument();
                        // docxml.LoadXml(txtreturn);        
                        // ds.ReadXml(new XmlNodeReader(docxml));
                        //if (ds.Tables["Response"].Rows[0]["StatusDescription"].ToString() == "OK")
                        //{
                        //    if (ds.Tables["Address"].Rows[0]["AddressLine"].ToString().Contains(","))
                        //    {
                        //        logradouro = ds.Tables["Address"].Rows[0]["AddressLine"].ToString().Split(',')[0];
                        //        numero = ds.Tables["Address"].Rows[0]["AddressLine"].ToString().Split(',')[1].Trim();
                        //    }
                        //    else
                        //    {
                        //        logradouro = ds.Tables["Address"].Rows[0]["AddressLine"].ToString();
                        //    }
                        //    row = new string[] { "NULL", logradouro, numero, ds.Tables["Address"].Rows[0]["Locality"].ToString(), "NULL", ds.Tables["Address"].Rows[0]["PostalCode"].ToString(), ds.Tables["Address"].Rows[0]["AdminDistrict"].ToString(),"Brasil",ds.Tables["Point"].Rows[0]["Latitude"].ToString(), ds.Tables["Point"].Rows[0]["Longitude"].ToString(), "Bing", ds.Tables["GeocodePoint"].Rows[0]["CalculationMethod"].ToString(), pk };
                        //}
                        //else
                        //{
                        //    row = new string[] { "Erro", "Não possui resultados", "", "", "", "", "", "", "", "", "", pk };
                        //}

                        if (json.statusDescription == "OK")
                        {
                            //precisao = json.results[0].geometry.location_type;
                            //lat = json.results[0].geometry.location.lat;
                            //lng = json.results[0].geometry.location.lng;


                            for (int i = 0; i < json.resourceSets.Count; i++)
                            {

                                for (int j = 0; j < json.resourceSets[i].resources.Count; j++)
                                {
                                    dynamic teste1 = json.resourceSets[i].resources[j].o.SelectToken("$.Manufacturers[?(@.Name == 'address')]");
                                    dynamic teste2 = json.resourceSets[i].resources.Children()["address"];
                                    dynamic teste3 = json.resourceSets[i].resources.SelectToken("address");
                                    dynamic teste = json.resourceSets[i].resources.Contains("address");









                                    type = json.resourceSets[i].resources[j].address.addressLine;//////address line
                                    if (type != "null" && type != "NULL" || type != "")
                                    {
                                        if (logradouro == "NULL" || numero == "NULL")
                                        {
                                            if (type.Contains(","))
                                            {
                                                logradouro = type.Split(',')[0];
                                                numero = type.Split(',')[1];
                                            }
                                            else if (type.Contains('-'))
                                            {

                                            }
                                            else
                                            {
                                                logradouro = type;
                                            }

                                        }

                                    }
                                    //////////////////////////fim addres line

                                    estado = json.resourceSets[i].resources[j].address.adminDistrict;//estado

                                    pais = json.resourceSets[i].resources[j].address.countryRegion;//pais

                                    cep = json.resourceSets[i].resources[j].address.postalCode;//cep

                                    cidade = json.resourceSets[i].resources[j].address.locality;//cidade

                                    type = json.resourceSets[i].resources[j].geocodePoints[0].type;
                                    if (type == "Point")
                                    {
                                        lat = json.resourceSets[i].resources[j].geocodePoints[0].coordinates[0];
                                        lng = json.resourceSets[i].resources[j].geocodePoints[0].coordinates[1];
                                        precisao = json.resourceSets[i].resources[j].geocodePoints[0].calculationMethod;
                                    }




                                }
                                if (numero != "NULL" && logradouro != "NULL" && bairro != "NULL" && cidade != "NULL" && estado != "NULL" && pais != "NULL" && cep != "NULL" && complemento != "NULL")//se tudo completado break
                                {
                                    break;
                                }

                            }


                            row = new string[] { bairro, logradouro, numero, cidade, complemento, cep, estado, pais, lat, lng, "Bing", precisao};

                        }
                        else//fim status
                        {
                            row = new string[] { "Erro(0x00101):", "Erro de requisição", "Messagem original:" + json.status, "", "", "", "", "", "", "", "", "" };
                        }


                        break;

                    default:

                        row = new string[] { "Erro", "Fonte não encontrada", "", "", "", "", "", "", "", "", "", };
                        break;




                }




                return row;
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                return row = new string[] { "Erro(0x00103):", "Erro de formatação", "Messagem original:" + erro, "", "", "", "", "", "", "", "", "" };

            }

        }
        public string[] resultReversoGeo(string txtxml,int fonte,string pk,string fk)
        {
            //fazer try catch instacia essa classe somente dentro da função
            // retorna messagem de erro quando ocorrer o mesmo

            DataSet ds = new DataSet();
            validar = new validacao();
            DataTable dt = new DataTable();
            string[] row =new string[12];
            
            XmlDocument docxml = new XmlDocument();
            docxml.LoadXml(txtxml);        
            ds.ReadXml(new XmlNodeReader(docxml));
            string numero = string.Empty;
            string logradouro = "NULL";
            string bairro = "NULL";
            string cidade = "NULL";
            string estado = "NULL";
            string pais = "NULL";
            string cep = "NULL";
            string lat = "NULL";
            string lng = "NULL";
            string precisao = "NULL";



            switch(fonte)
            {

                case 1://GOOGLE                  


                    if (ds.Tables["GeocodeResponse"].Rows[0]["status"].ToString() == "OK")//verifica se retorno algo
                    {
                        try
                        {
                            if (txtxml.Contains("premise"))
                            {
                                if (ds.Tables["GeocodeResponse"].Rows[0]["status"].ToString() == "OK")//verifica se retorno algo
                                {
                                    ds.Tables["address_component"].Rows[0]["long_name"].ToString();
                                    ds.Tables["address_component"].Rows[0]["short_name"].ToString();
                                    ds.Tables["address_component"].Rows[0]["type"].ToString();
                                    
                                }

                                row = new string[] { "Erro(0x00101):", "Erro de requisição", "Messagem original:" + txtxml, "", "", "", "", "", "", "", "", "", "" };
                            }

                            if (txtxml.Contains("(0x00101)"))
                            {

                                row = new string[] { "Erro(0x00101):", "Erro de requisição", "Messagem original:" +txtxml, "", "", "", "", "", "", "", "", "", "" };
                            }
                            else
                            {
                                if ((txtxml.Contains("sublocality_level_1")))
                                {
                                    

                                     if (txtxml.Contains("street_number"))
                                    {
                                        if (ds.Tables["address_component"].Rows[0][1].ToString().Contains("-"))
                                        {
                                            numero = validar.calcMedia(ds.Tables["address_component"].Rows[0]["long_name"].ToString().Split('-')).ToString();


                                        }
                                        else
                                        {
                                            numero = ds.Tables["address_component"].Rows[0]["long_name"].ToString();
                                        }
                                        
                                    }
                                    else
                                    {
                                        numero = "NULL";
                                    }
                                    if (txtxml.Contains("route"))
                                    {
                                        logradouro = ds.Tables["address_component"].Rows[1]["long_name"].ToString();

                                    }
                                    else
                                    {
                                        logradouro = "NULL";
                                    }
                                    if (txtxml.Contains("political"))
                                    {

                                        if (txtxml.Contains("sublocality"))
                                        {
                                            if (txtxml.Contains("sublocality_level_1"))
                                            {
                                                bairro = ds.Tables["address_component"].Rows[2]["long_name"].ToString();

                                            }
                                            else
                                            {
                                                bairro = "NULL";
                                            }
                                            
                                        }
                                        else
                                        {
                                            bairro = "NULL";
                                        }
                                        if (txtxml.Contains("locality"))
                                        {
                                            cidade = ds.Tables["address_component"].Rows[3]["long_name"].ToString();
                                        }
                                        else
                                        {
                                            cidade = "NULL";
                                        }
                                        if (txtxml.Contains("administrative_area_level_1"))
                                        {
                                            estado = ds.Tables["address_component"].Rows[5]["long_name"].ToString();
                                        }
                                        else
                                        {
                                            estado = "NULL";
                                        }
                                        if (txtxml.Contains("country"))
                                        {
                                            pais = ds.Tables["address_component"].Rows[6]["long_name"].ToString();
                                        }
                                        else
                                        {
                                            pais = "NULL";
                                        }


                                    }
                                    else
                                    {
                                        bairro = "NULL";
                                        cidade = "NULL";
                                        estado = "NULL";
                                        pais = "NULL";

                                    }

                                    if (txtxml.Contains("postal_code"))
                                    {

                                        cep = ds.Tables["address_component"].Rows[6]["long_name"].ToString();
                                    }
                                    else
                                    {
                                        cep = "NULL";
                                    }
                                    if(txtxml.Contains("location"))
                                    {
                                        lat = ds.Tables["location"].Rows[0]["lat"].ToString();
                                        lng = ds.Tables["location"].Rows[0]["lng"].ToString();
                                    }
                                    else
                                    {
                                        lat = "NULL";
                                        lng = "NULL";
                                    }
                                    if(txtxml.Contains("location_type"))
                                    {
                                        precisao = ds.Tables["geometry"].Rows[0]["location_type"].ToString();
                                    }
                                    else
                                    {
                                        precisao = "NULL";
                                    }
                                }                                
                                else
                                {
                                    

                                    if (txtxml.Contains("street_number"))
                                    {
                                        if (ds.Tables["address_component"].Rows[0][1].ToString().Contains("-"))
                                        {
                                            numero = validar.calcMedia(ds.Tables["address_component"].Rows[0]["long_name"].ToString().Split('-')).ToString();


                                        }
                                        else
                                        {
                                            numero = ds.Tables["address_component"].Rows[0]["long_name"].ToString();
                                        }

                                    }
                                    else
                                    {
                                        numero = "NULL";
                                    }
                                    if (txtxml.Contains("route"))
                                    {
                                        logradouro = ds.Tables["address_component"].Rows[1]["long_name"].ToString();

                                    }
                                    else
                                    {
                                        logradouro = "NULL";
                                    }
                                    if (txtxml.Contains("political"))
                                    {

                                        if (txtxml.Contains("sublocality"))
                                        {
                                            if (txtxml.Contains("sublocality_level_1"))
                                            {
                                                bairro = ds.Tables["address_component"].Rows[7]["long_name"].ToString();

                                            }
                                            else
                                            {
                                                bairro = "NULL";
                                            }

                                        }
                                        else
                                        {
                                            bairro = "NULL";
                                        }
                                        if (txtxml.Contains("locality"))
                                        {
                                            cidade = ds.Tables["address_component"].Rows[3]["long_name"].ToString();
                                        }
                                        else
                                        {
                                            cidade = "NULL";
                                        }
                                        if (txtxml.Contains("administrative_area_level_1"))
                                        {
                                            estado = ds.Tables["address_component"].Rows[4]["long_name"].ToString();
                                        }
                                        else
                                        {
                                            estado = "NULL";
                                        }
                                        if (txtxml.Contains("country"))
                                        {
                                            pais = ds.Tables["address_component"].Rows[5]["long_name"].ToString();
                                        }
                                        else
                                        {
                                            pais = "NULL";
                                        }


                                    }
                                    else
                                    {
                                        bairro = "NULL";
                                        cidade = "NULL";
                                        estado = "NULL";
                                        pais = "NULL";

                                    }

                                    if (txtxml.Contains("postal_code"))
                                    {

                                        cep = ds.Tables["address_component"].Rows[6]["long_name"].ToString();
                                    }
                                    else
                                    {
                                        cep = "NULL";
                                    }
                                    if (txtxml.Contains("location"))
                                    {
                                        lat = ds.Tables["location"].Rows[0]["lat"].ToString();
                                        lng = ds.Tables["location"].Rows[0]["lng"].ToString();
                                    }
                                    else
                                    {
                                        lat = "NULL";
                                        lng = "NULL";
                                    }
                                    if (txtxml.Contains("location_type"))
                                    {
                                        precisao = ds.Tables["geometry"].Rows[0]["location_type"].ToString();
                                    }
                                    else
                                    {
                                        precisao = "NULL";
                                    }
                                }
                                    
                                }
                                    
                            
                            
               

                            row = new string[] { bairro, logradouro, numero, cidade, "NULL", cep, estado,pais,lat ,lng, "Google", precisao, pk };


                        }
                        catch(Exception e)
                        {
                            string erro = e.Message;
                            row = new string[] { "Erro(0x00103):", "Erro de formatação", "Messagem original:" + erro, "", "", "", "", "", "", "", "", "", "" };
                        }

                       
                        

                    }
                    else 
                    {

                        row = new string[] { "Erro(0x00102):", "Não possui resultados", "Messagem original:" + ds.Tables["GeocodeResponse"].Rows[0]["status"].ToString(), "", "", "", "", "", "", "", "", "", "" };
                        
                    }
                    

                    break;

                case 2:

                    if (ds.Tables["reversegeocode"].Rows[0][0].ToString() == "0")
                    {

                        for (int j = 0; j < ds.Tables["result"].Rows.Count; j++)
                        {

                            //dsResult.Tables["addressparts"].Rows[0]["house_number"].ToString();//numero
                            //ds.Tables["addressparts"].Rows[0]["road"].ToString();//rua
                            //ds.Tables["addressparts"].Rows[0]["town"].ToString();//cidade
                            //ds.Tables["addressparts"].Rows[0]["county"].ToString();//bairro
                            //ds.Tables["addressparts"].Rows[0]["state"].ToString();//estado
                            //ds.Tables["addressparts"].Rows[0]["country"].ToString();//pais


                           row = new string[] {ds.Tables["addressparts"].Rows[0]["county"].ToString(), ds.Tables["addressparts"].Rows[0]["road"].ToString(), "NULL", ds.Tables["addressparts"].Rows[0]["town"].ToString(), "NULL", "NULL", ds.Tables["addressparts"].Rows[0]["state"].ToString(), ds.Tables["result"].Rows[j][4].ToString(), ds.Tables["result"].Rows[j][5].ToString(), "Nominatim", "",  pk};
                           

                        }

                    }
                    else
                    {
                       
                            row = new string[] { "Erro", "Não possui resultados", "", "", "", "", "", "", "", "", "", pk };
                                          

                       
                    }


                    break;
                case 3:

                    if (ds.Tables["Response"].Rows[0]["StatusDescription"].ToString() == "OK")
                    {
                        


                        if (ds.Tables["Address"].Rows[0]["AddressLine"].ToString().Contains(","))
                        {
                            logradouro = ds.Tables["Address"].Rows[0]["AddressLine"].ToString().Split(',')[0];
                            numero = ds.Tables["Address"].Rows[0]["AddressLine"].ToString().Split(',')[1].Trim();
                        }
                        else
                        {
                            logradouro = ds.Tables["Address"].Rows[0]["AddressLine"].ToString();
                        }
                        row = new string[] { "NULL", logradouro, numero, ds.Tables["Address"].Rows[0]["Locality"].ToString(), "NULL", ds.Tables["Address"].Rows[0]["PostalCode"].ToString(), ds.Tables["Address"].Rows[0]["AdminDistrict"].ToString(), ds.Tables["Point"].Rows[0]["Latitude"].ToString(), ds.Tables["Point"].Rows[0]["Longitude"].ToString(), "Bing", ds.Tables["GeocodePoint"].Rows[0]["CalculationMethod"].ToString(), pk };
                    }
                    else
                    {
                        row = new string[] { "Erro", "Não possui resultados", "", "", "", "", "", "", "", "", "", pk };
                    }


                    break;

                default:

                    row = new string[] { "Erro", "Não possui resultados", "", "", "", "", "", "", "", "", "", pk };
                    break;
               


            }




            return row;

        }

     


    }
}
