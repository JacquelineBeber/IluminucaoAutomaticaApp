using System.Text.Json.Serialization;

namespace IluminucaoAutomaticaApp.Models
{
    class MonitorarConsumo
    {
        [JsonPropertyName("consumoTotal")]
        public decimal ConsumoTotal { get; set; }

        [JsonPropertyName("acionamentos")]
        public int Acionamentos { get; set; }

        [JsonPropertyName("historicoConsumo")]
        public List<Consumo>? HistoricoConsumo {get; set; }
    }
}