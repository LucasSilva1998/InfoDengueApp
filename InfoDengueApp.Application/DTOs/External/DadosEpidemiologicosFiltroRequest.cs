using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Application.DTOs.External
{
    public class DadosEpidemiologicosFiltroRequest
    {
        public string FiltroMunicipio { get; set; }      // Código IBGE, nome ou UF
        public string MunicipioSelecionado { get; set; } // Nome exato, selecionado da lista
        public DateTime? SemanaInicio { get; set; }      // Data da semana epidemiológica inicial
        public DateTime? SemanaFim { get; set; }         // Data da semana epidemiológica final
        public string Arbovirose { get; set; }           // Ex: "dengue"
        public string FormatoSaida { get; set; } = "json"; // Default para json
    }
}
