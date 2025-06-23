using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class InicialPageViewModel : BaseViewModel
    {
        private readonly ILampadaService _lampadaService;

        public Lampada? LampadaPrincipal { get; set; }
        public ObservableCollection<Lampada> OutrasLampadas { get; set; } = new();

        public InicialPageViewModel()
        {
            _lampadaService = new LampadaService();
            CarregarLampadasCommand = new Command(async () => await CarregarLampadasAsync());
            _ = CarregarLampadasAsync(); // chamada automática ao iniciar
        }

        public ICommand CarregarLampadasCommand { get; }

        private async Task CarregarLampadasAsync()
        {
            try
            {
                var lista = await _lampadaService.BuscarLampadasAsync();

                Debug.WriteLine($"[DEBUG] Total de lâmpadas carregadas: {lista.Count}");

                foreach (var l in lista)
                    Debug.WriteLine($"[DEBUG] Lâmpada: {l.Nome}, Ativa: {l.Ativa}, Estado: {l.Estado}");

                LampadaPrincipal = lista.FirstOrDefault(l => l.Ativa);
                OnPropertyChanged(nameof(LampadaPrincipal));

                OutrasLampadas.Clear();
                foreach (var lamp in lista.Where(l => !l.Ativa))
                    OutrasLampadas.Add(lamp);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERRO] Falha ao carregar lâmpadas: {ex.Message}");
            }
        }
    }
}
