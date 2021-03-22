using System;
using System.Collections.Generic;

#nullable disable

namespace MSClientes.Models
{
    public partial class Reporte
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Asunto { get; set; }
        public string Notas { get; set; }
        public string Folio { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
    }
}
