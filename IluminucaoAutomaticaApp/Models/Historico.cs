using System.Globalization;
using System.Text.Json.Serialization;

namespace IluminucaoAutomaticaApp.Models
{
    class Historico
    {
        [JsonPropertyName("acao")]
        public string? Acao { get; set; }

        [JsonPropertyName("data_hora_acao")]
        public string? MomentoAcao { get; set; }

        [JsonPropertyName("responsavel_acao")]
        public string? OrigemAcao { get; set; }

        public DateTime? MomentoAcaoDataHora
        {
            get
            {
                if (DateTime.TryParseExact(MomentoAcao, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var data))
                    return data;
                return null;
            }
        }
    }
}
