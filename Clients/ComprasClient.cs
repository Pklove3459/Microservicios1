using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using MSInventario.Models;

namespace GatewayTienda.Clients
{
    public class ComprasClient
    {
        HttpClient client;
        private string urlServicio = "";

        public ComprasClient()
        {
            urlServicio = Environment.GetEnvironmentVariable("URL_MS_COMPRAS");
            client = new HttpClient();
        }

        // public async Task<Compra[]> BuscaCompra(DateTime inicio, DateTime fin)
        // {
            
        // }
    }
}