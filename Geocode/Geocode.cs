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
using System.IO;
namespace Geocode
{
    public partial class Geocode : MetroForm
    {
        Form1 inicio;
        Classes.Geocode geocode;
        Classes.validacao validar;
        Classes.formatResult format;
        string txtreslt;
        DataTable dtCoordinates;
        string path = "";
       
        const string colY = "LAT";
        
        const string colX = "LNG";
        string[] retorno = new string[12];

        public Geocode()
        {
            InitializeComponent();
        }

        private void mbntFechar_Click(object sender, EventArgs e)
        {
            inicio = new Form1();
            this.Close();
            inicio.Show();
        }

        private void mbntSelecionar_Click(object sender, EventArgs e)
        {
                mtxtArquivo.Text = "";
                openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;"; 
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                
                mtxtArquivo.Text = openFileDialog1.FileName;
                path = openFileDialog1.FileName;
               


                
                
            }
        }

        private void mbtnExcel_Click(object sender, EventArgs e)
        {
            if(dtCoordinates.Rows.Count>0)
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
                if (File.Exists("\\Users\\"+ System.Environment.UserName+"\\Documents\\File\\Excel" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "_GeocodereversoExcel.xlsx"))
                {
                   if (MessageBox.Show ("Deseja substituir?", "Exporta excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                   {
                       File.Delete("/File/Excel/" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "_Geocodereverso.xlsx");
                       menssagem = Classes.excel.ExportToExcel(dtCoordinates, "\\Users\\"+ System.Environment.UserName+"\\Documents\\File\\Excel\\" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "_GeocodereversoExcel.xlsx");
                   }
               
                }
                else
                {
                    menssagem = Classes.excel.ExportToExcel(dtCoordinates, "\\Users\\"+ System.Environment.UserName+"\\Documents\\File\\Excel\\" + DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "_GeocodereversoExcel.xlsx");
                }

                    
              }
            
        }

        private void mbntGeo_Click(object sender, EventArgs e)
        {
            txtreslt = "";
            dtCoordinates = new DataTable();
            validar = new Classes.validacao();
            format = new Classes.formatResult();
            DataTable dt = new DataTable();
            geocode = new Classes.Geocode();
            dt = Classes.excel.import(openFileDialog1.FileName);

            if (validar.existColunm(dt, colY) && validar.existColunm(dt, colX))
            {
                //cria as clunas com uma chave estrageira de fk
                dtCoordinates.Columns.AddRange(new DataColumn[13] 
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
                                     
                                     
                            });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][colY].ToString() != "" && dt.Rows[i][colX].ToString() != "")
                    {
                        dt.Rows[i][colY] = dt.Rows[i][colY].ToString().Trim();//remove espaço do valor da coluna Y na posição i
                        dt.Rows[i][colX] = dt.Rows[i][colX].ToString().Trim();//remove espaço do valor da coluna X na posição i
                        txtreslt = geocode.geocodeReverso(mcmbFonte.SelectedIndex, dt.Rows[i][colY].ToString(), dt.Rows[i][colX].ToString(), "ti@aryamap.com");





                        retorno = format.resultJsonReversoGeo(txtreslt, mcmbFonte.SelectedIndex);

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
                        if (!retorno[0].Contains("Erro") && !retorno[1].Contains("Erro") && retorno[1] != "NULL" && retorno[2] != "NULL" && retorno[3] != "NULL")
                        {
                            dtCoordinates.Rows.Add(i, validar.formatUTF8(retorno[0]), validar.formatUTF8(retorno[1]), validar.formatUTF8(retorno[2]), validar.formatUTF8(retorno[3]), validar.formatUTF8(retorno[4]), validar.formatUTF8(retorno[5]), validar.formatUTF8(retorno[6]), validar.formatUTF8(retorno[7]), validar.formatUTF8(retorno[8]), validar.formatUTF8(retorno[9]), validar.formatUTF8(retorno[10]), validar.formatUTF8(retorno[11]));
                        }
                        else
                        {
                            dtCoordinates.Rows.Add(i, validar.formatUTF8(retorno[0]), validar.formatUTF8(retorno[1]), validar.formatUTF8(retorno[2]), validar.formatUTF8(retorno[3]));
                        }



                    }




                }
            }
               



        }
    }
}
