using System;
using System.Collections.Generic;

#nullable disable

namespace MSClientes.Models
{
    public partial class Cliente
    {
        // public Cliente()
        // {
        //     Correos = new HashSet<Correo>();
        //     Direcciones = new HashSet<Direccione>();
        //     Membresia = new HashSet<Membresia>();
        //     Reportes = new HashSet<Reporte>();
        //     Telefonos = new HashSet<Telefono>();
        // }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Correo> Correos { get; set; }
        public virtual ICollection<Direccione> Direcciones { get; set; }
        public virtual ICollection<Membresia> Membresia { get; set; }
        public virtual ICollection<Reporte> Reportes { get; set; }
        public virtual ICollection<Telefono> Telefonos { get; set; }
    }
}
