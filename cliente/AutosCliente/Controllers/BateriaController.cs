using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutosCliente.Models;
using AutosCliente.Services;

namespace AutosCliente.Controllers
{
    // Controlador para Bateria (clase D)
    public class BateriaController
    {
        private readonly AutoService _service;

        public BateriaController(AutoService service)
        {
            _service = service;
        }

        public async Task<(bool ok, string mensaje, Bateria? resultado)> Agregar(
            string tipo, string capacidad, string ciclos)
        {
            if (!double.TryParse(capacidad, out double capD)) return (false, "Capacidad inválida.", null);
            if (!int.TryParse(ciclos, out int ciclosI)) return (false, "Ciclos inválidos.", null);

            var b = new Bateria { Tipo = tipo.Trim(), CapacidadKwh = capD, CiclosVida = ciclosI };
            var r = await _service.AgregarBateria(b);
            return r != null ? (true, $"Batería agregada. ID: {r.Id}", r) : (false, "Error al agregar.", null);
        }

        public async Task<(bool ok, string mensaje, Bateria? resultado)> Buscar(string idStr)
        {
            if (!int.TryParse(idStr, out int id)) return (false, "ID inválido.", null);
            var b = await _service.BuscarBateriaPorId(id);
            return b != null ? (true, "", b) : (false, "Batería no encontrada.", null);
        }

        public async Task<(bool ok, string mensaje, Bateria? resultado)> Actualizar(
            int id, string tipo, string capacidad, string ciclos)
        {
            if (!double.TryParse(capacidad, out double capD)) return (false, "Capacidad inválida.", null);
            if (!int.TryParse(ciclos, out int ciclosI)) return (false, "Ciclos inválidos.", null);

            var b = new Bateria { Tipo = tipo.Trim(), CapacidadKwh = capD, CiclosVida = ciclosI };
            var r = await _service.ActualizarBateria(id, b);
            return r != null ? (true, "Batería actualizada.", r) : (false, "Error al actualizar.", null);
        }

        public async Task<(bool ok, string mensaje)> Eliminar(int id)
        {
            bool ok = await _service.EliminarBateria(id);
            return ok ? (true, "Batería eliminada.") : (false, "Error al eliminar.");
        }

        public Task<List<Bateria>?> ListarTodas() => _service.ListarBaterias();
    }
}
