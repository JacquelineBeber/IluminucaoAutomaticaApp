using IluminucaoAutomaticaApp.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace IluminucaoAutomaticaApp.Services
{
    class AgendamentoService : IAgendamentoService
    {
        private readonly HttpClient _httpClient;

        public AgendamentoService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://uklj62bzxe.execute-api.sa-east-1.amazonaws.com/Desenvolvimento/agendamento/")
            };
        }

        public async Task<bool> CadastrarAgendamentoAsync(Agendamento agendamento)
        {
            try
            {
                Console.WriteLine($"Sending request to create agendamento: {agendamento} -- {agendamento.Nome} at {agendamento.MomentoAcionamento} with action {agendamento.Acao}");
                var response = await _httpClient.PostAsJsonAsync("criar", agendamento);
                Console.WriteLine($"Response status code: {response.StatusCode}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Agendamento>> BuscarAgendamentosAsync()
        {
            try
            { 
                var response = await _httpClient.GetAsync($"listar");

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                var agendamentosJson = root.GetProperty("agendamentos");

                var agendamentos = JsonSerializer.Deserialize<List<Agendamento>>(agendamentosJson);

                return agendamentos ?? new List<Agendamento>();
            }
            catch (Exception ex)
            {
                return new List<Agendamento>();
            }
        }
    }
}
