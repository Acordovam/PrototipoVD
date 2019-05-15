using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using PrototipoVD.Class;

namespace PrototipoVD.Forms
{
    /// <summary>
    /// Lógica de interacción para detallefactura.xaml
    /// </summary>
    public partial class detallefactura : Window
    {
        public void llenarCB(ComboBox caja)
        {

            MySqlDataReader reader = con.consulta("select idfactura from factura;");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    caja.Items.Add(reader.GetString(0));

                }
            }
            reader.Close();
        }
        public void llenarCB2(ComboBox caja)
        {

            MySqlDataReader reader = con.consulta("select idproducto, descripcion from producto;");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    caja.Items.Add(reader.GetString(0)+" "+ reader.GetString(1));

                }
            }
            reader.Close();
        }
        public void llenarDGdetalle()
        {

            string dd = cbfactura.SelectionBoxItem.ToString().Substring(0, 1);
            string sql = "select detallefactura.idfactura, producto.descripcion, " +
                "detallefactura.cantidad from detallefactura inner join producto on " +
                "detallefactura.idproducto=producto.idproducto where idfactura = '"+dd+"';";

            MySqlDataReader reader = con.consultaTabla(sql);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID Factura");
            dt.Columns.Add("Producto");
            dt.Columns.Add("Cantidad");


            string habi = "";
            string fab = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {


                    dt.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                }
            }
            dgdetalleFactura.ItemsSource = dt.DefaultView;
            reader.Close();

        }

        private connection con = new connection();
        public detallefactura()
        {
            InitializeComponent();
            con.conectar();
            llenarCB(cbfactura);
            llenarCB2(cbproducto);
            con.desconectar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string id = cbfactura.SelectionBoxItem.ToString().Substring(0, 1);
            string prod = cbproducto.SelectionBoxItem.ToString().Substring(0, 1);
            string cant = txtcant.Text;
            con.conectar();
            con.consulta("insert into detallefactura values('"+id+"', '"+prod+"', '"+cant+"');");
            MySqlDataReader reader =con.consulta("select sum(detallefactura.cantidad * producto.precio) total from detallefactura, producto where detallefactura.idproducto=producto.idproducto and detallefactura.idfactura='"+id+"';");
            reader.Read();
            if (reader.HasRows)
            {
                string total= reader.GetString(0);
                lbltotal.Content = total;
                lbltotal.Visibility = Visibility.Visible;
                con.consulta("update factura set total='"+total+"' where idfactura='" + id + "';");
            }
           
            reader.Close();
            llenarDGdetalle();
            con.desconectar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cbfactura_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            con.conectar();
            string id = cbfactura.SelectionBoxItem.ToString().Substring(0, 1);
            MySqlDataReader reader = con.consulta("select sum(detallefactura.cantidad * producto.precio) total from detallefactura, producto where detallefactura.idproducto=producto.idproducto and detallefactura.idfactura='" + id + "';");
            reader.Read();
            if (reader.HasRows)
            {
                try
                {
                    string total = reader.GetString(0);
                    lbltotal.Content = total;
                    lbltotal.Visibility = Visibility.Visible;
                }
                catch (Exception ed)
                {
                    MessageBox.Show("No existen datos para esta facutra", "No existen datos", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
            }
            reader.Close();
            llenarDGdetalle();
            con.desconectar();
        }
    }
}
