using System;
using System.Collections.Generic;

#nullable disable

namespace MSCompras.Models
{
    public partial class ProductosCompra
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public double Costo { get; set; }
        public string Notas { get; set; }

        public virtual Compra IdCompraNavigation { get; set; }
    }
}
