import { Vehiculo } from './Vehiculo.js';

// Clase A — hereda de Vehiculo, tiene varias Baterias (Clase B)
export class AutoElectrico extends Vehiculo {
  constructor(id, marca, modelo, anio, autonomiaKm, fechaRegistro, baterias) {
    super(id, marca, modelo, anio, autonomiaKm, fechaRegistro);
    this.baterias = baterias || []; // Array de Baterias
  }

  // Polimorfismo: costo = suma de (kWh / km) * 100 * 0.15 USD/kWh por batería
  calcularCostoOperacion() {
    if (this.baterias.length === 0 || this.autonomiaKm === 0) return 0;
    return this.baterias.reduce((total, bateria) => {
      return total + (bateria.capacidadKwh / this.autonomiaKm) * 100 * 0.15;
    }, 0);
  }
}

// Patrón Builder para AutoElectrico
export class AutoElectricoBuilder {
  constructor() { this._a = {}; }
  setId(v)            { this._a.id = v;            return this; }
  setMarca(v)         { this._a.marca = v;          return this; }
  setModelo(v)        { this._a.modelo = v;         return this; }
  setAnio(v)          { this._a.anio = v;           return this; }
  setAutonomiaKm(v)   { this._a.autonomiaKm = v;    return this; }
  setFechaRegistro(v) { this._a.fechaRegistro = v;  return this; }
  setBaterias(v)      { this._a.baterias = v;       return this; }
  build() {
    return new AutoElectrico(
      this._a.id, this._a.marca, this._a.modelo, this._a.anio,
      this._a.autonomiaKm, this._a.fechaRegistro, this._a.baterias
    );
  }
}
