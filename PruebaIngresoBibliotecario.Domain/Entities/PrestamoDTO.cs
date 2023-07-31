using System;

namespace PruebaIngresoBibliotecario.Api.Models
{
    public class PrestamoDTO
    {
        public Guid Id { get; set; }
        public string Isbn { get; set; }
        public string IdentificacionUsuario { get; set; }
        public int TipoUsuario { get; set; }
        public DateTime FechaMaximaDevolucion { get; set; }

    }    
}
