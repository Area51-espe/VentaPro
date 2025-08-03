using System.Collections.Generic;

namespace VentaPro.Models
{
    interface IProductoRepository
    {
        void Add(ProductoModel producto);
        void Edit(ProductoModel producto);
        void Remove(string nombre);
        ProductoModel GetById(int id);
        IEnumerable<ProductoModel> GetAll();

        bool ExisteProducto(string nombre);
        bool ExisteProducto(string nombre, int productoId);
        void ActualizarEstado(int productoId, bool estado);
        IEnumerable<ProductoModel> GetProductosActivos();
    }
}
