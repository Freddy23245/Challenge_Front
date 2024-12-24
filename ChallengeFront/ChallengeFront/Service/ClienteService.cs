using ChallengeFront.Interfaces;
using ChallengeFront.Models;
using Newtonsoft.Json;
using System.Text;

namespace ChallengeFront.Service
{
    public class ClienteService:IClienteService
    {
        private readonly HttpClient _httpClient;
        public ClienteService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44371/api/");// https://localhost:7025/swagger/index.html
        }

        public async Task<ClienteViewModel> Get(int Id)
        {
            var response = await _httpClient.GetAsync($"/api/Clientes/Get?id={Id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<ClienteViewModel>(content);
                return cliente; // Retorna el cliente obtenido
            }

            // Manejar el caso de error
            return null;
        }

        public async Task<IEnumerable<ClienteViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/Clientes/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var clientes = JsonConvert.DeserializeObject<IEnumerable<ClienteViewModel>>(content);
                return clientes; // Devuelve la lista de clientes obtenida
            }

            // Manejar el caso de error, puedes devolver un valor por defecto, lanzar una excepción o devolver una lista vacía
            return Enumerable.Empty<ClienteViewModel>();
        }

        public async Task Insert(ClienteViewModel cliente)
        {
            // Convertir el cliente a un formato JSON
            var content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Clientes/Insert", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al agregar el cliente.");
            }
     
        }

        public async Task<IEnumerable<ClienteViewModel>> Search(string Name)
        {
            try
            {
                // Asegurarse de que el parámetro Name no esté vacío o nulo
                if (string.IsNullOrEmpty(Name))
                {
                    var allClientsUrl = "/api/Clientes/GetAll"; 
                    var allClientsResponse = await _httpClient.GetAsync(allClientsUrl);

                    if (allClientsResponse.IsSuccessStatusCode)
                    {
                        var allClientsContent = await allClientsResponse.Content.ReadAsStringAsync();
                        var allClients = JsonConvert.DeserializeObject<IEnumerable<ClienteViewModel>>(allClientsContent);
                        return allClients;
                    }
                    else
                    {
                        var errorMessage = await allClientsResponse.Content.ReadAsStringAsync();
                        throw new Exception($"Error al obtener todos los clientes: {errorMessage}");
                    }
                }
                var url = $"/api/Clientes/Search?Name={Uri.EscapeDataString(Name)}";
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var clientes = JsonConvert.DeserializeObject<IEnumerable<ClienteViewModel>>(content);
                    return clientes;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error en la búsqueda: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
                throw;
            }
        }

        public async Task Update(ClienteViewModel cliente)
        {
            try
            {
                // Validar si el cliente tiene datos requeridos
                if (cliente == null)
                {
                    throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
                }
                var content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

                var url = $"/api/Clientes"; // Asumiendo que la API usa el Id del cliente en la URL
                var response = await _httpClient.PutAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar el cliente: {errorMessage}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error: {ex.Message}");
            }
        }
    }
}
