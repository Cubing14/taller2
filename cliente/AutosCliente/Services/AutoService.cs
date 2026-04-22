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

        // ── Observer ──────────────────────────────────────────────────────
        private readonly List<IAutoObserver> _observers = new();

        public void Suscribir(IAutoObserver observer) => _observers.Add(observer);
        public void Desuscribir(IAutoObserver observer) => _observers.Remove(observer);
        private void Notificar() => _observers.ForEach(o => o.OnAutosActualizados());

        // ── Helpers ───────────────────────────────────────────────────────
        private static StringContent ToJson(object obj) =>
            new(JsonSerializer.Serialize(obj, _opts), Encoding.UTF8, "application/json");

        private T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, _opts);

        // ── AutoElectrico (B) ─────────────────────────────────────────────
        public async Task<AutoElectrico?> Agregar(AutoElectrico auto)
        {
            var resp = await _http.PostAsync("autos", ToJson(auto));
            var result = Deserialize<AutoElectrico>(await resp.Content.ReadAsStringAsync());
            Notificar();
            return result;
        }

        public async Task<AutoElectrico?> BuscarPorId(int id)
        {
            var json = await _http.GetStringAsync($"autos/{id}");
            return Deserialize<AutoElectrico>(json);
        }

        public async Task<bool> Eliminar(int id)
        {
            var resp = await _http.DeleteAsync($"autos/{id}");
            if (resp.IsSuccessStatusCode) Notificar();
            return resp.IsSuccessStatusCode;
        }

        public async Task<AutoElectrico?> Actualizar(int id, AutoElectrico auto)
        {
            var resp = await _http.PutAsync($"autos/{id}", ToJson(auto));
            if (!resp.IsSuccessStatusCode) return null;
            var result = Deserialize<AutoElectrico>(await resp.Content.ReadAsStringAsync());
            Notificar();
            return result;
        }

        public async Task<List<AutoElectrico>?> ListarTodos()
        {
            var json = await _http.GetStringAsync("autos");
            return Deserialize<List<AutoElectrico>>(json);
        }

        public async Task<List<AutoElectrico>?> FiltrarPorMarca(string marca)
        {
            var json = await _http.GetStringAsync($"autos/filtrar/marca/{marca}");
            return Deserialize<List<AutoElectrico>>(json);
        }

        public async Task<List<AutoElectrico>?> FiltrarPorAnio(int anio)
        {
            var json = await _http.GetStringAsync($"autos/filtrar/anio/{anio}");
            return Deserialize<List<AutoElectrico>>(json);
        }

        // ── AutoHibrido (C) ───────────────────────────────────────────────
        public async Task<AutoHibrido?> AgregarHibrido(AutoHibrido auto)
        {
            var resp = await _http.PostAsync("hibridos", ToJson(auto));
            var result = Deserialize<AutoHibrido>(await resp.Content.ReadAsStringAsync());
            Notificar();
            return result;
        }

        public async Task<AutoHibrido?> BuscarHibridoPorId(int id)
        {
            var json = await _http.GetStringAsync($"hibridos/{id}");
            return Deserialize<AutoHibrido>(json);
        }

        public async Task<bool> EliminarHibrido(int id)
        {
            var resp = await _http.DeleteAsync($"hibridos/{id}");
            if (resp.IsSuccessStatusCode) Notificar();
            return resp.IsSuccessStatusCode;
        }

        public async Task<AutoHibrido?> ActualizarHibrido(int id, AutoHibrido auto)
        {
            var resp = await _http.PutAsync($"hibridos/{id}", ToJson(auto));
            if (!resp.IsSuccessStatusCode) return null;
            var result = Deserialize<AutoHibrido>(await resp.Content.ReadAsStringAsync());
            Notificar();
            return result;
        }

        public async Task<List<AutoHibrido>?> ListarHibridos()
        {
            var json = await _http.GetStringAsync("hibridos");
            return Deserialize<List<AutoHibrido>>(json);
        }

        public async Task<List<AutoHibrido>?> FiltrarHibridosPorMarca(string marca)
        {
            var json = await _http.GetStringAsync($"hibridos/filtrar/marca/{marca}");
            return Deserialize<List<AutoHibrido>>(json);
        }

        public async Task<List<AutoHibrido>?> FiltrarHibridosPorAnio(int anio)
        {
            var json = await _http.GetStringAsync($"hibridos/filtrar/anio/{anio}");
            return Deserialize<List<AutoHibrido>>(json);
        }

        // ── Bateria (D) ───────────────────────────────────────────────────
        public async Task<Bateria?> AgregarBateria(Bateria b)
        {
            var resp = await _http.PostAsync("baterias", ToJson(b));
            return Deserialize<Bateria>(await resp.Content.ReadAsStringAsync());
        }

        public async Task<Bateria?> BuscarBateriaPorId(int id)
        {
            var json = await _http.GetStringAsync($"baterias/{id}");
            return Deserialize<Bateria>(json);
        }

        public async Task<bool> EliminarBateria(int id)
        {
            var resp = await _http.DeleteAsync($"baterias/{id}");
            return resp.IsSuccessStatusCode;
        }

        public async Task<Bateria?> ActualizarBateria(int id, Bateria b)
        {
            var resp = await _http.PutAsync($"baterias/{id}", ToJson(b));
            if (!resp.IsSuccessStatusCode) return null;
            return Deserialize<Bateria>(await resp.Content.ReadAsStringAsync());
        }

        public async Task<List<Bateria>?> ListarBaterias()
        {
            var json = await _http.GetStringAsync("baterias");
            return Deserialize<List<Bateria>>(json);
        }

        // ── Costo operación (polimorfismo) ────────────────────────────────
        public async Task<string?> ObtenerCostoElectrico(int id)
        {
            var resp = await _http.GetAsync($"autos/{id}/costo");
            return resp.IsSuccessStatusCode ? await resp.Content.ReadAsStringAsync() : null;
        }

        public async Task<string?> ObtenerCostoHibrido(int id)
        {
            var resp = await _http.GetAsync($"hibridos/{id}/costo");
            return resp.IsSuccessStatusCode ? await resp.Content.ReadAsStringAsync() : null;
        }
    }
}
