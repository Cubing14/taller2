using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutosCliente.Models;
using AutosCliente.Services;

namespace AutosCliente.Controllers
{
    // Controlador para AutoElectrico (clase B) y AutoHibrido (clase C)
    // Separa la lógica de negocio de los formularios (Vista)
    public class VehiculoController
    {
        private readonly AutoService _service;

        public VehiculoController(AutoService service)
        {
            _service = service;
        }

        // ── AutoElectrico (B) ─────────────────────────────────────────────
        public async Task<(bool ok, string mensaje, AutoElectrico? resultado)> AgregarElectrico(
            string marca, string modelo, string anio, string autonomia,
            string batTipo, string batCapacidad, string batCiclos)
        {
            if (!int.TryParse(anio, out int anioInt)) return (false, "Año inválido.", null);
            if (!double.TryParse(autonomia, out double autonomiaD)) return (false, "Autonomía inválida.", null);
            if (!double.TryParse(batCapacidad, out double capD)) return (false, "Capacidad de batería inválida.", null);
            if (!int.TryParse(batCiclos, out int ciclosI)) return (false, "Ciclos de vida inválidos.", null);

            var auto = new AutoElectrico
            {
                Marca = marca.Trim(), Modelo = modelo.Trim(), Anio = anioInt,
                AutonomiaKm = autonomiaD, FechaRegistro = DateTime.Now,
                Bateria = new Bateria { Tipo = batTipo.Trim(), CapacidadKwh = capD, CiclosVida = ciclosI }
            };
            var r = await _service.Agregar(auto);
            return r != null ? (true, $"Auto eléctrico guardado. ID: {r.Id}", r) : (false, "Error al guardar.", null);
        }

        public async Task<(bool ok, string mensaje, AutoElectrico? resultado)> BuscarElectrico(string idStr)
        {
            if (!int.TryParse(idStr, out int id)) return (false, "ID inválido.", null);
            var a = await _service.BuscarPorId(id);
            return a != null ? (true, "", a) : (false, "No se encontró ningún auto eléctrico con ese ID.", null);
        }

        public async Task<(bool ok, string mensaje, AutoElectrico? resultado)> ActualizarElectrico(
            int id, string marca, string modelo, string anio, string autonomia,
            string batTipo, string batCapacidad, string batCiclos)
        {
            if (!int.TryParse(anio, out int anioInt)) return (false, "Año inválido.", null);
            if (!double.TryParse(autonomia, out double autonomiaD)) return (false, "Autonomía inválida.", null);
            if (!double.TryParse(batCapacidad, out double capD)) return (false, "Capacidad inválida.", null);
            if (!int.TryParse(batCiclos, out int ciclosI)) return (false, "Ciclos inválidos.", null);

            var auto = new AutoElectrico
            {
                Marca = marca.Trim(), Modelo = modelo.Trim(), Anio = anioInt,
                AutonomiaKm = autonomiaD, FechaRegistro = DateTime.Now,
                Bateria = new Bateria { Tipo = batTipo.Trim(), CapacidadKwh = capD, CiclosVida = ciclosI }
            };
            var r = await _service.Actualizar(id, auto);
            return r != null ? (true, "Auto eléctrico actualizado.", r) : (false, "Error al actualizar.", null);
        }

        public async Task<(bool ok, string mensaje)> EliminarElectrico(int id)
        {
            bool ok = await _service.Eliminar(id);
            return ok ? (true, "Auto eléctrico eliminado.") : (false, "Error al eliminar.");
        }

        public Task<List<AutoElectrico>?> ListarElectricos() => _service.ListarTodos();
        public Task<List<AutoElectrico>?> FiltrarElectricosPorMarca(string marca) => _service.FiltrarPorMarca(marca);
        public Task<List<AutoElectrico>?> FiltrarElectricosPorAnio(int anio) => _service.FiltrarPorAnio(anio);

        // ── AutoHibrido (C) ───────────────────────────────────────────────
        public async Task<(bool ok, string mensaje, AutoHibrido? resultado)> AgregarHibrido(
            string marca, string modelo, string anio, string autonomia, string consumo, string bateria)
        {
            if (!int.TryParse(anio, out int anioInt)) return (false, "Año inválido.", null);
            if (!double.TryParse(autonomia, out double autonomiaD)) return (false, "Autonomía inválida.", null);
            if (!double.TryParse(consumo, out double consumoD)) return (false, "Consumo inválido.", null);
            if (!double.TryParse(bateria, out double bateriaD)) return (false, "Batería inválida.", null);

            var auto = new AutoHibrido
            {
                Marca = marca.Trim(), Modelo = modelo.Trim(), Anio = anioInt,
                AutonomiaKm = autonomiaD, FechaRegistro = DateTime.Now,
                ConsumoCombustibleL100km = consumoD, CapacidadBateriaKwh = bateriaD
            };
            var r = await _service.AgregarHibrido(auto);
            return r != null ? (true, $"Auto híbrido guardado. ID: {r.Id}", r) : (false, "Error al guardar.", null);
        }

        public async Task<(bool ok, string mensaje, AutoHibrido? resultado)> BuscarHibrido(string idStr)
        {
            if (!int.TryParse(idStr, out int id)) return (false, "ID inválido.", null);
            var a = await _service.BuscarHibridoPorId(id);
            return a != null ? (true, "", a) : (false, "No se encontró ningún auto híbrido con ese ID.", null);
        }

        public async Task<(bool ok, string mensaje, AutoHibrido? resultado)> ActualizarHibrido(
            int id, string marca, string modelo, string anio, string autonomia, string consumo, string bateria)
        {
            if (!int.TryParse(anio, out int anioInt)) return (false, "Año inválido.", null);
            if (!double.TryParse(autonomia, out double autonomiaD)) return (false, "Autonomía inválida.", null);
            if (!double.TryParse(consumo, out double consumoD)) return (false, "Consumo inválido.", null);
            if (!double.TryParse(bateria, out double bateriaD)) return (false, "Batería inválida.", null);

            var auto = new AutoHibrido
            {
                Marca = marca.Trim(), Modelo = modelo.Trim(), Anio = anioInt,
                AutonomiaKm = autonomiaD, FechaRegistro = DateTime.Now,
                ConsumoCombustibleL100km = consumoD, CapacidadBateriaKwh = bateriaD
            };
            var r = await _service.ActualizarHibrido(id, auto);
            return r != null ? (true, "Auto híbrido actualizado.", r) : (false, "Error al actualizar.", null);
        }

        public async Task<(bool ok, string mensaje)> EliminarHibrido(int id)
        {
            bool ok = await _service.EliminarHibrido(id);
            return ok ? (true, "Auto híbrido eliminado.") : (false, "Error al eliminar.");
        }

        public Task<List<AutoHibrido>?> ListarHibridos() => _service.ListarHibridos();
        public Task<List<AutoHibrido>?> FiltrarHibridosPorMarca(string marca) => _service.FiltrarHibridosPorMarca(marca);
        public Task<List<AutoHibrido>?> FiltrarHibridosPorAnio(int anio) => _service.FiltrarHibridosPorAnio(anio);

        // ── Polimorfismo ──────────────────────────────────────────────────
        public Task<string?> ObtenerCostoElectrico(int id) => _service.ObtenerCostoElectrico(id);
        public Task<string?> ObtenerCostoHibrido(int id) => _service.ObtenerCostoHibrido(id);
    }
}
