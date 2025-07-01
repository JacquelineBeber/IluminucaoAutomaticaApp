using System.Text.Json.Serialization;

namespace IluminucaoAutomaticaApp.Models
{
    class MonitorarConsumo
    {
        [JsonPropertyName("consumoTotal")]
        public decimal Consumo { get; set; }

        [JsonPropertyName("acionamentos")]
        public int Acionamentos { get; set; }

        [JsonPropertyName("historicoConsumo")]
        public Consumo? HistoricoConsumo {get; set; }
    }
}