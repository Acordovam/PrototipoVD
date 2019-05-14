using System;
using System.Collections.Generic;
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
using PrototipoVD.Class;

namespace PrototipoVD.Forms
{
    /// <summary>
    /// Lógica de interacción para nuevotransporte.xaml
    /// </summary>
    public partial class nuevotransporte : Window
    {
        connection con = new connection();
        public nuevotransporte()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string placa = txtplaca.Text;
            string desc = txtdescripcion.Text;
            con.conectar();
            con.consulta("insert into transporte values('"+placa+"', '"+desc+"');");
            con.desconectar();
        }
    }
}
