//using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.Domain.Entities;
using PruebaIngresoBibliotecario.Infrastructure.Interfaces;
using PruebaIngresoBibliotecario.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PruebaIngresoBibliotecario.Infrastructure.Repository
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly PersistenceContext _context;

        public PrestamoRepository(PersistenceContext context)
        {
            _context = context;
        }

        public async Task<Prestamo> AddPrestamo(Prestamo prestamo)
        {
            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
            return prestamo;
        }

        public async Task<Prestamo> GetPrestamoById(Guid id)
        {
            return await _context.Prestamos.FindAsync(id);
        }

        public async Task<Prestamo> GetPrestamoByUsuario(string identificacionUsuario)
        {
            return await _context.Prestamos.FirstOrDefaultAsync(p => p.IdentificacionUsuario == identificacionUsuario);
        }
    }
}
