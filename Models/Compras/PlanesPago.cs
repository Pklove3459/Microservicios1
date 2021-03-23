using System;
using System.Collections.Generic;

#nullable disable

namespace MSCompras.Models
{
    public partial class PlanesPago
    {
        // public PlanesPago()
        // {
        //     Compras = new HashSet<Compra>();
        // }

        public int Id { get; set; }
        public int? Mensualidades { get; set; }
        public string Notas { get; set; }
        public int Tipo { get; set; }

        public virtual TipoPlanPago TipoNavigation { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
