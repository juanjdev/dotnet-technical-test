using Microsoft.AspNetCore.Mvc;
using PruebaIngresoBibliotecario.Api.Models;
using PruebaIngresoBibliotecario.Domain.Entities;
using PruebaIngresoBibliotecario.Application.Interfaces;
using System;
using System.Threading.Tasks;
using System.Data.Common;

namespace PruebaIngresoBibliotecario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;
        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrestamo([FromBody] PrestamoDTO prestamoDto)
        {
            try
            {
                if (prestamoDto.TipoUsuario < 1 || prestamoDto.TipoUsuario > 3)
                {
                    return BadRequest(new { mensaje = "El campo tipoUsuario debe tener un valor válido (1, 2 o 3)." });
                }

                if (string.IsNullOrWhiteSpace(prestamoDto.Isbn))
                {
                    return BadRequest(new  { mensaje = "El campo ISBN es requerido y no puede estar vacío." });
                }

                if (string.IsNullOrWhiteSpace(prestamoDto.IdentificacionUsuario) || prestamoDto.IdentificacionUsuario.Length > 10)
                {
                    return BadRequest(new { mensaje = "El campo identificacionUsuario debe tener una longitud máxima de 10 caracteres y no puede estar vacío." });
                }

                var result = await _prestamoService.CrearPrestamo(prestamoDto);

                if(result == null)
                {
                    return BadRequest(new { mensaje = $"El usuario con identificacion {prestamoDto.IdentificacionUsuario} ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo" }
                    );
                }
                //var prestamoResponseDTO = new PrestamoDTO
                //{
                //    Id = result.Id,
                //    FechaMaximaDevolucion = result.FechaMaximaDevolucion
                //};
                
                return Ok(new
                {
                    id = result.Id,
                    fechaMaximaDevolucion = result.FechaMaximaDevolucion
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al procesar la solicitud");
            }           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrestamoById(Guid id)
        {
            try
            {
                var prestamo = await _prestamoService.ObtenerPrestamoPorId(id);
                if(prestamo == null)
                {
                    return NotFound(new { mensaje = $"El prestamo con id {id} no existe" });
                }

                return Ok(new
                {
                    id = prestamo.Id,
                    isbn = prestamo.Isbn,
                    identificacionUsuario = prestamo.IdentificacionUsuario,
                    tipoUsuario = prestamo.TipoUsuario,
                    fechaMaximaDevolucion = prestamo.FechaMaximaDevolucion
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
                
            }
            
        }

    }
}
