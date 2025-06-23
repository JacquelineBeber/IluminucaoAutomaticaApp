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
            LigarCommand = new Command<Lampada>(async (lampada) => await LigarLampadaAsync(lampada));
            DesligarCommand = new Command<Lampada>(async (lampada) => await DesligarLampadaAsync(lampada));
            ExcluirCommand = new Command<Lampada>(async (lampada) => await ExcluirLampadaAsync(lampada));
            AtivarCommand = new Command<Lampada>(async (lampada) => await AtivarLampadaAsync(lampada));
        }

        public ICommand CarregarLampadasCommand { get; }
        public ICommand LigarCommand { get; }
        public ICommand DesligarCommand { get; }
        public ICommand ExcluirCommand { get; }
        public ICommand AtivarCommand { get; }

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
        private async Task LigarLampadaAsync(Lampada lampada)
        {
            try
            {
                lampada.Estado = "Ligada";
                OnPropertyChanged(nameof(LampadaPrincipal));
                await _lampadaService.LigarLampadaAsync();
                Debug.WriteLine($"[DEBUG] Lâmpada {lampada.Nome} ligada.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERRO] Falha ao ligar lâmpada {lampada.Nome}: {ex.Message}");
            }
        }
        private async Task DesligarLampadaAsync(Lampada lampada)
        {
            try
            {
                lampada.Estado = "Desligada";
                OnPropertyChanged(nameof(LampadaPrincipal));
                await _lampadaService.DesligarLampadaAsync();
                Debug.WriteLine($"[DEBUG] Lâmpada {lampada.Nome} desligada.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERRO] Falha ao desligar lâmpada {lampada.Nome}: {ex.Message}");
            }
        }
        private async Task ExcluirLampadaAsync(Lampada lampada)
        {
            try
            {
                var sucesso = await _lampadaService.ExcluirLampadaAsync(lampada.Id);
                OutrasLampadas.Remove(lampada);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERRO] Falha ao desligar lâmpada {lampada.Nome}: {ex.Message}");
            }
        }
        public async Task AtivarLampadaAsync(Lampada novaPrincipal)
        {
            try
            {
                // Desativa a atual principal
                if (LampadaPrincipal != null)
                    LampadaPrincipal.Ativa = false;

                // Ativa a nova
                novaPrincipal.Ativa = true;

                var sucesso = await _lampadaService.AtivarLampadaAsync(novaPrincipal.Id);

                await CarregarLampadasAsync(); // Recarrega tudo
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERRO] Falha ao ativar lâmpada {novaPrincipal.Nome}: {ex.Message}");
            }
        }
    }
}
