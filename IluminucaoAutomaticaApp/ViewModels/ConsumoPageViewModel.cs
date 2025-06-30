using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.Services;
using System.Collections.ObjectModel;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class ConsumoPageViewModel : BaseViewModel
    {
        private readonly IConsumoService _consumoService;
        public ObservableCollection<Consumo> Consumo { get; set; } = new();

        private bool _semConsumo;
        public bool SemConsumo
        {
            get => _semConsumo;
            set => SetProperty(ref _semConsumo, value);
        }
        public ConsumoPageViewModel()
        {
            _consumoService = new ConsumoService();
            _ = CarregarConsumoAsync();
        }
        private async Task CarregarConsumoAsync()
        {
            Carregando = true;
            var lista = await _consumoService.BuscarConsumoAsync();
            if (lista != null && lista.Any())
            {
                var datasAcionamentoOrdenadas = lista
                    .Where(c => c.AcionamentoDataHora != null)
                    .OrderByDescending(c => c.AcionamentoDataHora)
                    .ToList();
                Consumo.Clear();
                foreach (var item in datasAcionamentoOrdenadas)
                    Consumo.Add(item);
            }

            SemConsumo = !Consumo.Any();
            Carregando = false;
        }
    }
}