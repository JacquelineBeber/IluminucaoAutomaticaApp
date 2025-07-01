using IluminucaoAutomaticaApp.Models;
using System.Text.Json;

namespace IluminucaoAutomaticaApp.Services
{
    class HistoricoService : IHistoricoService
    {
        private readonly HttpClient _httpClient;

        public HistoricoService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://uklj62bzxe.execute-api.sa-east-1.amazonaws.com/Desenvolvimento/historicolampada/")
            };
        }
        public async Task<List<Historico>> BuscarHistoricoAsync()
        {          
            try
            {
                var response = await _httpClient.GetAsync("listar_historico_lampada");
                
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                var historicoJson = root.GetProperty("historico_lampada").GetProperty("historico");

                var historico = JsonSerializer.Deserialize<List<Historico>>(historicoJson);

                return historico ?? new List<Historico>();
            }
            catch (Exception ex)
            {
                return new List<Historico>();
            }
        }
    }
}
