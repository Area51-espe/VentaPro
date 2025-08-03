using System.Windows; // For Window, Application, RoutedEventArgs
using System.Windows.Controls; // Crucial for PasswordBox
using System.Windows.Input; // For MouseButtonEventArgs
using VentaPro.VistaModelo; // Assuming your ViewModel is in this namespace
using System.ComponentModel;
using System.Windows.Input;

namespace VentaPro.View
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent(); // This method initializes the components defined in XAML.
                                   // It's essential and must be called in the constructor.

            // Set the DataContext of the window to an instance of your LoginViewModel.
            // This allows the XAML bindings (e.g., Text="{Binding Username}") to work.
            this.DataContext = new LoginViewModel();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove(); // Allows dragging the window when the left mouse button is pressed.
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized; // Minimizes the window.
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Shuts down the entire application.
        }

        /// <summary>
        /// Handles the PasswordChanged event of the PasswordBox.
        /// This is necessary because PasswordBox does not have a bindable Text property for security reasons.
        /// It updates the Password property in the ViewModel with the SecureString from the PasswordBox.
        /// </summary>
        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                var passwordBox = (PasswordBox)sender;
                viewModel.Password = passwordBox.SecurePassword;
            }
        }




    }


}