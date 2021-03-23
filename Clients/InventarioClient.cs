using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using MSInventario.Models;

namespace GatewayTienda.Clients
{
    public class InventarioClient
    {
        HttpClient client;
        private string urlServicio = "";

        public InventarioClient()
        {
            urlServicio = Environment.GetEnvironmentVariable("URL_MS_INVENTARIO");
            client = new HttpClient();
        }

        public async Task<Dispositivo[]> BuscaDispositivo(string nombre = "", int idFabricante = -1)
        {
            Dispositivo[] values;
            string url = urlServicio + "/dispositivos/buscar?";
            bool pa = false;
            if(!String.IsNullOrEmpty(nombre)) 
            {
                url += "nombre=" + nombre;
                pa = true;
            }
            if(idFabricante >= 0) 
            {
                if(pa == true)
                {
                    pa = false;
                    url += "&";
                }
                url += "idFabricante=" + idFabricante;
            }
            try
            {
                string responseBody = await client.GetStringAsync(url);
                values = JsonConvert.DeserializeObject<Dispositivo[]>(responseBody);
                return values;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError al obtener una respuesta.");
                Console.WriteLine("Error: {0}", ex.Message);
                return null;
            }
        }
    }
}