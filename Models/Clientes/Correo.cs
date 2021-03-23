using System;
using System.Collections.Generic;

#nullable disable

namespace MSClientes.Models
{
    public partial class Correo
    {
        // public Correo()
        // {
        //     Membresia = new HashSet<Membresia>();
        // }

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Correo1 { get; set; }
        public sbyte EsPrincipal { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual ICollection<Membresia> Membresia { get; set; }
    }
}
