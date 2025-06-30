using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class MonitorarConsumoPageViewModel : BaseViewModel
    {
        private readonly IConsumoService _consumoService;
        public MonitorarConsumoPageViewModel()
        {
            _consumoService = new ConsumoService();

            Meses = new ObservableCollection<string>(System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames[..12]);

            int anoAtual = DateTime.Now.Year;
            for (int i = anoAtual; i >= anoAtual - 5; i--)
                Anos.Add(i.ToString());

            DataSelecionada = DateTime.Today;
            AnoSelecionado = anoAtual.ToString();
            MesSelecionado = Meses[DateTime.Now.Month - 1];

            AtualizarResumoInicial();
        }

        private bool _mostrarResumoInicial = true;
        public bool MostrarResumoInicial
        {
            get => _mostrarResumoInicial;
            set => SetProperty(ref _mostrarResumoInicial, value);
        }

        private async void AtualizarResumoInicial()
        {
            DataReferenciaFormatada = $"Dia: {DateTime.Today:dd/MM/yyyy}";
            //var dados = await _consumoService.BuscarConsumoDiarioAsync(DateTime.Today);
            //ConsumoTotal = $"{dados.Consumo} KWh";
            //Acionamentos = dados.Acionamentos;
        }
        public ObservableCollection<string> TiposFiltro { get; } = new() { "Diário", "Mensal", "Anual" };
        public ObservableCollection<string> Meses { get; } = new();
        public ObservableCollection<string> Anos { get; } = new();

        private string? _tipoFiltroSelecionado;
        public string TipoFiltroSelecionado
        {
            get => _tipoFiltroSelecionado;
            set
            {
                MostrarResumoInicial = false;
                SetProperty(ref _tipoFiltroSelecionado, value);
                OnPropertyChanged(nameof(IsFiltroDiario));
                OnPropertyChanged(nameof(IsFiltroMensal));
                OnPropertyChanged(nameof(IsFiltroAnual));
                AtualizarDadosFiltro();
            }
        }
        public bool IsFiltroDiario => TipoFiltroSelecionado == "Diário";
        public bool IsFiltroMensal => TipoFiltroSelecionado == "Mensal";
        public bool IsFiltroAnual => TipoFiltroSelecionado == "Anual";

        private DateTime _dataSelecionada;
        public DateTime DataSelecionada
        {
            get => _dataSelecionada;
            set
            {
                SetProperty(ref _dataSelecionada, value);
                AtualizarDadosFiltro();
            }
        }

        private string? _mesSelecionado;
        public string? MesSelecionado
        {
            get => _mesSelecionado;
            set
            {
                SetProperty(ref _mesSelecionado, value);
                AtualizarDadosFiltro();
            }
        }

        private string? _anoSelecionado;
        public string? AnoSelecionado
        {
            get => _anoSelecionado;
            set
            {
                SetProperty(ref _anoSelecionado, value);
                AtualizarDadosFiltro();
            }
        }

        private string _dataReferenciaFormatada = string.Empty;
        public string DataReferenciaFormatada
        {
            get => _dataReferenciaFormatada;
            set => SetProperty(ref _dataReferenciaFormatada, value);
        }

        private string _consumoTotal = "0 KWh";
        public string ConsumoTotal
        {
            get => _consumoTotal;
            set => SetProperty(ref _consumoTotal, value);
        }

        private int _acionamentos;
        public int Acionamentos
        {
            get => _acionamentos;
            set => SetProperty(ref _acionamentos, value);
        }

        private async void AtualizarDadosFiltro()
        {
            Carregando = true;
            if (TipoFiltroSelecionado == "Diário")
            {
                DataReferenciaFormatada = $"Dia: {DataSelecionada:dd/MM/yyyy}";
                //var dados = await _consumoService.BuscarConsumoDiarioAsync(DataSelecionada);
                //ConsumoTotal = $"{dados.Consumo} KWh";
                //Acionamentos = dados.Acionamentos;
            }
            else if(TipoFiltroSelecionado == "Mensal")
            {
                var mes = DateTime.ParseExact(MesSelecionado, "MMMM", new System.Globalization.CultureInfo("pt-BR")).Month;
                DataReferenciaFormatada = $"Mês: {MesSelecionado} / {AnoSelecionado}";
                //var dados = await _consumoService.BuscarConsumoMensalAsync(mes, int.Parse(AnoSelecionado));
                //ConsumoTotal = $"{dados.Consumo} KWh";
                //Acionamentos = dados.Acionamentos;
            }
            else if (TipoFiltroSelecionado == "Anual")
            {
                DataReferenciaFormatada = $"Ano: {AnoSelecionado}";
                //var dados = await _consumoService.BuscarConsumoAnualAsync(int.Parse(AnoSelecionado));
                //ConsumoTotal = $"{dados.Consumo} KWh";
                //Acionamentos = dados.Acionamentos;
            }
            Carregando = false;
        }
    }
}