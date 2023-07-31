using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaIngresoBibliotecario.Domain.Entities
{
    public class Prestamo
    {
        public Guid Id { get; set; }
        public string Isbn { get; set; }
        public string IdentificacionUsuario { get; set; }
        public int TipoUsuario { get; set; }
        public DateTime FechaMaximaDevolucion { get; set; }

    }

}
