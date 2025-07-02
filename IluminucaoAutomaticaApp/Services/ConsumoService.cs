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
                BaseAddress = new Uri("https://uklj62bzxe.execute-api.sa-east-1.amazonaws.com/Desenvolvimento/consumo/")
            };
        }

        public async Task<List<Consumo>> BuscarConsumoAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("listar");

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
                var response = await _httpClient.GetAsync($"diario/{data:dd-MM-yyyy}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                var consumoDiarioJson = root.GetProperty("consumo");

                var consumoDiario = JsonSerializer.Deserialize<MonitorarConsumo>(consumoDiarioJson);
                
                return consumoDiario?? new MonitorarConsumo();
            }
            catch (Exception ex)
            {
                return new MonitorarConsumo();
            }
        }

        public async Task<MonitorarConsumo> BuscarConsumoMensalAsync(int ano, int mes)
        {
            try
            {
                var response = await _httpClient.GetAsync($"mensal/{ano}/{mes}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                var consumoMensalJson = root.GetProperty("consumo");

                var consumoMennsal = JsonSerializer.Deserialize<MonitorarConsumo>(json);
                
                return consumoMennsal?? new MonitorarConsumo();
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
                var response = await _httpClient.GetAsync($"anual/{ano}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                var consumoAnualJson = root.GetProperty("consumo");

                var consumoAnual = JsonSerializer.Deserialize<MonitorarConsumo>(json);
                    
                return  consumoAnual?? new MonitorarConsumo();
            }
            catch (Exception ex)
            {
                return new MonitorarConsumo();
            }
        }
    }
}