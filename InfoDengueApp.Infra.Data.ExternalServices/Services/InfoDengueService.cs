using InfoDengueApp.Application.DTOs.External;
using InfoDengueApp.Application.Interfaces.ExternalServices;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfoDengueApp.Infra.Data.ExternalServices
{
    public class InfoDengueService : IInfoDengueService
    {
        private readonly HttpClient _httpClient;

        public InfoDengueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://info.dengue.mat.br/services/api/");
        }

        public async Task<string> ObterDadosBrutosAsync()
        {
            var response = await _httpClient.GetAsync("alertcity");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ObterFiltradoAsync(DadosEpidemiologicosFiltroRequest filtro)
        {
            var queryParams = new Dictionary<string, string>
            {
                ["geocode"] = filtro.MunicipioSelecionado,
                ["semana_ini"] = filtro.SemanaInicio?.ToString("yyyy-MM-dd"),
                ["semana_fim"] = filtro.SemanaFim?.ToString("yyyy-MM-dd"),
                ["arbovirose"] = filtro.Arbovirose,
                ["formato"] = filtro.FormatoSaida ?? "json"
            };

            var queryString = string.Join("&", queryParams
                .Where(p => !string.IsNullOrEmpty(p.Value))
                .Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));

            var url = $"alertcitydata?{queryString}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
