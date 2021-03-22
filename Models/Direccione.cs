using System;
using System.Collections.Generic;

#nullable disable

namespace MSClientes.Models
{
    public partial class Direccione
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Domicilio { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
    }
}
