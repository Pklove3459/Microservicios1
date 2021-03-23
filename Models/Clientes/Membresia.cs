using System;
using System.Collections.Generic;

#nullable disable

namespace MSClientes.Models
{
    public partial class Membresia
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int Tipo { get; set; }
        public DateTime FechaMembresia { get; set; }
        public DateTime UltimaRenovacion { get; set; }
        public int? IdTelefono { get; set; }
        public int? IdCorreo { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Correo IdCorreoNavigation { get; set; }
        public virtual Telefono IdTelefonoNavigation { get; set; }
    }
}
