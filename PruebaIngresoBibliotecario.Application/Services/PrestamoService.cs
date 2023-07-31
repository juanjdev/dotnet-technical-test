using System;
using System.Threading.Tasks;
using PruebaIngresoBibliotecario.Api.Extensions;
using PruebaIngresoBibliotecario.Api.Models;
using PruebaIngresoBibliotecario.Application.Interfaces;
using PruebaIngresoBibliotecario.Domain.Entities;
using PruebaIngresoBibliotecario.Infrastructure.Interfaces;


namespace PruebaIngresoBibliotecario.Application.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository  _prestamoRepository;

        public PrestamoService(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
        }

        public async Task<PrestamoDTO> CrearPrestamo(PrestamoDTO prestamoDto)
        {
            if(prestamoDto.TipoUsuario < 1 || prestamoDto.TipoUsuario > 3)
            {
                throw new ArgumentException("Tipo de usuario no valido");
            }

            var existingPrestamo = await _prestamoRepository.GetPrestamoByUsuario(prestamoDto.IdentificacionUsuario);

            if(existingPrestamo != null && prestamoDto.TipoUsuario == 3)  
            {
                return null;
                //throw new ArgumentException($"El usuario con identificacion {prestamoDto.IdentificacionUsuario} ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo");
            }

            var prestamo = new Prestamo
            {
                Id = Guid.NewGuid(),
                Isbn = prestamoDto.Isbn,
                IdentificacionUsuario = prestamoDto.IdentificacionUsuario,
                TipoUsuario = (int)prestamoDto.TipoUsuario
            };

            prestamo.FechaMaximaDevolucion = CalcularFechaMaximaDevolucion(prestamo.TipoUsuario);

            await _prestamoRepository.AddPrestamo(prestamo);

            return new PrestamoDTO
            {
                Id = prestamo.Id,
                Isbn = prestamo.Isbn,
                IdentificacionUsuario = prestamo.IdentificacionUsuario,
                TipoUsuario = prestamo.TipoUsuario, 
                FechaMaximaDevolucion = prestamo.FechaMaximaDevolucion
            };
        }

        public async Task<PrestamoDTO> ObtenerPrestamoPorId(Guid idPrestamo)
        {
            var prestamo = await _prestamoRepository.GetPrestamoById(idPrestamo);

            if(prestamo == null)
            {
                //throw new ArgumentException($"El prestamo con id {idPrestamo} no existe");
                return null;
            }

            return new PrestamoDTO
            {
                Id = prestamo.Id,
                Isbn = prestamo.Isbn,
                IdentificacionUsuario = prestamo.IdentificacionUsuario,
                TipoUsuario = prestamo.TipoUsuario, 
                FechaMaximaDevolucion = prestamo.FechaMaximaDevolucion
            };
        }

        private DateTime CalcularFechaMaximaDevolucion(int tipoUsuario)
        {
            var currentDate = DateTime.Now;
            var workingDaysToAdd = tipoUsuario switch
            {
                1 => 10,
                2 => 8,
                3 => 7,
                _ => throw new ArgumentException("Tipo de usuario invalido")
            };

            var fechaMaxima = currentDate.AddWorkingDays(workingDaysToAdd);
            return fechaMaxima;
        }
    }
}
