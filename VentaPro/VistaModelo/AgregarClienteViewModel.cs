using System.Linq;
using System.Windows;
using System.Windows.Input;
using VentaPro.Models;
using VentaPro.Repositories;
using VentaPro.Utils;

namespace VentaPro.VistaModelo
{


    public class AgregarClienteViewModel : ViewModelBase
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteModel Cliente { get; set; }
        public ICommand GuardarClienteCommand { get; }
        public ICommand CerrarVentanaCommand { get; }







        // Constructor para nuevo cliente
        public AgregarClienteViewModel()
        {
            _clienteRepository = new ClienteRepository();
            Cliente = new ClienteModel { Activo = true }; // Por defecto activo
            GuardarClienteCommand = new ViewModelCommand(GuardarCliente);
            CerrarVentanaCommand = new ViewModelCommand(CerrarVentana);
        }

        // Constructor para editar cliente existente
        public AgregarClienteViewModel(ClienteModel clienteExistente) : this()
        {
            Cliente = clienteExistente;
        }

      


        private void GuardarCliente(object obj)
        {
            //if (!ValidarCampos())
            // return;

            if (!Validarcedula())
                return;




            // Normalizamos los valores
            string nombre = Cliente.Nombre?.Trim();
            string cedula = Cliente.Cedula?.Trim();
         
            string telefono = Cliente.Telefono.Trim();

            

           
            if (Cliente.Id == 0)
            {
                // Validación para nuevo cliente
                if (_clienteRepository.ExisteClienteNombre(nombre))
                {
                    MessageBox.Show("Ya existe un cliente con ese nombre.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (_clienteRepository.ExisteClienteCedula(cedula))
                {
                    MessageBox.Show("Ya existe un cliente con ese número de cédula.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                _clienteRepository.Add(Cliente);
                MessageBox.Show("Cliente agregado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                // Reiniciar Cliente o limpiar campos según sea necesario.
            }
            else
            {
                // Validación para edición, excluyendo el cliente actual
                if (_clienteRepository.ExisteClienteNombre(nombre, Cliente.Id))
                {
                    MessageBox.Show("Ya existe otro cliente con ese nombre.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (_clienteRepository.ExisteClienteCedula(cedula, Cliente.Id))
                {
                    MessageBox.Show("Ya existe otro cliente con ese número de cédula.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                _clienteRepository.Edit(Cliente);
                MessageBox.Show("Cliente editado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                // Cerrar ventana o actualizar UI según corresponda.
            }
        }


        public void CerrarVentana(object obj)
        {
            Window ventana = obj as Window;
            ventana?.Close();
        }



        ///funcion validad
        ///

        private bool Validarcedula()
        {
            if (string.IsNullOrWhiteSpace(Cliente.Nombre))
            {
                MessageBox.Show("El nombre es obligatorio.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Cliente.Telefono))
            {
                MessageBox.Show("Telefono Requerido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }


            if (string.IsNullOrWhiteSpace(Cliente.Cedula))
            {
                MessageBox.Show("La cédula es obligatoria.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!Cliente.Cedula.Trim().All(char.IsDigit))
            {
                MessageBox.Show("La cédula debe contener solo dígitos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!CedulaValidator.EsCedulaValida(Cliente.Cedula.Trim()))
            {
                MessageBox.Show("La cédula ingresada no es válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }


    }
}