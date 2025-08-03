using System.Windows;

namespace VentaPro.View
{
    /// <summary>
    /// Lógica de interacción para AgregarProductoView.xaml
    /// </summary>
    public partial class AgregarProductoView : Window
    {
        public AgregarProductoView()
        {
            InitializeComponent();
        }

        private void CerrarVentana(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
