using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Timers;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class InicialPageViewModel : BaseViewModel
    {
        private readonly ILampadaService _lampadaService;
        private System.Timers.Timer _timer;

        public Lampada? LampadaPrincipal { get; set; }
        public ObservableCollection<Lampada> OutrasLampadas { get; set; } = new();

        public InicialPageViewModel()
        {
            _lampadaService = new LampadaService();
            CarregarLampadasCommand = new Command(async () => await CarregarLampadasAsync());
            _ = CarregarLampadasAsync();
            
            LigarCommand = new Command<Lampada>(async (lampada) => await LigarLampadaAsync(lampada));
            DesligarCommand = new Command<Lampada>(async (lampada) => await DesligarLampadaAsync(lampada));
            ExcluirCommand = new Command<Lampada>(async (lampada) => await ExcluirLampadaAsync(lampada));
            AtivarCommand = new Command<Lampada>(async (lampada) => await AtivarLampadaAsync(lampada));

            IniciarTimerAtualizacao();
        }

        public ICommand CarregarLampadasCommand { get; }
        public ICommand LigarCommand { get; }
        public ICommand DesligarCommand { get; }
        public ICommand ExcluirCommand { get; }
        public ICommand AtivarCommand { get; }

        private void IniciarTimerAtualizacao()
        {
            _timer = new System.Timers.Timer(1000); // 1000ms = 1s  100000000ms = 27H
            _timer.Elapsed += async (s, e) =>
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await CarregarLampadasAsync();
                });
            };
            _timer.AutoReset = true;
            _timer.Start();
        }

        public void PararTimer()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }

        private async Task CarregarLampadasAsync()
        {
            try
            {
                
                List<Lampada> lista = await _lampadaService.BuscarLampadasAsync();

                Lampada? novaLampadaPrincipal = lista.FirstOrDefault(l => l.Ativa);

                if (LampadaPrincipal == null || novaLampadaPrincipal == null || LampadaPrincipal.Id != novaLampadaPrincipal.Id || LampadaPrincipal.Estado != novaLampadaPrincipal.Estado)
                {
                    LampadaPrincipal = novaLampadaPrincipal;
                    OnPropertyChanged(nameof(LampadaPrincipal));
                }

                OutrasLampadas.Clear();
                    foreach (var lamp in lista.Where(l => !l.Ativa))
                        OutrasLampadas.Add(lamp);

                //bool houveMudancaNasOutras = lista
                //    .Where(l => !l.Ativa)
                //    .Count() != OutrasLampadas.Count ||
                //    lista
                //    .Where(l => !l.Ativa)
                //    .Zip(OutrasLampadas, (nova, atual) =>
                //        nova.Id != atual.Id || nova.Estado != atual.Estado)
                //    .Any(mudanca => mudanca);

                //if (houveMudancaNasOutras)
                //{
                //    OutrasLampadas.Clear();
                //    foreach (var lamp in lista.Where(l => !l.Ativa))
                //        OutrasLampadas.Add(lamp);
                //}
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
