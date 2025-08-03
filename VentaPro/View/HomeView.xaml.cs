using System.Windows.Controls;
using VentaPro.VistaModelo;

namespace VentaPro.View
{
    /// <summary>
    /// Lógica de interacción para HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

            this.DataContext = new HomeViewModel();
        }
    }
}
