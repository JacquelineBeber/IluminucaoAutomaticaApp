using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IluminucaoAutomaticaApp.Models
{
    class Lampada
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("potencia")]
        public int Potencia { get; set; }

        [JsonPropertyName("estado")]
        public string? Estado { get; set; }

        [JsonPropertyName("ativa")]
        public bool Ativa { get; set; }
    }
}