using IluminucaoAutomaticaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
    }
}