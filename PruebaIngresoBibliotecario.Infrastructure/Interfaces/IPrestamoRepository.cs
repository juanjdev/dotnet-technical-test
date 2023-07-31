using PruebaIngresoBibliotecario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace PruebaIngresoBibliotecario.Infrastructure.Interfaces
{
    public interface IPrestamoRepository
    {
        Task<Prestamo> AddPrestamo(Prestamo prestamo);
        Task<Prestamo> GetPrestamoById(Guid id);
        Task<Prestamo> GetPrestamoByUsuario(string identificacionUsuario);
    }
}
