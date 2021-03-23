using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using MSClientes.Models;

namespace GatewayTienda.Clients
{
    public class ClientesClient
    {
        HttpClient client;
        private string urlServicio = "";

        public ClientesClient()
        {
            urlServicio = Environment.GetEnvironmentVariable("URL_MS_CLIENTES");
            client = new HttpClient();
        }

        public async Task<Cliente[]> BuscaClientes(string nombre = "", int idCliente = -1)
        {
            Cliente[] values;
            string url = urlServicio + "/clientes/buscar?";
            bool pa = false;
            if(!String.IsNullOrEmpty(nombre)) 
            {
                url += "nombre=" + nombre;
                pa = true;
            }
            if(idCliente >= 0) 
            {
                if(pa == true)
                {
                    pa = false;
                    url += "&";
                }
                url += "idCliente=" + idCliente;
            }
            try
            {
                string responseBody = await client.GetStringAsync(url);
                values = JsonConvert.DeserializeObject<Cliente[]>(responseBody);
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