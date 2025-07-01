using IluminucaoAutomaticaApp.Models;
using System.Text.Json;

namespace IluminucaoAutomaticaApp.Services
{
    class ConsumoService : IConsumoService
    {
        private readonly HttpClient _httpClient;

        public ConsumoService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://uklj62bzxe.execute-api.sa-east-1.amazonaws.com/Desenvolvimento/lampada/")
            };
        }

        public async Task<List<Consumo>> BuscarConsumoAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("consumo");

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                
                var consumoJson = root.GetProperty("consumo_lampada").GetProperty("consumo");
                
                var consumo = JsonSerializer.Deserialize<List<Consumo>>(consumoJson);
                
                return consumo ?? new List<Consumo>();
            }
            catch (Exception ex)
            {
                return new List<Consumo>();
            }
        }

        public async Task<MonitorarConsumo> BuscarConsumoDiarioAsync(DateTime data)
        {
            try
            {
                var response = await _httpClient.GetAsync($"consumo/diario/{data:dd-MM-yyyy}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<MonitorarConsumo>(json) ?? new MonitorarConsumo();
            }
            catch (Exception ex)
            {
                return new MonitorarConsumo();
            }
        }

        public async Task<MonitorarConsumo> BuscarConsumoMensalAsync(int mes, int ano)
        {
            try
            {
                var response = await _httpClient.GetAsync($"consumo/mensal/ano-mes");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<MonitorarConsumo>(json) ?? new MonitorarConsumo();
            }
            catch (Exception ex)
            {
                return new MonitorarConsumo();
            }
        }

        public async Task<MonitorarConsumo> BuscarConsumoAnualAsync(int ano)
        {
            try
            {
                var response = await _httpClient.GetAsync($"consumo/anual/ano");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<MonitorarConsumo>(json) ?? new MonitorarConsumo();
            }
            catch (Exception ex)
            {
                return new MonitorarConsumo();
            }
        }
    }
}