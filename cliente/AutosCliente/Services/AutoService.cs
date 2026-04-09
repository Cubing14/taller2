using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutosCliente.Models;

namespace AutosCliente.Services
{
    public class AutoService
    {
        private static readonly HttpClient _http = new() { BaseAddress = new Uri("http://localhost:8080/") };

        private static readonly JsonSerializerOptions _opts = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new LocalDateTimeConverter() }
        };

        private static StringContent ToJson(object obj) =>
            new(JsonSerializer.Serialize(obj, _opts), Encoding.UTF8, "application/json");

        public async Task<AutoElectrico?> Agregar(AutoElectrico auto)
        {
            var resp = await _http.PostAsync("autos", ToJson(auto));
            var json = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AutoElectrico>(json, _opts);
        }

        public async Task<AutoElectrico?> BuscarPorId(int id)
        {
            var json = await _http.GetStringAsync($"autos/{id}");
            return JsonSerializer.Deserialize<AutoElectrico>(json, _opts);
        }

        public async Task<bool> Eliminar(int id)
        {
            var resp = await _http.DeleteAsync($"autos/{id}");
            return resp.IsSuccessStatusCode;
        }

        public async Task<AutoElectrico?> Actualizar(int id, AutoElectrico auto)
        {
            var resp = await _http.PutAsync($"autos/{id}", ToJson(auto));
            var json = await resp.Content.ReadAsStringAsync();
            return resp.IsSuccessStatusCode ? JsonSerializer.Deserialize<AutoElectrico>(json, _opts) : null;
        }

        public async Task<List<AutoElectrico>?> ListarTodos()
        {
            var json = await _http.GetStringAsync("autos");
            return JsonSerializer.Deserialize<List<AutoElectrico>>(json, _opts);
        }

        public async Task<List<AutoElectrico>?> FiltrarPorMarca(string marca)
        {
            var json = await _http.GetStringAsync($"autos/filtrar/marca/{marca}");
            return JsonSerializer.Deserialize<List<AutoElectrico>>(json, _opts);
        }

        public async Task<List<AutoElectrico>?> FiltrarPorAnio(int anio)
        {
            var json = await _http.GetStringAsync($"autos/filtrar/anio/{anio}");
            return JsonSerializer.Deserialize<List<AutoElectrico>>(json, _opts);
        }
    }
}
