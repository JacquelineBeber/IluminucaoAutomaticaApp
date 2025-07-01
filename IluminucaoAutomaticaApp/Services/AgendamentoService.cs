using IluminucaoAutomaticaApp.Models;
using System.Net.Http.Json;

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
    }
}
