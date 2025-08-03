using System.Windows;

namespace VentaPro.View
{
    /// <summary>
    /// Lógica de interacción para AgregarClienteView.xaml
    /// </summary>
    public partial class AgregarClienteView : Window
    {
        public AgregarClienteView()
        {
            InitializeComponent();
        }
        private void CerrarVentana(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
