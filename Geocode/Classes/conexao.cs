using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace Geocode.Classes
{
    class conexao
    {

         SqlConnection SqlConn = new SqlConnection();
        public DataTable SqlTable = new DataTable();

        public conexao()
        {
            //SqlConn.ConnectionString = "Data Source=10.101.0.41\\sqlexpress;Initial Catalog=CODEMIG_TESTE_V3;Persist Security Info=True;User ID=daniel.veiga;Password=daniel@123";
            SqlConn.ConnectionString = "Data Source=10.101.0.41\\SQLEXPRESS;Initial Catalog=CODEMIG_TESTE_V3;Persist Security Info=True;User ID=pedro.rezende;Password=pedro@123";
        }
       public int returnLast(string tabela,string id)
        {
           int value=-1;
           DataTable data = new DataTable();
           limpaData(data);
           data.Clear();
           data = SELECT("Select TOP 1 " + id + " FROM " + tabela + " ORDER BY " + id + " DESC");
           if(data.Rows.Count>0)
           {
               value = Convert.ToInt32(data.Rows[0][0]);
           }
           else
           {
               value = 0;
           }
           
                
           return value;
        }
        public int insertReturn(String command, String chave)
        {
            DataTable dt = new DataTable();
            try
            {
                // dt.Clear();
                //dt = limpaData(dt);
                SqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(command, SqlConn);
                da.Fill(dt);
                SqlConn.Close();
                if (dt.Rows.Count > 0)
                {
                    return (int)dt.Rows[dt.Rows.Count - 1][chave];
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                SqlConn.Close();
                string erro = ex.Message;
                return -1;
            }
        }
        public DataTable SELECT(String command)
        {
            DataTable dt = new DataTable();
            try
            {
               
                SqlConn.Open();
                dt.Clear();
                
                SqlDataAdapter da = new SqlDataAdapter(command, SqlConn);
                SqlConn.Close();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                
            }

            SqlConn.Close();
            return dt;


        }

        public DataTable limpaData(DataTable dt)
        {
            for (int counter = dt.Columns.Count - 1; counter >= 0; counter--)
            {
                dt.Columns.RemoveAt(counter);
            }
            return dt;
        }
        public int commandExec(String command)
        {
            try
            {
                this.SqlConn.Open();
                SqlCommand SqlEx = new SqlCommand(command, this.SqlConn);

                int rowInfected = SqlEx.ExecuteNonQuery();
                SqlConn.Close();

                if (rowInfected > 0)
                {
                    // HttpContext.Current.Response.Write("<script>alert('Tudo Certo' )</script>");
                    return rowInfected;
                }
                else
                {
                    SqlConn.Close();
                    return 0;// HttpContext.Current.Response.Write("<script>alert('Ops, Algo deu errado!')</script>");
                }
            }
            catch (Exception ex)
            {
                SqlConn.Close();
                string ERRO = ex.Message.ToString();
                return -1;
            }
            
        }

    }
}
