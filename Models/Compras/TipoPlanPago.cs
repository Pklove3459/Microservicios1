using System;
using System.Collections.Generic;

#nullable disable

namespace MSCompras.Models
{
    public partial class TipoPlanPago
    {
        // public TipoPlanPago()
        // {
        //     PlanesPagos = new HashSet<PlanesPago>();
        // }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Abreviacion { get; set; }

        public virtual ICollection<PlanesPago> PlanesPagos { get; set; }
    }
}
