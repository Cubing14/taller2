using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutosCliente.Models;

namespace AutosCliente.Services
{
    public class AutoService
    {
        private static readonly HttpClient _http = new() { BaseAddress = new System.Uri("http://localhost:8080/") };

        public async Task<AutoElectrico?> Agregar(AutoElectrico auto)
        {
            var resp = await _http.PostAsJsonAsync("autos", auto);
            return await resp.Content.ReadFromJsonAsync<AutoElectrico>();
        }

        public Task<AutoElectrico?> BuscarPorId(int id) =>
            _http.GetFromJsonAsync<AutoElectrico>($"autos/{id}");

        public async Task<bool> Eliminar(int id)
        {
            var resp = await _http.DeleteAsync($"autos/{id}");
            return resp.IsSuccessStatusCode;
        }

        public async Task<AutoElectrico?> Actualizar(int id, AutoElectrico auto)
        {
            var resp = await _http.PutAsJsonAsync($"autos/{id}", auto);
            return resp.IsSuccessStatusCode ? await resp.Content.ReadFromJsonAsync<AutoElectrico>() : null;
        }

        public Task<List<AutoElectrico>?> ListarTodos() =>
            _http.GetFromJsonAsync<List<AutoElectrico>>("autos");

        public Task<List<AutoElectrico>?> FiltrarPorMarca(string marca) =>
            _http.GetFromJsonAsync<List<AutoElectrico>>($"autos/filtrar/marca/{marca}");

        public Task<List<AutoElectrico>?> FiltrarPorAnio(int anio) =>
            _http.GetFromJsonAsync<List<AutoElectrico>>($"autos/filtrar/anio/{anio}");
    }
}
