import { autoService } from '../services/AutoService.js';
import { BateriaBuilder } from '../models/Bateria.js';

export class BateriaController {

  async agregarBateria(tipo, capacidad, ciclos, fecha, estado) {
    const capD = parseFloat(capacidad), ciclosI = parseInt(ciclos);
    if (!tipo.trim() || isNaN(capD) || isNaN(ciclosI) || !fecha || !estado.trim())
      return { ok: false, mensaje: 'Todos los campos son obligatorios.' };

    const b = new BateriaBuilder()
      .setTipo(tipo.trim()).setCapacidadKwh(capD).setCiclosVida(ciclosI)
      .setFechaInstalacion(fecha).setEstado(estado.trim()).build();

    const r = await autoService.agregarBateria(b);
    return r ? { ok: true, mensaje: `Batería agregada. ID: ${r.id}` } : { ok: false, mensaje: 'Error al agregar.' };
  }

  async buscarBateria(id) {
    const idI = parseInt(id);
    if (isNaN(idI)) return { ok: false, mensaje: 'ID inválido.' };
    const b = await autoService.buscarBateriaPorId(idI);
    return b ? { ok: true, data: b } : { ok: false, mensaje: 'Batería no encontrada.' };
  }

  async actualizarBateria(id, tipo, capacidad, ciclos, fecha, estado) {
    const b = new BateriaBuilder();
    if (tipo) b.setTipo(tipo.trim());
    if (capacidad) b.setCapacidadKwh(parseFloat(capacidad));
    if (ciclos) b.setCiclosVida(parseInt(ciclos));
    if (fecha) b.setFechaInstalacion(fecha);
    if (estado) b.setEstado(estado.trim());
    const built = b.build();

    const r = await autoService.actualizarBateria(id, built);
    return r ? { ok: true, mensaje: 'Batería actualizada.' } : { ok: false, mensaje: 'Error al actualizar.' };
  }

  async eliminarBateria(id) {
    const ok = await autoService.eliminarBateria(id);
    return ok ? { ok: true, mensaje: 'Batería eliminada.' } : { ok: false, mensaje: 'Error al eliminar.' };
  }

  async listarBaterias() { return await autoService.listarBaterias(); }
}
