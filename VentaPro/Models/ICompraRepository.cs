using System;
using System.Collections.Generic;

namespace VentaPro.Models
{
    public interface ICompraRepository
    {
        void Add(CompraModel compra);
        void Edit(CompraModel compra);
        void Remove(int compraId);
        CompraModel GetById(int id);
        IEnumerable<CompraModel> GetAll();
        IEnumerable<CompraModel> GetReportes(DateTime fechaInicio, DateTime fechaFin);

    }
}
