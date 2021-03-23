using System;
using System.Collections.Generic;

#nullable disable

namespace MSCompras.Models
{
    public partial class Compra
    {
        // public Compra()
        // {
        //     ProductosCompras = new HashSet<ProductosCompra>();
        // }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdVendedor { get; set; }
        public int IdCliente { get; set; }
        public double Descuento { get; set; }
        public int? PlanPagos { get; set; }

        public virtual PlanesPago PlanPagosNavigation { get; set; }
        public virtual ICollection<ProductosCompra> ProductosCompras { get; set; }
    }
}
