using PruebaIngresoBibliotecario.Domain.Entities;
using PruebaIngresoBibliotecario.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Application.Interfaces 
{ 

    public interface IPrestamoService
    {
        Task<PrestamoDTO> CrearPrestamo(PrestamoDTO prestamoDto);
        Task<PrestamoDTO> ObtenerPrestamoPorId(Guid idPrestamo);
    }
}
