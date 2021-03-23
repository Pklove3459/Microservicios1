using System;
using System.Collections.Generic;

#nullable disable

namespace MSClientes.Models
{
    public partial class Telefono
    {
        // public Telefono()
        // {
        //     Membresia = new HashSet<Membresia>();
        // }

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Telefono1 { get; set; }
        public int Tipo { get; set; }
        public sbyte EsPrincipal { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual ICollection<Membresia> Membresia { get; set; }
    }
}
