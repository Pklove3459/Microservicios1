using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using MSInventario.Models;
using MSCompras.Models;

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

        public async Task<Compra[]> BusquedaCompra(int idCompra = -1)
        {
            Compra[] listaCompras;
            string url = urlServicio + "/compras/buscar?";

            if(idCompra >=0) 
            {
                url += "idCompra=" + idCompra;
            }
            try
            {
                string datosCompra = await client.GetStringAsync(url);
                listaCompras = JsonConvert.DeserializeObject<Compra[]>(datosCompra);
                return listaCompras;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError al obtener una respuesta.");
                Console.WriteLine("Error: {0}", ex.Message);
                return null;
            }
        }

        public async Task<Compra> DetallesCompra(int idCompra)
        {
            Compra compraDetallada;
            string url = urlServicio + "/compras/detalles/";

            if(idCompra >= 0)
            {
                url += idCompra;
            }
            try
            {
                string compraDetalle = await client.GetStringAsync(url);
                compraDetallada = JsonConvert.DeserializeObject<Compra>(compraDetalle);
                return compraDetallada;
            }
            catch(Exception ex)
            {
                Console.WriteLine("\nError al obtener una respuesta.");
                Console.WriteLine("Error: {0}", ex.Message);
                return null;
            }
        }
    }
}