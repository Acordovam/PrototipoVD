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
    /// Lógica de interacción para nuevoestado.xaml
    /// </summary>
    public partial class nuevoestado : Window
    {
        private connection con = new connection();
        public nuevoestado()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            con.conectar();
            string estado = txtestado.Text;
            con.consulta("insert into estado values('0', '"+estado+"');");
            txtestado.Clear();
            con.desconectar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
