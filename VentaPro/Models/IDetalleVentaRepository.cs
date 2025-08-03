using System.Collections.Generic;

namespace VentaPro.Models
{
    interface IDetalleVentaRepository
    {
        void Add(DetalleVentaModel detalle);
        void Edit(DetalleVentaModel detalle);
        void Remove(int detalleVentaId);
        DetalleVentaModel GetById(int id);
        IEnumerable<DetalleVentaModel> GetAll();

        IEnumerable<DetalleVentaModel> GetDetalleVenta(int ventaId);
    }
}
