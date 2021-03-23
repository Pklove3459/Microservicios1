using System;
using System.Collections.Generic;

#nullable disable

namespace MSInventario.Models
{
    public partial class Dispositivo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Fabricante { get; set; }
        public DateTime Lanzamiento { get; set; }
        public int Categoria { get; set; }

        public virtual Categoria CategoriaNavigation { get; set; }
        public virtual Fabricante FabricanteNavigation { get; set; }
    }
}
