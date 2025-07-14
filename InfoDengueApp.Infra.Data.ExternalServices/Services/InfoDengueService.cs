using InfoDengueApp.Application.DTOs.External;
using InfoDengueApp.Application.Interfaces.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;

namespace InfoDengueApp.Infra.Data.ExternalServices
{
    public class InfoDengueService : IInfoDengueService
    {
        private readonly HttpClient _httpClient;

        public InfoDengueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://info.dengue.mat.br/api/");
        }

        public async Task<string> ObterDadosBrutosAsync()
        {
            var response = await _httpClient.GetAsync("alertcity");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ObterFiltradoAsync(DadosEpidemiologicosFiltroRequest filtro)
        {
            if (filtro == null)
                throw new ArgumentNullException(nameof(filtro));

            if (string.IsNullOrEmpty(filtro.Geocode))
                throw new ArgumentException("O código IBGE do município (geocode) é obrigatório.");

            if (string.IsNullOrEmpty(filtro.Disease))
                throw new ArgumentException("O campo Arbovirose (disease) é obrigatório.");

            if (filtro.SemanaInicio < 1 || filtro.SemanaInicio > 53)
                throw new ArgumentException("SemanaInicio deve estar entre 1 e 53.");

            if (filtro.SemanaFim < 1 || filtro.SemanaFim > 53)
                throw new ArgumentException("SemanaFim deve estar entre 1 e 53.");

            if (filtro.AnoInicio < 0)
                throw new ArgumentException("AnoInicio inválido.");

            if (filtro.AnoFim < 0)
                throw new ArgumentException("AnoFim inválido.");

            var queryParams = new Dictionary<string, string>
            {
                ["geocode"] = filtro.Geocode,
                ["disease"] = filtro.Disease.ToLower(),
                ["format"] = filtro.FormatoSaida ?? "json",
                ["ew_start"] = filtro.SemanaInicio.ToString(),
                ["ew_end"] = filtro.SemanaFim.ToString(),
                ["ey_start"] = filtro.AnoInicio.ToString(),
                ["ey_end"] = filtro.AnoFim.ToString()
            };

            var queryString = string.Join("&", queryParams
                .Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));

            var url = $"alertcity?{queryString}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
