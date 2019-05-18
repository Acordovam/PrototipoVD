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
    /// Lógica de interacción para Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private connection con = new connection();
        public Window2()
        {
            InitializeComponent();
            con.conectar();
            llenarCB(cbid, "select nomina.idnomina, nomina.fechaInicio from nomina;");
            llenarCB(cbconcepto, "select concepto.idconceptos, concepto.nombre from concepto;");
            llenarCB(cbempleado, "select empleado.idempleado, empleado.nombre from empleado;");
            llenarDGdetalle();
            con.desconectar();
        }
        public void llenarCB(ComboBox caja, string sql)
        {

            MySqlDataReader reader = con.consulta(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    caja.Items.Add(reader.GetString(0) + " " + reader.GetString(1));

                }
            }
            reader.Close();
        }
        public void llenarDGdetalle()
        {

            string dd = cbid.SelectionBoxItem.ToString().Substring(0, 1);
            string sql = "select detallenomina.idnomina, concepto.nombre, empleado.nombre, detallenomina.valor " +
                "from detallenomina inner join concepto on concepto.idconceptos=detallenomina.idconceptos inner " +
                "join empleado on empleado.idempleado=detallenomina.idempleado where detallenomina.idnomina='" + dd+"';";

            MySqlDataReader reader = con.consultaTabla(sql);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID Nomina");
            dt.Columns.Add("Concepto");
            dt.Columns.Add("Empleado");
            dt.Columns.Add("Valor");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dt.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                }
            }
            dgdetalle.ItemsSource = dt.DefaultView;
            reader.Close();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string idn = cbid.SelectionBoxItem.ToString().Substring(0, 1);
            string concepto = cbconcepto.SelectionBoxItem.ToString().Substring(0, 1);
            string empleado = cbempleado.SelectionBoxItem.ToString().Substring(0, 1);
            string valor = txtvalor.Text;
            con.conectar();
            con.consulta("insert into detallenomina values('"+idn+"', '"+concepto+"', '"+empleado+"', '"+valor+"');");
            llenarDGdetalle();
            txtvalor.Clear();
            con.desconectar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                con.conectar();
                llenarDGdetalle();
                con.desconectar();
            }catch(Exception p)
            {
                MessageBox.Show("No hay datos para esta nomina", "No hay datos", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
    }
