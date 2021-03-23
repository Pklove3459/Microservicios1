using System;
using System.Collections.Generic;

#nullable disable

namespace MSInventario.Models
{
    public partial class Fabricante
    {
        public Fabricante()
        {
            Dispositivos = new HashSet<Dispositivo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Dispositivo> Dispositivos { get; set; }
    }
}
