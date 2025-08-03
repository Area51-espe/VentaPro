using System;
using System.Collections.Generic;

namespace VentaPro.Models
{
    public interface IVentaRepository
    {
        void Add(VentaModel venta);
        void Edit(VentaModel venta);
        void Remove(int ventaId);
        VentaModel GetById(int id);
        IEnumerable<VentaModel> GetAll();
        IEnumerable<VentaModel> GetReportes(DateTime fechaInicio, DateTime fechaFin);
    }
}
