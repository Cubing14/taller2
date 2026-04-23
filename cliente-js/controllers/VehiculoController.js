import { autoService } from '../services/AutoService.js';
import { AutoElectricoBuilder } from '../models/AutoElectrico.js';
import { AutoHibridoBuilder } from '../models/AutoHibrido.js';

export class VehiculoController {

  // ── AutoElectrico (A) ─────────────────────────────────────────────────
  async agregarElectrico(marca, modelo, anio, autonomia, baterias) {
    const anioI = parseInt(anio), autonomiaD = parseFloat(autonomia);
    if (isNaN(anioI) || isNaN(autonomiaD) || !baterias || baterias.length === 0)
      return { ok: false, mensaje: 'Todos los campos son obligatorios, incluyendo al menos una batería.' };

    for (let bat of baterias) {
      const capD = parseFloat(bat.capacidadKwh), ciclosI = parseInt(bat.ciclosVida);
      if (isNaN(capD) || isNaN(ciclosI) || !bat.tipo.trim() || !bat.fechaInstalacion || !bat.estado.trim())
        return { ok: false, mensaje: 'Datos de batería inválidos.' };
      bat.capacidadKwh = capD;
      bat.ciclosVida = ciclosI;
    }

    const auto = new AutoElectricoBuilder()
      .setMarca(marca.trim()).setModelo(modelo.trim()).setAnio(anioI)
      .setAutonomiaKm(autonomiaD).setFechaRegistro(new Date().toISOString())
      .setBaterias(baterias)
      .build();

    const r = await autoService.agregarElectrico(auto);
    return r ? { ok: true, mensaje: `Auto eléctrico guardado. ID: ${r.id}` } : { ok: false, mensaje: 'Error al guardar.' };
  }

  async buscarElectrico(id) {
    const idI = parseInt(id);
    if (isNaN(idI)) return { ok: false, mensaje: 'ID inválido.' };
    const a = await autoService.buscarElectricoPorId(idI);
    return a ? { ok: true, data: a } : { ok: false, mensaje: 'No se encontró ningún auto eléctrico con ese ID.' };
  }

  async actualizarElectrico(id, marca, modelo, anio, autonomia, baterias) {
    const anioI = parseInt(anio), autonomiaD = parseFloat(autonomia);
    if (isNaN(anioI) || isNaN(autonomiaD) || !baterias || baterias.length === 0)
      return { ok: false, mensaje: 'Campos inválidos.' };

    for (let bat of baterias) {
      const capD = parseFloat(bat.capacidadKwh), ciclosI = parseInt(bat.ciclosVida);
      if (isNaN(capD) || isNaN(ciclosI) || !bat.tipo.trim() || !bat.fechaInstalacion || !bat.estado.trim())
        return { ok: false, mensaje: 'Datos de batería inválidos.' };
      bat.capacidadKwh = capD;
      bat.ciclosVida = ciclosI;
    }

    const auto = new AutoElectricoBuilder()
      .setMarca(marca.trim()).setModelo(modelo.trim()).setAnio(anioI)
      .setAutonomiaKm(autonomiaD).setFechaRegistro(new Date().toISOString())
      .setBaterias(baterias)
      .build();

    const r = await autoService.actualizarElectrico(id, auto);
    return r ? { ok: true, mensaje: 'Auto eléctrico actualizado.' } : { ok: false, mensaje: 'Error al actualizar.' };
  }

  async listarElectricos() {
    return await autoService.listarElectricos();
  }

  async filtrarElectricosPorMarca(marca) {
    return await autoService.filtrarElectricosPorMarca(marca);
  }

  async filtrarElectricosPorAnio(anio) {
    return await autoService.filtrarElectricosPorAnio(anio);
  }

  async calcularCostoElectrico(id) {
    return await autoService.costoElectrico(id);
  }

  listarElectricos()                  { return autoService.listarElectricos(); }
  filtrarElectricosPorMarca(marca)    { return autoService.filtrarElectricosPorMarca(marca); }
  filtrarElectricosPorAnio(anio)      { return autoService.filtrarElectricosPorAnio(anio); }
  costoElectrico(id)                  { return autoService.costoElectrico(id); }

  // ── AutoHibrido (C) ───────────────────────────────────────────────────
  async agregarHibrido(marca, modelo, anio, autonomia, consumo, bateria) {
    const anioI = parseInt(anio), autonomiaD = parseFloat(autonomia);
    const consumoD = parseFloat(consumo), bateriaD = parseFloat(bateria);
    if (isNaN(anioI) || isNaN(autonomiaD) || isNaN(consumoD) || isNaN(bateriaD))
      return { ok: false, mensaje: 'Todos los campos numéricos son obligatorios.' };

    const auto = new AutoHibridoBuilder()
      .setMarca(marca.trim()).setModelo(modelo.trim()).setAnio(anioI)
      .setAutonomiaKm(autonomiaD).setFechaRegistro(new Date().toISOString())
      .setConsumoCombustibleL100km(consumoD).setCapacidadBateriaKwh(bateriaD)
      .build();

    const r = await autoService.agregarHibrido(auto);
    return r ? { ok: true, mensaje: `Auto híbrido guardado. ID: ${r.id}` } : { ok: false, mensaje: 'Error al guardar.' };
  }

  async buscarHibrido(id) {
    const idI = parseInt(id);
    if (isNaN(idI)) return { ok: false, mensaje: 'ID inválido.' };
    const a = await autoService.buscarHibridoPorId(idI);
    return a ? { ok: true, data: a } : { ok: false, mensaje: 'No se encontró ningún auto híbrido con ese ID.' };
  }

  async actualizarHibrido(id, marca, modelo, anio, autonomia, consumo, bateria) {
    const anioI = parseInt(anio), autonomiaD = parseFloat(autonomia);
    const consumoD = parseFloat(consumo), bateriaD = parseFloat(bateria);
    if (isNaN(anioI) || isNaN(autonomiaD) || isNaN(consumoD) || isNaN(bateriaD))
      return { ok: false, mensaje: 'Campos numéricos inválidos.' };

    const auto = new AutoHibridoBuilder()
      .setMarca(marca.trim()).setModelo(modelo.trim()).setAnio(anioI)
      .setAutonomiaKm(autonomiaD).setFechaRegistro(new Date().toISOString())
      .setConsumoCombustibleL100km(consumoD).setCapacidadBateriaKwh(bateriaD)
      .build();

    const r = await autoService.actualizarHibrido(id, auto);
    return r ? { ok: true, mensaje: 'Auto híbrido actualizado.' } : { ok: false, mensaje: 'Error al actualizar.' };
  }

  async eliminarHibrido(id) {
    const ok = await autoService.eliminarHibrido(id);
    return ok ? { ok: true, mensaje: 'Auto híbrido eliminado.' } : { ok: false, mensaje: 'Error al eliminar.' };
  }

  listarHibridos()               { return autoService.listarHibridos(); }
  filtrarHibridosPorMarca(marca) { return autoService.filtrarHibridosPorMarca(marca); }
  filtrarHibridosPorAnio(anio)   { return autoService.filtrarHibridosPorAnio(anio); }
  costoHibrido(id)               { return autoService.costoHibrido(id); }
}
