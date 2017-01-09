using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Net;
using System.IO;
using System.Threading;
using MetroFramework;



namespace Geocode
{
    public partial class ViewGeocodeReverso : MetroForm
    {
        /**************
         * OBS: A tabela de ENDERECO OS DADOS DO GEOCODEREVERSO DEVE TER AS SEGUINTES COLUNAS
         * (["+pkEnder+"],[BAIRRO],[LOGRADOURO],[NUM],[MUNICIPIO],[UF],[CEP],[COMPLEMENTO],[PRECISAO],[LAT],[LNG],[FONTE],["+fk+"])
         * SENDO "pkEnder" constante chave primaria e "fk"  constante chave estrangeira citada abaixo
         * *****************/
        

        const string tblRepositorio = "LOTE";//tabela do repositorio das coordenadas
        const string tblEnder = "ENDERECO_LOTE";//tabela para gravar os dados geocodificados
        const string pk = "COD_LOTE_PK";//chave primaria repositorio das coordenadas
        const string fk = "COD_LOTE_FK";//coluna chave estrageira para ligar os dados geocode as informações do repositorio
        const string pkEnder = "COD_ENDERECO_LOTE_PK";//chave primaria da tabela dos dados geocodificados
        const string colY = "Y";//coluna Y Latitutede
        const string colX = "X";//coluna X Longitude
        const string email = "pedro.rezende@aryamap.com";//seu email
        bool existcolunm = false;//variavel para receber 
        string fontegeo = "";//recebe o nome da fonte que foi geocodeficado
        Form1 inicio;//formulario de inicio
        string cmb = "";//recebe o valor do combobox interface        
        DataTable dtCoordinates = new DataTable(); //instancia tabela na memoria RAM para posteiormente receber os dados geocodificados       
        Classes.conexao conexao = new Classes.conexao();//instancia conexao
        Classes.validacao validar = new Classes.validacao();//instacia objeto da classe validacao
        Classes.Geocode geocode;//instacia objeto da classe geocode
        Classes.formatResult format;
        
       
        
        int fonte = 0;//instacia variavel inteira para recever o valor da fonte em numero  definido no combobox
        public ViewGeocodeReverso()
        {
            InitializeComponent();
        }

        private void ViewGeocodeReverso_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            mcmbDB.SelectedIndex = 0;
            mcmbFonte.SelectedIndex = 0;
            dt = limpaData(dt);
            dt = conexao.SELECT("SELECT table_name FROM information_schema.tables ORDER BY table_name ");
            
            for(int i = 0;i<dt.Rows.Count;i++)
            {
                mcmbDB.Items.Add(dt.Rows[i][0]);
            }
           
            dt.Clear();
            dt.Dispose();
            
        }

        private void mbntFechar_Click(object sender, EventArgs e)
        {
            inicio = new Form1();
            this.Close();
            inicio.Show();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            string menssagem = "";
            if (!Directory.Exists("\\Users\\"+ System.Environment.UserName+"\\Documents\\File"))
            {
                Directory.CreateDirectory("\\Users\\"+ System.Environment.UserName+"\\Documents\\File");
            }
            if (!Directory.Exists("\\Users\\"+ System.Environment.UserName+"\\Documents\\File\\Excel"))
            {
                Directory.CreateDirectory("\\Users\\"+ System.Environment.UserName+"\\Documents\\File\\Excel");
            }
            if (File.Exists("\\Users\\"+ System.Environment.UserName+"\\Documents\\File\\Excel" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "_Geocodereverso.xlsx"))
            {
               if (MessageBox.Show ("Deseja substituir?", "Exporta excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
               {
                   File.Delete("/File/Excel/" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "_Geocodereverso.xlsx");
                   menssagem = Classes.excel.ExportToExcel(dtCoordinates, "\\Users\\"+ System.Environment.UserName+"\\Documents\\File\\Excel\\" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "_Geocodereverso.xlsx");
               }
               
            }
            else
            {
                menssagem = Classes.excel.ExportToExcel(dtCoordinates, "\\Users\\"+ System.Environment.UserName+"\\Documents\\File\\Excel\\" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "_Geocodereverso.xlsx");
            }

            
            

           
        }

        private  void mbntGeocode_Click(object sender, EventArgs e)
        {

            bgWork.RunWorkerAsync();


        }
       
        public DataTable limpaData(DataTable table)
        {
            for (int counter = table.Columns.Count - 1; counter >= 0; counter--)
            {
                table.Columns.RemoveAt(counter);
            }
            return table;
        }

        private  void mcmbDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(mcmbDB.SelectedIndex!=0)
                {
                    cmb = mcmbDB.SelectedItem.ToString();                   
                    grdResult.ClearSelection();
                    grdResult.DataSource = conexao.SELECT("SELECT * FROM " + cmb); ;

                }
                
            }           
            catch (Exception ex)
            {
                MessageBox.Show("O processo falhou: " + ex.ToString());
            }
            
            
            
            
        }
       

        private void metroProgressSpinner1_Click(object sender, EventArgs e)
        {

        }

        private void mbntCancelar_Click(object sender, EventArgs e)
        {
            bgWork.CancelAsync();

        }

        private void mcmbFonte_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                fonte = mcmbFonte.SelectedIndex;
            
            
        }
       
        private void bgWork_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataSet dsResult = new DataSet();//Data set para guarda as tabelas do request url
                geocode = new Classes.Geocode();  //instaciando objeto da classe geocode
                format = new Classes.formatResult();// instaciando objeto da classe formatResult

                string docxml="";
                string erro = "0";//string para reporta erros
                int busca = -1;//instancia da variavel de busca;
                bool validabusca;//variavel para verificar se  vai alterar os dados no banco ou permancer se houver algum
                string url = string.Empty;//inicializar url vazia para fazer request
                string[] retorno =new string[12];
                string numero=string.Empty;
                string logradouro = string.Empty;
                string bairro = string.Empty;
                string cidade = string.Empty;
                string estado = string.Empty;
                string pais = string.Empty;
                string cep = string.Empty;
                string lat = string.Empty;
                string lng = string.Empty;
                string precisao = string.Empty;
      

                
               //cria datatable na memoria
                DataTable dt = new DataTable("Dados");
                dt = Dados();//carrega o datatable com os dados do grid
               
                //verifica se tem registro e se tem uma fonte selecionada
                if (dt.Rows.Count > 0 && cmb != "Selecione uma tabela" && fonte != 0)
                {


                    existcolunm=validar.existColunm(dt,pk);//verifica se a coluna existe no DataTable             


                    //instancia tabela na memoria RAM
                    DataTable datageo = new DataTable();
                    // Busca dados da tabela ENDERECO_LOTE cadastrados e ordenados pelo codigo
                    datageo = conexao.SELECT("SELECT * FROM "+tblEnder+" ORDER BY "+fk+" ASC");// seleciona dados geocodificados do banco de dados        

                    //verifica se tem a coluna 
                    bool contemXY = validar.existColunm(dt, colX, colY);

                    
                   
                    //limpa coluna da tabela na memoria RAM
                    dtCoordinates = new DataTable();


                    //verifica se possui coluna
                    if(dtCoordinates.Columns.Count<=0)
                    {
                        //verifica se existe a coluna "Id"
                        if(!validar.existColunm(dtCoordinates,"Id"))
                        {
                            //cria as clunas com uma chave estrageira de fk
                            dtCoordinates.Columns.AddRange(new DataColumn[15] 
                            { 
                                     new DataColumn("Id", typeof(int)),
                                     new DataColumn("BAIRRO", typeof(string)),
                                     new DataColumn("LOGRADOURO", typeof(string)),
                                     new DataColumn("NUM", typeof(string)),
                                     new DataColumn("MUNICIPIO", typeof(string)),
                                     new DataColumn("COMPLEMENTO", typeof(string)),
                                     new DataColumn("CEP", typeof(string)),
                                     new DataColumn("UF", typeof(string)),
                                     new DataColumn("PAIS", typeof(string)),
                                     new DataColumn("Latitude",typeof(string)),
                                     new DataColumn("Longitude",typeof(string)),
                                     new DataColumn("Fonte",typeof(string)),
                                     new DataColumn("Precisao",typeof(string)),
                                     new DataColumn("validacao",typeof(bool)),
                                     new DataColumn(fk,typeof(string))
                            });
                        }
                    
                       
                     }
                    
                    
                    //verifica se a variavel é verdadeira se tem a coluna X (longitude) e Y(Latitude)
                    if (contemXY)
                    {

                        //loop  numero de linhas
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            try
                            {
                                //operação para canlar o loop
                                if(bgWork.CancellationPending)
                                {
                                    e.Cancel = true;
                                    break;
                                }
                                else
                                {
                                    validabusca = false;
                                    busca = -1;//inicializa a variavel todas as vezes que repetir
                                    tarefa();//sleep  intervalo de request
                                    bgWork.ReportProgress((100 * i) / dt.Rows.Count);//acrescenta na barra de progresso

                                    
                                    dt.Rows[i][colY] = dt.Rows[i][colY].ToString().Trim();//remove espaço do valor da coluna Y na posição i
                                    dt.Rows[i][colX] = dt.Rows[i][colX].ToString().Trim();//remove espaço do valor da coluna Y na posição i

                                    busca = validar.BuscaBinaria(datageo, Convert.ToInt32(dt.Rows[i][pk]), fk);//busca no data table se existe o registro
                                    if(busca != -1)//veririca se acho o registro
                                    {
                                        if (datageo.Rows[busca]["FONTE"].ToString() != geocode.getFonte(fonte) && datageo.Rows[busca]["LAT"].ToString() != dt.Rows[i][colY].ToString() && datageo.Rows[busca]["LNG"].ToString() != dt.Rows[i][colX].ToString())
                                        {
                                            if (datageo.Rows[busca]["NUM"].ToString() == "NULL" || datageo.Rows[busca]["NUM"].ToString() == "")//verifica a precisao do registro encontrado é diferente de ROOFTOP
                                            {
                                                docxml = geocode.geocodeReverso(fonte, dt.Rows[i][colY].ToString(), dt.Rows[i][colX].ToString(), email);//faz o geocodereverso e retorna o dataset
                                                fontegeo = geocode.getFonte();//pega a fonte em texto
                                                validabusca = true;


                                            }
                                            else if (datageo.Rows[busca]["LOGRADOURO"].ToString() == "NULL" || datageo.Rows[busca]["LOGRADOURO"].ToString() == "")
                                            {
                                                docxml = geocode.geocodeReverso(fonte, dt.Rows[i][colY].ToString(), dt.Rows[i][colX].ToString(), email);//faz o geocodereverso e retorna o dataset
                                                fontegeo = geocode.getFonte();//pega a fonte em texto
                                                validabusca = true;
                                            }
                                            
                                            else
                                            {
                                                continue;//passa para o proximo posição não é necessario geocodificar
                                            }

                                        }
                                        else
                                        {
                                            continue;
                                        }
                                       
                                        
                                    }
                                    else
                                    {
                                        docxml = geocode.geocodeReverso(fonte, dt.Rows[i][colY].ToString(), dt.Rows[i][colX].ToString(), email);//faz o geocodereverso e retorna o dataset
                                        fontegeo = geocode.getFonte();//pega a fonte em texto
                                        validabusca=false;//

                                    }



                                    retorno = format.resultJsonReversoGeo(docxml, fonte, dt.Rows[i][pk].ToString(), fk);

                                    if(validabusca)
                                    {
                                     //   new DataColumn("BAIRRO", typeof(string)),
                                     //new DataColumn("LOGRADOURO", typeof(string)),
                                     //new DataColumn("NUM", typeof(string)),
                                     //new DataColumn("MUNICIPIO", typeof(string)),
                                     //new DataColumn("COMPLEMENTO", typeof(string)),
                                     //new DataColumn("CEP", typeof(string)),
                                     //new DataColumn("UF", typeof(string)),
                                     //new DataColumn("PAIS", typeof(string)),
                                     //new DataColumn("Latitude",typeof(string)),
                                     //new DataColumn("Longitude",typeof(string)),
                                     //new DataColumn("Fonte",typeof(string)),
                                     //new DataColumn("Precisao",typeof(string)),
                                     //new DataColumn("validacao",typeof(bool)),
                                     //new DataColumn(fk,typeof(string))
                                        if ( retorno[1] != "NULL" && retorno[2] != "NULL" && retorno[3] != "NULL")
                                        {
                                            dtCoordinates.Rows.Add(i, validar.formatUTF8(retorno[0]), validar.formatUTF8(retorno[1]), validar.formatUTF8(retorno[2]), validar.formatUTF8(retorno[3]), validar.formatUTF8(retorno[4]), validar.formatUTF8(retorno[5]), validar.formatUTF8(retorno[6]), validar.formatUTF8(retorno[7]), validar.formatUTF8(retorno[8]), validar.formatUTF8(retorno[9]), validar.formatUTF8(retorno[10]), validar.formatUTF8(retorno[11]), validabusca, validar.formatUTF8(retorno[12]));
                                        }
                                        else
                                        {

                                        }


                                    }
                                    else
                                    {
                                        dtCoordinates.Rows.Add(i, validar.formatUTF8(retorno[0]), validar.formatUTF8(retorno[1]), validar.formatUTF8(retorno[2]), validar.formatUTF8(retorno[3]), validar.formatUTF8(retorno[4]), validar.formatUTF8(retorno[5]), validar.formatUTF8(retorno[6]), validar.formatUTF8(retorno[7]), validar.formatUTF8(retorno[8]), validar.formatUTF8(retorno[9]), validar.formatUTF8(retorno[10]), validar.formatUTF8(retorno[11]), validabusca, validar.formatUTF8(retorno[12]));
                                    }

                                    

                                   
                                    
                                        //dtCoordinates.Rows.Add(i,format.resultReversoGeo(dsResult, fonte, dt.Rows[i][pk].ToString(), fk).ToString());
                                    
                                    if(retorno[2].Contains("OVER_QUERY_LIMIT"))
                                    {
                                        MessageBox.Show("Limite diario ultrapassado");
                                        break;
                                    }
                                    

                                            //if (fonte == 1)//fonte 1 google
                                            //{
                                                
                                            //    if (dsResult.Tables["GeocodeResponse"].Rows[0]["status"].ToString() == "OK")//verifica se retorno algo
                                            //    {
                                            //        int media = 0;

                                            //        if (dsResult.Tables["address_component"].Rows[0][1].ToString().Contains("-"))
                                            //        {
                                            //            media = validar.calcMedia(dsResult.Tables["address_component"].Rows[0][1].ToString().Split('-'));
                                            //            //////////////////////id///////////////////////////bairro/////////////////////////////////////logradouro////////////////////////////////////////////numero//////////////////////////////////////////////////////////////////municipio///////////////////////////////////comlemento//////////////cep//////////////////////////////////////////////////////////////////Estado////////////////////////////////////lat///////////////////////////////////////////long////////////////////////////////////////fonte////////////cod_lote/////////////
                                            //            ///adiciona os dados no datatable
                                            //            dtCoordinates.Rows.Add(i, dsResult.Tables["address_component"].Rows[2][0].ToString(), dsResult.Tables["address_component"].Rows[1][0].ToString(), media, dsResult.Tables["address_component"].Rows[3][0].ToString(), "NULL", dsResult.Tables["address_component"].Rows[7][0].ToString(), dsResult.Tables["address_component"].Rows[5][0].ToString(), dsResult.Tables["location"].Rows[0][1].ToString(), dsResult.Tables["location"].Rows[0][0].ToString(), "Google", dsResult.Tables["geometry"].Rows[0][1].ToString(), validabusca, dt.Rows[i][pk].ToString());

                                            //        }
                                            //        else
                                            //        {
                                            //            //////////////////////id///////////////////////////bairro/////////////////////////////////////logradouro////////////////////////////////////////////numero//////////////////////////////////////////////////////////////////municipio///////////////////////////////////comlemento//////////////cep//////////////////////////////////////////////////////////////////Estado////////////////////////////////////lat///////////////////////////////////////////long////////////////////////////////////////fonte////////////cod_lote/////////////
                                            //            ///adiciona os dados no datatable
                                            //            dtCoordinates.Rows.Add(i, dsResult.Tables["address_component"].Rows[2][0].ToString(), dsResult.Tables["address_component"].Rows[1][0].ToString(), dsResult.Tables["address_component"].Rows[0][1].ToString(), dsResult.Tables["address_component"].Rows[3][0].ToString(), "NULL", dsResult.Tables["address_component"].Rows[7][0].ToString(), dsResult.Tables["address_component"].Rows[5][0].ToString(), dsResult.Tables["location"].Rows[0][0].ToString(), dsResult.Tables["location"].Rows[0][1].ToString(), "Google", dsResult.Tables["geometry"].Rows[0][1].ToString(), validabusca, dt.Rows[i][pk].ToString());

                                            //        }
                                                    
                                            //    }
                                            //    else if (dsResult.Tables["GeocodeResponse"].Rows[0]["status"].ToString() == "OVER_QUERY_LIMIT")//verifica se ultrapasso o limite diario de request
                                            //    {
                                            //        MessageBox.Show("limite excedido");
                                            //        break;
                                            //    }
                                            //    else
                                            //    {
                                            //        //adiciona null se algo nao achar
                                            //        dtCoordinates.Rows.Add(i, "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", validabusca, dt.Rows[i][pk].ToString());
                                            //    }

                                            //}
                                            //else if (fonte == 2)// 2 fonte Nominatim
                                            //{
                                            //    if (dsResult.Tables["reversegeocode"].Rows[0][0].ToString() == "0")
                                            //    {
                                                   
                                            //            for (int j = 0; j < dsResult.Tables["result"].Rows.Count; j++)
                                            //            {

                                            //                //dsResult.Tables["addressparts"].Rows[0]["house_number"].ToString();//numero
                                            //                dsResult.Tables["addressparts"].Rows[0]["road"].ToString();//rua
                                            //                    dsResult.Tables["addressparts"].Rows[0]["town"].ToString();//cidade
                                            //                        dsResult.Tables["addressparts"].Rows[0]["county"].ToString();//bairro
                                            //                            dsResult.Tables["addressparts"].Rows[0]["state"].ToString();//estado
                                            //                            dsResult.Tables["addressparts"].Rows[0]["country"].ToString();//pais


                                            //                            dtCoordinates.Rows.Add(i, dsResult.Tables["addressparts"].Rows[0]["county"].ToString(), dsResult.Tables["addressparts"].Rows[0]["road"].ToString(),"NUMBER", dsResult.Tables["addressparts"].Rows[0]["town"].ToString(), "NULL", "NULL", dsResult.Tables["addressparts"].Rows[0]["state"].ToString(), dsResult.Tables["result"].Rows[j][4].ToString(), dsResult.Tables["result"].Rows[j][5].ToString(), "Nominatim", "", validabusca, dt.Rows[i][pk].ToString());


                                            //            }
                                                    
                                            //    }
                                            //    else
                                            //    {


                                            //        dtCoordinates.Rows.Add(i, "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL", "NULL" ,"NULL");
                                                    
                                                 
                                            //    }

                                            //}
                                            //else if(fonte == 3)
                                            //{
                                            //    if (dsResult.Tables["Response"].Rows[0]["StatusDescription"].ToString() == "OK")
                                            //    {
                                                    

                                            //        dsResult.Tables["GeocodePoint"].Rows[0]["CalculationMethod"].ToString();//precisao
                                            //        dsResult.Tables["Point"].Rows[0]["Latitude"].ToString();//lat
                                            //        dsResult.Tables["Point"].Rows[0]["Longitude"].ToString();//lomg
                                            //        dsResult.Tables["Address"].Rows[0]["AddressLine"].ToString();//rua,numero
                                            //        dsResult.Tables["Address"].Rows[0]["AdminDistrict"].ToString();// sigla estado
                                            //        //dsResult.Tables["Address"].Rows[0]["AdminDistrict2"].ToString();
                                            //        dsResult.Tables["Address"].Rows[0]["CountryRegion"].ToString();//pais
                                            //        dsResult.Tables["Address"].Rows[0]["FormattedAddress"].ToString();//ender completo
                                            //        dsResult.Tables["Address"].Rows[0]["Locality"].ToString();//cidade
                                            //        dsResult.Tables["Address"].Rows[0]["PostalCode"].ToString();//cep

                                            //        dtCoordinates.Rows.Add(i, dsResult.Tables["addressparts"].Rows[0]["county"].ToString(), dsResult.Tables["addressparts"].Rows[0]["road"].ToString(),"NUMBER", dsResult.Tables["addressparts"].Rows[0]["town"].ToString(), "NULL", "NULL", dsResult.Tables["addressparts"].Rows[0]["state"].ToString(), dsResult.Tables["result"].Rows[0][4].ToString(), dsResult.Tables["result"].Rows[0][5].ToString(), "Nominatim", "", validabusca, dt.Rows[i][pk].ToString());

                                            //    }


                                                
                                            //}


                                        
                                    
                                }                            

                                
                                

                                

                            }
                            catch (Exception exc)
                            {
                                erro = erro + "" + exc.Message;

                            }

                        }

                       
                        if (erro != "0")
                        {
                            MessageBox.Show("Erro:" + erro);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Tabela não possui coluna (X,Y). Selecione outra tabela porfavor");
                    }



                }



                //limpar linhas da tabela
                dt.Clear();
                //função para limpar colunas
                
                dt.Dispose();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }

            

        }

        private void bgWork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            mpbGeocode.Value = e.ProgressPercentage;
        }

        private void bgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                if(dtCoordinates.Rows.Count>0)
                {
                    MessageBox.Show("Operação Cancelada");
                    populatedrip(dtCoordinates);
                }
                else
                {
                    MessageBox.Show("Operação Cancelada! Não existe ou não precisam ser geocodeficados.");
                }
               
            }
            else
            {
                if (dtCoordinates.Rows.Count > 0)
                {
                    MessageBox.Show("Operação finalizada com sucesso.");
                    populatedrip(dtCoordinates);
                }
                else
                {
                    MessageBox.Show("Operação finalizada! Não existe ou não precisam ser geocodeficados.");
                }
               

            }
            
        }
        private void tarefa()
        {
            
            Thread.Sleep(200);
        }
       
        private void populatedrip(DataTable data)
        {

            grdResult.ClearSelection();
            grdResult.DataSource = data;
        }
        private int insertBD(string fontegeo)
        {
            int cont = conexao.returnLast(tblEnder, pkEnder);
            int verifica = 0;
            int result=-1;
            string erro = "";
            


            try
            {
                for (int i = 0; i < dtCoordinates.Rows.Count; i++)
                {
                    if(!dtCoordinates.Rows[i][0].ToString().Contains("Erro"))
                    {
                        if (Convert.ToBoolean(dtCoordinates.Rows[i]["validacao"]))
                        {
                            result = conexao.commandExec("UPDATE ENDERECO_LOTE SET BAIRRO=" + validar.prepDB(dtCoordinates.Rows[i]["BAIRRO"].ToString()) + ", LOGRADOURO=" + validar.prepDB(dtCoordinates.Rows[i]["LOGRADOURO"].ToString()) + ", NUM=" + validar.prepDB(dtCoordinates.Rows[i]["NUM"].ToString()) + ",MUNICIPIO=" + validar.prepDB(dtCoordinates.Rows[i]["MUNICIPIO"].ToString()) + ",UF=" + validar.prepDB(dtCoordinates.Rows[i]["UF"].ToString()) + ",CEP=" + validar.prepDB(dtCoordinates.Rows[i]["CEP"].ToString()) + ",COMPLEMENTO=" + validar.prepDB(dtCoordinates.Rows[i]["COMPLEMENTO"].ToString()) + ",PRECISAO=" + validar.prepDB(dtCoordinates.Rows[i]["PRECISAO"].ToString()) + ",LAT=" + validar.prepDB(dtCoordinates.Rows[i]["Latitude"].ToString()) + ",LNG=" + validar.prepDB(dtCoordinates.Rows[i]["Longitude"].ToString()) + ",FONTE=" + validar.prepDB(fontegeo) + " WHERE COD_LOTE_FK=" + dtCoordinates.Rows[i][fk].ToString());

                        }
                        else
                        {
                            result = conexao.commandExec("Declare @count int " +
                                                     "SELECT @count = COUNT(*) FROM " + tblEnder +
                                                     " IF(@count > 0) " +
                                                     " BEGIN " +
                                                     " INSERT INTO " + tblEnder + "([" + pkEnder + "],[BAIRRO],[LOGRADOURO],[NUM],[MUNICIPIO],[UF],[CEP],[COMPLEMENTO],[PRECISAO],[LAT],[LNG],[FONTE],[PAIS],[DATA_CONSULTA],[" + fk + "]) VALUES(((SELECT TOP 1 " + pkEnder + " FROM " + tblEnder + " ORDER BY COD_ENDERECO_LOTE_PK DESC) + 1)," + validar.prepDB(dtCoordinates.Rows[i]["BAIRRO"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["LOGRADOURO"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["NUM"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["MUNICIPIO"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["UF"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["CEP"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["COMPLEMENTO"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["PRECISAO"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["Latitude"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["Longitude"].ToString()) + "," + validar.prepDB(fontegeo) + "," + validar.prepDB(dtCoordinates.Rows[i]["PAIS"].ToString()) + ",'" + (DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day) + "'," + validar.prepDB(dtCoordinates.Rows[i][fk].ToString()) + ")" +
                                                     " END " +
                                                     " ELSE " +
                                                     " BEGIN " +
                                                     " INSERT INTO " + tblEnder + "([" + pkEnder + "],[BAIRRO],[LOGRADOURO],[NUM],[MUNICIPIO],[UF],[CEP],[COMPLEMENTO],[PRECISAO],[LAT],[LNG],[FONTE],[PAIS],[DATA_CONSULTA],[" + fk + "]) VALUES(1," + validar.prepDB(dtCoordinates.Rows[i]["BAIRRO"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["LOGRADOURO"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["NUM"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["MUNICIPIO"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["UF"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["CEP"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["COMPLEMENTO"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["PRECISAO"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["Latitude"].ToString()) + "," + validar.prepDB(dtCoordinates.Rows[i]["Longitude"].ToString()) + "," + validar.prepDB(fontegeo) + "," + validar.prepDB(dtCoordinates.Rows[i]["PAIS"].ToString()) + ",'" + (DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day) + "'," + validar.prepDB(dtCoordinates.Rows[i]["COD_LOTE_FK"].ToString()) + ")" +
                                                     " END");
                        }


                        if (result != 0 && result != -1)
                        {
                            verifica++;
                            result = -1;
                        }
                    }
                    else
                    {
                        erro = dtCoordinates.Rows[i][0] + " " + dtCoordinates.Rows[i][3] + "LOTE: " + dtCoordinates.Rows[i][dtCoordinates.Columns.Count - 1]+" |";
                    }
                    
                            
                  
                    
                   
                }
                if(erro!="")
                {
                    MessageBox.Show(erro);
                }

                return verifica;

            }
            catch(Exception ex)
            {
                return verifica;
                throw new Exception(ex.Message);
            }
           

        }

        private void mbtnBD_Click(object sender, EventArgs e)
        {
            if(existcolunm)
            {
                int result = insertBD(fontegeo);
                if (result == dtCoordinates.Rows.Count)
                {
                    MessageBox.Show("Gravado com sucesso.");
                    
                }
                else
                {
                    MessageBox.Show("Numero de linhas nao cadastradas:" + (dtCoordinates.Rows.Count - result));
                }
            }
            else
            {
                MessageBox.Show("Selecione uma tabela que possui a coluna"+pk);
            }
            
            
            
        }
        private DataTable Dados()
        {
            
            return  (DataTable)grdResult.DataSource;
            
        }

        private void grdResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
