using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Application.DTOs.External
{
    public class DadosEpidemiologicosFiltroRequest
    {
        public string Geocode { get; set; }       // Código IBGE do município (ex: "3304557 RIO DE JANEIRO")
        public string Disease { get; set; }       // Tipo de arbovirose: "dengue", "zika", "chikungunya"
        public int SemanaInicio { get; set; }     // Semana epidemiológica inicial (1-53)
        public int SemanaFim { get; set; }        // Semana epidemiológica final (1-53)
        public int AnoInicio { get; set; }        // Ano inicial da consulta (ex: 2017)
        public int AnoFim { get; set; }           // Ano final da consulta (ex: 2017)
        public string FormatoSaida { get; set; } = "json"; // Formato de saída (json ou csv)
    }
}