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
    /// Lógica de interacción para nuevoempleo.xaml
    /// </summary>
    public partial class nuevoempleo : Window
    {
        private connection con = new connection();
        public nuevoempleo()
        {
            InitializeComponent();
            
        }

        private void btnguardarempleo(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string empleo = txtempleo.Text;
            con.conectar();
            con.consulta("insert into tipoempleo values('0','"+empleo +"');");
            txtempleo.Clear();
            con.desconectar();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
