using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.Services;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows.Input;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class MonitorarConsumoPageViewModel : BaseViewModel
    {
        private readonly IConsumoService _consumoService;
        public ICommand FiltrarCommand { get; }

        public MonitorarConsumoPageViewModel()
        {
            _consumoService = new ConsumoService();
            FiltrarCommand = new Command(Filtrar);

            Meses = new ObservableCollection<string>(System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames[..12]);

            int anoAtual = DateTime.Now.Year;
            for (int i = anoAtual; i >= anoAtual - 5; i--)
                Anos.Add(i.ToString());

            DataSelecionada = DateTime.Today;
            AnoSelecionado = anoAtual.ToString();
            MesSelecionado = Meses[DateTime.Now.Month - 1];

            AtualizarResumoInicial();
        }

        public ObservableCollection<MonitorarConsumo> MonitorarConsumo { get; set; } = new();

        public ObservableCollection<string> TiposFiltro { get; } = new() { "Diário", "Mensal", "Anual" };
        public ObservableCollection<string> Meses { get; } = new();
        public ObservableCollection<string> Anos { get; } = new();
        public bool IsFiltroDiario => TipoFiltroSelecionado == "Diário";
        public bool IsFiltroMensal => TipoFiltroSelecionado == "Mensal";
        public bool IsFiltroAnual => TipoFiltroSelecionado == "Anual";
        public bool MostrarBtnFiltrar => TipoFiltroSelecionado == "Diário" || TipoFiltroSelecionado == "Mensal" || TipoFiltroSelecionado == "Anual";

        private bool _mostrarResumoInicial = true;
        public bool MostrarResumoInicial
        {
            get => _mostrarResumoInicial;
            set => SetProperty(ref _mostrarResumoInicial, value);
        }

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
                OnPropertyChanged(nameof(MostrarBtnFiltrar));
            }
        }

        private DateTime _dataSelecionada;
        public DateTime DataSelecionada
        {
            get => _dataSelecionada;
            set
            {
                SetProperty(ref _dataSelecionada, value);
            }
        }

        private string? _mesSelecionado;
        public string? MesSelecionado
        {
            get => _mesSelecionado;
            set
            {
                SetProperty(ref _mesSelecionado, value);
            }
        }

        private string? _anoSelecionado;
        public string? AnoSelecionado
        {
            get => _anoSelecionado;
            set
            {
                SetProperty(ref _anoSelecionado, value);
            }
        }

        private string _dataReferenciaFormatada = string.Empty;
        public string DataReferenciaFormatada
        {
            get => _dataReferenciaFormatada;
            set => SetProperty(ref _dataReferenciaFormatada, value);
        }

        private decimal _consumoTotal;
        public decimal ConsumoTotal
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

        private async void AtualizarResumoInicial()
        {
            DataReferenciaFormatada = $"Hoje:\n{DateTime.Today:dd/MM/yyyy}";
            Console.WriteLine($"Data para ser filtrada: {DataReferenciaFormatada}");
            MonitorarConsumo dados = await _consumoService.BuscarConsumoDiarioAsync(DateTime.Today);
            MonitorarConsumo.Add(dados);
            ConsumoTotal = dados.ConsumoTotal;
            Acionamentos = dados.Acionamentos;
        }

        private async void Filtrar()
        {
            Carregando = true;
            if (TipoFiltroSelecionado == "Diário")
            {
                DataReferenciaFormatada = $"Filtro Diário\n{DataSelecionada:dd/MM/yyyy}:";
                MonitorarConsumo dados = await _consumoService.BuscarConsumoDiarioAsync(DataSelecionada);
                MonitorarConsumo.Add(dados);
                ConsumoTotal = dados.ConsumoTotal;
                Acionamentos = dados.Acionamentos;
            }
            else if (TipoFiltroSelecionado == "Mensal")
            {
                int mes = DateTime.ParseExact(MesSelecionado, "MMMM", new System.Globalization.CultureInfo("pt-BR")).Month;
                DataReferenciaFormatada = $"Filtro Mensal\n{MesSelecionado} / {AnoSelecionado}:";
                MonitorarConsumo dados = await _consumoService.BuscarConsumoMensalAsync(int.Parse(AnoSelecionado), mes);
                ConsumoTotal = dados.ConsumoTotal;
                Acionamentos = dados.Acionamentos;
            }
            else if (TipoFiltroSelecionado == "Anual")
            {
                DataReferenciaFormatada = $"Filtro Anual\n{AnoSelecionado}:";
                MonitorarConsumo dados = await _consumoService.BuscarConsumoAnualAsync(int.Parse(AnoSelecionado));
                ConsumoTotal = dados.ConsumoTotal;
                Acionamentos = dados.Acionamentos;
            }
            Carregando = false;
        }
    }
}