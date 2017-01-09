using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
namespace Geocode.Classes
{
     static class excel
    {
        public static string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }
        public static DataTable import(string arquivo)
        {
            string ext = string.Empty;
            string aspas = "\"";
            string Conexao = string.Empty;
            for (int i = arquivo.Length - 1; i < arquivo.Length; i--)
            {
                if (arquivo[i] != '.')
                {
                    ext += arquivo[i];
                }
                else
                {
                    ext += "."; break;
                }
            }
            ext = Reverse(ext);
            if (ext == ".xls")
            {
                Conexao = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + arquivo + ";" + "Extended Properties=" + aspas + "Excel 8.0;HDR=YES" + aspas;
            }
            if (ext == ".xlsx")
            {
                Conexao = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + arquivo + ";" + "Extended Properties=" + aspas + "Excel 12.0;HDR=YES" + aspas;
            }
            System.Data.OleDb.OleDbConnection Cn = new System.Data.OleDb.OleDbConnection();
            Cn.ConnectionString = Conexao; Cn.Open();
            object[] Restricoes = { null, null, null, "TABLE" };
            DataTable DTSchema = Cn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, Restricoes);
            if (DTSchema.Rows.Count > 0)
            {
                string Sheet = DTSchema.Rows[0]["TABLE_NAME"].ToString();
                System.Data.OleDb.OleDbCommand Comando = new System.Data.OleDb.OleDbCommand("SELECT * FROM [" + Sheet + "]", Cn);
                DataTable Dados = new DataTable();
                System.Data.OleDb.OleDbDataAdapter DA = new System.Data.OleDb.OleDbDataAdapter(Comando);
                DA.Fill(Dados);
                Cn.Close(); return Dados;
            } return null;
        }
        public static string ExportToExcel(this DataTable Tbl, string ExcelFilePath = null)
         {  
                string menssagem = "";
                try
                {
                   
                    if (Tbl == null || Tbl.Columns.Count == 0)
                        throw new Exception("Tabela vazia");

                    // load excel, and create a new workbook
                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Workbooks.Add();

                    // single worksheet
                    Excel._Worksheet workSheet = excelApp.ActiveSheet;

                    // column headings
                    for (int i = 0; i < Tbl.Columns.Count; i++)
                    {
                        workSheet.Cells[1, (i+1)] = Tbl.Columns[i].ColumnName;
                    }

                    // rows
                    for (int i = 0; i < Tbl.Rows.Count; i++)
                    {
                        // to do: format datetime values before printing
                        for (int j = 0; j < Tbl.Columns.Count; j++)
                        {
                            workSheet.Cells[(i + 2), (j + 1)] = Tbl.Rows[i][j];
                        }
                    }

                    // check fielpath
                    if (ExcelFilePath != null && ExcelFilePath != "")
                    {
                        try
                        {
                            workSheet.SaveAs(ExcelFilePath);
                            excelApp.Quit();
                            menssagem = "Exportado com sucesso!";
                            
                        }
                        catch (Exception ex)
                        {
                            menssagem = "Erro ao tentar exporta o arquivo.|"+ex.Message;
                            return menssagem;
                            throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                + ex.Message);
                        }
                    }
                    else    // no filepath is given
                    {
                        excelApp.Visible = true;
                    }
                }
                catch(Exception ex)
                {
                    menssagem="Erro ao exporta arquivo:"+ex.Message;
                    return menssagem;
                    throw new Exception("ExportToExcel: \n" + ex.Message);
                    
                }
                return menssagem;
        }


    }
}
