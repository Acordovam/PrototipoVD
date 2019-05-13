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
using PrototipoVD.Forms;

namespace PrototipoVD
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        private int AnchoMenu = 214;
        private int contr = 100;
        private connection con = new connection();
        private MySqlDataReader reader;

        public Principal()
        {

            InitializeComponent();
            try
            {
                con.conectar();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo conectar a la Base de Datos. Si el problema perciste porfavor comunicarse con el administrador: "+e, "Error de Conección", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            actualizarData();
        }
        public void ocultar(Grid cosa)
        {
            this.proveedor.Visibility = Visibility.Collapsed;
            this.cliente.Visibility = Visibility.Collapsed;
            this.producto.Visibility = Visibility.Collapsed;
            this.empleo.Visibility = Visibility.Collapsed;
            this.factura.Visibility = Visibility.Collapsed;
            this.bitacora.Visibility = Visibility.Collapsed;
            this.otros.Visibility = Visibility.Collapsed;
            this.Pedidos.Visibility = Visibility.Collapsed;
            cosa.Visibility = Visibility.Visible;
            
    }
        public void limpiar()
        {
            //Combo Box
            cbdepartamentoSubDepartamento.Items.Clear();
            cbtipoempleo.Items.Clear();
            cbsubdepartamento.Items.Clear();
            cbclienteF.Items.Clear();
            cbsucursalF.Items.Clear();
            cbempleadoF.Items.Clear();
            cbestadoF.Items.Clear();
     
        }
        
        public void actualizarData()
        {
            limpiar();
            llenarCB(cbdepartamentoSubDepartamento, "select iddepartamento, descripcion from departamento");
            llenarCB(cbtipoempleo, "select idtipoempleo, descripcion from tipoempleo");
            llenarCB(cbsubdepartamento, "select idsubdepartamento, descripcion from subdepartamento");

            llenarCB(cbclienteF, "select idcliente, nombre from cliente");
            llenarCB(cbsucursalF, "select idsucursal, nombre from sucursal");
            llenarCB(cbempleadoF, "select idempleado, nombre from empleado");
            llenarCB(cbestadoF, "select idestado, descripcion from estado");

            llenarDGEmpleado();
            llenarDGFactura();
        }
        public void limpiarText()
        {
            //TextBox
            txtidE.Clear();
            txtsubdepartamento.Clear();
            txttelefono.Clear();
            txtfolio.Clear();
            txtnombre.Clear();
            txtdireccion.Clear();
            txtemail.Clear();
            txtsalario.Clear();
            txtdpi.Clear();
            txttotalconF.Clear();
            txttotalsinF.Clear();


        }
        public void llenarCB(ComboBox caja, string sql)
        {
           
            reader= con.consulta(sql);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    caja.Items.Add(reader.GetString(0)+" "+reader.GetString(1));
                    // Ejemplo para mostrar en el listView1 :
                    //string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                    //var listViewItem = new ListViewItem(row);
                    //listView1.Items.Add(listViewItem);
                }
            }
            reader.Close();
        }

        public void llenarDGEmpleado()//DATA GRIED DE EMPLEADOS
        {
            string sql = "select empleado.idempleado, tipoempleo.descripcion, subdepartamento.descripcion, empleado.nombre, " +
                "empleado.direccion, empleado.telefono, empleado.email, empleado.folio, empleado.salario, " +
                "empleado.dpi from empleado, tipoempleo, subdepartamento where empleado.idtipoempleo=tipoempleo.idtipoempleo " +
                "and empleado.idsubdepartamento=subdepartamento.idsubdepartamento;";
            dgempleado.Items.Clear();
            reader = con.consulta(sql);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Empleo");
            dt.Columns.Add("Sub Departamento");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Dirección");
            dt.Columns.Add("Teléfono");
            dt.Columns.Add("Email");
            dt.Columns.Add("Folio");
            dt.Columns.Add("Salario");
            dt.Columns.Add("DPI");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dt.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), 
                    reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                }
            }reader.Close();
            dgempleado.ItemsSource = dt.DefaultView;
        }
        //DATA GRIED DE FACTURA
        public void llenarDGFactura()
        {
            string sql = "select factura.idfactura, cliente.nombre, sucursal.nombre, empleado.nombre, estado.descripcion, " +
                "factura.fecha, factura.totalIVA, factura.totalsinIVA from factura, cliente, sucursal, empleado, " +
                "estado where factura.idcliente=cliente.idcliente and factura.idsucursal=sucursal.idsucursal and " +
                "factura.idempleado=empleado.idempleado and estado.idestado=estado.idestado;";
            dgfactura.Items.Clear();
            reader = con.consulta(sql);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID Factura");
            dt.Columns.Add("Cliente");
            dt.Columns.Add("Sucursal");
            dt.Columns.Add("Empleado");
            dt.Columns.Add("Estado");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Total con IVA");
            dt.Columns.Add("Total sin IVA");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dt.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7));
                }
            }
            reader.Close();
            dgfactura.ItemsSource = dt.DefaultView;
        }
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            con.desconectar();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (menu.Width == AnchoMenu)
            {
                menu.Width = contr ;
            }
            else
            {
                menu.Width = AnchoMenu;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btnfactura_Click(object sender, RoutedEventArgs e)
        {
            ocultar(factura);
        }

        private void Btnproducto_Click(object sender, RoutedEventArgs e)
        {
            ocultar(producto);
        }

        private void Btnempleado_Click(object sender, RoutedEventArgs e)
        {
            ocultar(empleo);
        }

        private void Btnproveedor_Click(object sender, RoutedEventArgs e)
        {
            ocultar(proveedor);
        }

        private void Btncliente_Click(object sender, RoutedEventArgs e)
        {
            ocultar(cliente);
        }

        private void Btnpedido_Click(object sender, RoutedEventArgs e)
        {
            ocultar(Pedidos);
        }

        private void Btn_otros_Click(object sender, RoutedEventArgs e)
        {
            ocultar(otros);
        }

        private void Btnbitacora_Click(object sender, RoutedEventArgs e)
        {
            ocultar(bitacora);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btnguardarclas_Click(object sender, RoutedEventArgs e)
        {
            string clasificacion = txtclasificacion.Text;

            con.consulta("insert into clasificacion values('0', '"+clasificacion+"');");
        }

        private void Btnguardarconcepto_Click(object sender, RoutedEventArgs e)
        {
            string concepto = txtconcepto.Text;
            string afecta = cbafecta.SelectionBoxItem.ToString();
            con.consulta("insert into concepto values('0', '"+concepto+"', '"+afecta+"');");
        }

        private void Btnnuevodepartamento_Click(object sender, RoutedEventArgs e)
        {
            Window1 depart = new Window1();
            depart.ShowDialog();

        }

        private void BtnguardarRuta_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtnombreRuta.Text;
            string ruta = txtdescripcionRuta.Text;
            con.consulta("insert into ruta values('0', '"+nombre+"', '"+ruta+"');");

        }

        private void Btnguardarsubdepartamento_Click(object sender, RoutedEventArgs e)
        {
            string dept = cbdepartamentoSubDepartamento.SelectionBoxItem.ToString().Substring(0, 1);
            string nombre = txtsubdepartamento.Text;
            con.consulta("insert into subdepartamento values('0', '"+dept+"', '"+nombre+"');");
        }

        private void Btnguardarclas_Copy_Click(object sender, RoutedEventArgs e)
        {
            string nombre=txtnombreSucursal.Text;
            string direccion=txtdireccionSucursal.Text;
            string telefono=txttelefonoSucursal.Text;
            string email=txtemailSucursal.Text;
            con.consulta("insert into sucursal values('0', '"+nombre+"', '"+direccion+"', '"+telefono+"', '"+email+"')");
        }

        private void Btnnuevoempleo_Click(object sender, RoutedEventArgs e)
        {
            nuevoempleo nuevoempleo = new nuevoempleo();
            nuevoempleo.ShowDialog();
            actualizarData();
        }

        private void TxtguardarE_Click(object sender, RoutedEventArgs e)
        {
            string empleo = cbtipoempleo.SelectionBoxItem.ToString().Substring(0, 1);
            string subdept = cbsubdepartamento.SelectionBoxItem.ToString().Substring(0, 1);
            string tel = txttelefono.Text;
            string folio = txtfolio.Text;
            string nombre = txtnombre.Text;
            string dire = txtdireccion.Text;
            string email = txtemail.Text;
            string salario = txtsalario.Text;
            string dpi = txtdpi.Text;
            con.consulta("insert into empleado values('0', '"+empleo+"', '"+subdept+"', '"+nombre+"', '"+dire+"', '"+tel+"', '"+email+"', '"+folio+"', '"+salario+"', '"+dpi+"');");
            limpiarText();
            llenarDGEmpleado();
        }

        private void Btnnuevoestado_Click(object sender, RoutedEventArgs e)
        {
            nuevoestado estado = new nuevoestado();
            estado.ShowDialog();
            actualizarData();
        }

        private void BtnguardarF_Click(object sender, RoutedEventArgs e)
        {
            string id = txtidF.Text;
            string cliente = cbclienteF.SelectionBoxItem.ToString().Substring(0, 1);
            string sucursal = cbsucursalF.SelectionBoxItem.ToString().Substring(0, 1);
            string empleado = cbempleadoF.SelectionBoxItem.ToString().Substring(0, 1);
            string estado = cbestadoF.SelectionBoxItem.ToString().Substring(0, 1);
            string fecha = dpfechaF.SelectedDate.ToString().Substring(0, 10);
            string coniva = txttotalconF.Text;
            string siniva = txttotalsinF.Text;
            con.consulta("insert into factura values('0', '"+cliente+"', '"+sucursal+"', '"+empleado+"', '"+estado+"', '"+fecha+"', '"+coniva+"', '"+siniva+"');");
            limpiarText();
            llenarDGFactura();
        }
    }
}
