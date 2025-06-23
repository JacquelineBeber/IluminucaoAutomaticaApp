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
    class LampadaService : ILampadaService
    {
        private readonly HttpClient _httpClient;

        public LampadaService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://uklj62bzxe.execute-api.sa-east-1.amazonaws.com/Desenvolvimento/lampada/")
            };
        }

        public async Task<List<Lampada>> BuscarLampadasAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("listar_lampadas");
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

        public async Task<bool> LigarLampadaAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("ligar/app", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DesligarLampadaAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("desligar/app", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> ExcluirLampadaAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"remover/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AtivarLampadaAsync(string id)
        {
            try
            {
                var response = await _httpClient.PostAsync($"definir_lampada_ativa/{id}", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

    }
}
