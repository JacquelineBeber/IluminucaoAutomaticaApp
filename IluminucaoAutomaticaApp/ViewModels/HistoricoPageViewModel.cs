using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.Services;
using System.Collections.ObjectModel;


namespace IluminucaoAutomaticaApp.ViewModels
{
    class HistoricoPageViewModel : BaseViewModel
    {
        private readonly IHistoricoService _historicoService;
        public ObservableCollection<Historico> Historico { get; set; } = new();

        private bool _semHistorico;
        public bool SemHistorico
        {
            get => _semHistorico;
            set => SetProperty(ref _semHistorico, value);
        }
        public HistoricoPageViewModel()
        {
            _historicoService = new HistoricoService();
            _ = CarregarHistoricoAsync();
        }
        private async Task CarregarHistoricoAsync()
        {
            Carregando = true;
            var lista = await _historicoService.BuscarHistoricoAsync();

            if (lista != null && lista.Any())
            {
                var datasMomentoAcaoOrdenadas = lista
                    .Where(h => h.MomentoAcaoDataHora != null)
                    .OrderByDescending(h => h.MomentoAcaoDataHora)
                    .ToList();

                Historico.Clear();
                foreach (var item in datasMomentoAcaoOrdenadas)
                    Historico.Add(item);
            }

            SemHistorico = !Historico.Any();
            Carregando = false;
        }

    }
}
