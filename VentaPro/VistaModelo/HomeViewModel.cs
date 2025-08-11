using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VentaPro.Models;
using VentaPro.Repositories;

namespace VentaPro.VistaModelo
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IVentaRepository _ventaRepository;

        private int _totalClientes;
        public int TotalClientes
        {
            get => _totalClientes;
            set { _totalClientes = value; OnPropertyChanged(nameof(TotalClientes)); }
        }

        private decimal _ventasDia;
        public decimal VentasDia
        {
            get => _ventasDia;
            set { _ventasDia = value; OnPropertyChanged(nameof(VentasDia)); }
        }

        private decimal _ingresosMensuales;
        public decimal IngresosMensuales
        {
            get => _ingresosMensuales;
            set { _ingresosMensuales = value; OnPropertyChanged(nameof(IngresosMensuales)); }
        }

        private string _notificacionVenta;
        public string NotificacionVenta
        {
            get => _notificacionVenta;
            set { _notificacionVenta = value; OnPropertyChanged(nameof(NotificacionVenta)); }
        }

        private decimal _ultimaVentaMonto;
        public decimal UltimaVentaMonto
        {
            get => _ultimaVentaMonto;
            set { _ultimaVentaMonto = value; OnPropertyChanged(nameof(UltimaVentaMonto)); }
        }

        private string _ultimaVentaCliente;
        public string UltimaVentaCliente
        {
            get => _ultimaVentaCliente;
            set { _ultimaVentaCliente = value; OnPropertyChanged(nameof(UltimaVentaCliente)); }
        }

        private DateTime _fechaSeleccionada;
        public DateTime FechaSeleccionada
        {
            get => _fechaSeleccionada;
            set
            {
                _fechaSeleccionada = value;
                OnPropertyChanged(nameof(FechaSeleccionada));
                ConsultarVentasDia(); // Actualizar ventas cuando cambie la fecha
                ConsultarNotificacionVenta(); // Actualizar notificaciones
            }
        }

        private string _textoFechaSeleccionada;
        public string TextoFechaSeleccionada
        {
            get => _textoFechaSeleccionada;
            set { _textoFechaSeleccionada = value; OnPropertyChanged(nameof(TextoFechaSeleccionada)); }
        }

        public ICommand ActualizarFechaCommand { get; }

        public HomeViewModel()
        {
            _clienteRepository = new ClienteRepository();
            _ventaRepository = new VentaRepository();

            // Inicializar la fecha seleccionada con la fecha actual
            _fechaSeleccionada = DateTime.Today;
            ActualizarTextoFecha();

            ActualizarFechaCommand = new RelayCommand(ActualizarFecha);

            CargarClientes();
            ConsultarVentasDia();
            ConsultarIngresosMensuales();
            ConsultarNotificacionVenta();
        }

        private void ActualizarTextoFecha()
        {
            if (FechaSeleccionada.Date == DateTime.Today)
            {
                TextoFechaSeleccionada = "Ventas de Hoy";
            }
            else
            {
                TextoFechaSeleccionada = $"Ventas del {FechaSeleccionada:dd/MM/yyyy}";
            }
        }

        private void ActualizarFecha(object parameter)
        {
            // Este método se puede usar si necesitas forzar una actualización
            ConsultarVentasDia();
            ConsultarNotificacionVenta();
            ActualizarTextoFecha();
        }

        private void CargarClientes()
        {
            var clientes = _clienteRepository.GetAll();
            TotalClientes = clientes.Count();
        }

        private void ConsultarVentasDia()
        {
            try
            {
                var ventasDia = _ventaRepository.GetReportes(FechaSeleccionada, FechaSeleccionada);
                VentasDia = ventasDia.Sum(v => v.Total);
                ActualizarTextoFecha();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar ventas del día: " + ex.Message);
            }
        }

        private void ConsultarIngresosMensuales()
        {
            try
            {
                DateTime inicioMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);
                var ventasMes = _ventaRepository.GetReportes(inicioMes, finMes);
                IngresosMensuales = ventasMes.Sum(v => v.Total);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar ingresos mensuales: " + ex.Message);
            }
        }

        private void ConsultarNotificacionVenta()
        {
            var ventasDia = _ventaRepository.GetReportes(FechaSeleccionada, FechaSeleccionada)
                                            .OrderByDescending(v => v.FechaVenta)
                                            .ToList();
            if (ventasDia.Any())
            {
                var ultimaVenta = ventasDia.First();
                if (ultimaVenta.ClienteId.HasValue)
                {
                    var cliente = _clienteRepository.GetById(ultimaVenta.ClienteId.Value);
                    if (cliente != null)
                    {
                        Debug.WriteLine($"[ConsultarNotificacionVenta] Cliente encontrado: {cliente.Nombre}");
                        UltimaVentaCliente = cliente.Nombre;
                    }
                    else
                    {
                        Debug.WriteLine($"[ConsultarNotificacionVenta] No se encontró cliente para ID: {ultimaVenta.ClienteId.Value}");
                        UltimaVentaCliente = "Desconocido";
                    }
                }
                else
                {
                    Debug.WriteLine("[ConsultarNotificacionVenta] ClienteId es null, se usa 'Consumidor Final'");
                    UltimaVentaCliente = "Consumidor Final";
                }
                UltimaVentaMonto = ultimaVenta.Total;
            }
            else
            {
                UltimaVentaMonto = 0;
                UltimaVentaCliente = "";
            }
        }
    }

    // Clase auxiliar para el comando
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}