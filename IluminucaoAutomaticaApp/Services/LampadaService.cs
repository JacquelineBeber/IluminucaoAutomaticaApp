using IluminucaoAutomaticaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IluminucaoAutomaticaApp.Services
{
    class LampadaService: ILampadaService
    {
        private readonly HttpClient _httpClient;

        public LampadaService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://uklj62bzxe.execute-api.sa-east-1.amazonaws.com/Desenvolvimento/")
            };
        }

        public async Task<List<Lampada>> BuscarLampadasAsync()
        {
            try
            {
                //var lampadas = await _httpClient.GetFromJsonAsync<List<Lampada>>("lampada/listar_lampadas");
                //return lampadas ?? new List<Lampada>();
                var response = await _httpClient.GetAsync("lampada/listar_lampadas");
                var json = await response.Content.ReadAsStringAsync();

                var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                var lampadasJson = root.GetProperty("lampadas");

                var lampadas = JsonSerializer.Deserialize<List<Lampada>>(lampadasJson);
                return lampadas ?? new List<Lampada>();
            }
            catch
            {
                return new List<Lampada>();
            }
        }
    }
}
