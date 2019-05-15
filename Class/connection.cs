using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrototipoVD.Class
{
    class connection
    {
        string ip="127.0.0.1";
        string puerto="3306";
        string usuario="root";
        string password="alejandro198";
        string database="prototipobd";
        public MySqlConnection con;
        public MySqlDataReader reader;
       
        public DataTable dt = new DataTable();
        public bool conectar()
        {
            bool regreso = false;
            string coneccion = "datasource=" + ip + ";port=" + puerto + ";username=" + usuario + ";password=" + password + ";database=" + database + ";SslMode=Preferred;Pooling=True;";
            
            try
            {
                con = new MySqlConnection(coneccion);
                con.Open();
                regreso = true;
                //MessageBox.Show("Conección Asegurada y Exitosa", "Conección exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception e)
            {
                regreso= false;
                MessageBox.Show("Error al intentar conectar la Base de Datos, si el problema persiste comunicarse con el administrador:\n"+e, "Error en Conección", MessageBoxButton.OK, MessageBoxImage.Error);
               
            }


            return regreso;
        }

        public void desconectar()
        {
            try
            {
                con.Close();
            }catch(Exception e)
            {
                MessageBox.Show("No se pudo desconectar la Base de Datos. Si el error persiste comunicarse con el administrador:\n"+ e, "Error en Base de Datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public MySqlDataReader consulta(string sql)
        {
            try
            {
                MySqlCommand mySql = new MySqlCommand(sql, con);
                reader = mySql.ExecuteReader();
               // MessageBox.Show("Consulta ejecutada correctamente", "Correcto", MessageBoxButton.OK, MessageBoxImage.Information);
            }catch(Exception e){
                // MessageBox.Show("Consulta ejecutada Incorrectamente. Si el error persiste porfavor comunicarse con el administrador:\n"+sql+"\n\n"+e, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                //reader = null;
            }
            return reader;
        }
        public MySqlDataReader consultaTabla(string sql)
        {
            try
            {
                MySqlCommand mySql = new MySqlCommand(sql, con);
                reader = mySql.ExecuteReader();
                // MessageBox.Show("Consulta ejecutada correctamente", "Correcto", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
              //  MessageBox.Show("Consulta ejecutada Incorrectamente. Si el error persiste porfavor comunicarse con el administrador:\n" + sql + "\n\n" + e, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                //reader = null;
            }
            return reader;
        }

    }
}
