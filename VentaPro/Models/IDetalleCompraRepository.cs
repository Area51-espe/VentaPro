using System.Collections.Generic;

namespace VentaPro.Models
{
    public interface IDetalleCompraRepository
    {
        void Add(DetalleCompraModel detalle);
        void Edit(DetalleCompraModel detalle);
        void Remove(int detalleCompraId);
        DetalleCompraModel GetById(int id);
        IEnumerable<DetalleCompraModel> GetAll();

        IEnumerable<DetalleCompraModel> GetDetalleCompra(int compraId);
    }
}
