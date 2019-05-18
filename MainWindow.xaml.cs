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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using PrototipoVD.Class;
using MySql.Data.MySqlClient;

namespace PrototipoVD
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private connection conexion = new connection();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtuser.Text;
            string pass = txtpass.Password.ToString();
            try
            {
                conexion.conectar();
                MySqlDataReader reader = conexion.consultaTabla("select 1 from usuario where userd='"+usuario+"' and pass=aes_encrypt('"+pass+"', 'isis');");
                
                if (reader.HasRows)
                {
                    reader.Read();
                   if (reader.GetString(0) =="1")
                    {
                        conexion.desconectar();
                        reader.Close();
                        if (conexion.conectar() == true)
                        {
                            Principal nuevo = new Principal();
                            this.Hide();
                            conexion.desconectar();
                            nuevo.ShowDialog();
                            this.Show();
                        }
                        else
                        {

                        }
                    }

                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña Invalido", "Contraseña o Usuario Invalido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception es)
            {
                MessageBox.Show("Error al intentar entrar al sistema. Si el problema persiste comuniquese con el administrador "+es,"Error al logear",  MessageBoxButton.OK, MessageBoxImage.Error);
            }


            
            
          
        }

        private void Moverbarra_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
