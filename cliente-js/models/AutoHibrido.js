import { Vehiculo } from './Vehiculo.js';

// Clase C — hereda de Vehiculo (A), implementa IAplicable
export class AutoHibrido extends Vehiculo {
  constructor(id, marca, modelo, anio, autonomiaKm, fechaRegistro, consumoCombustibleL100km, capacidadBateriaKwh) {
    super(id, marca, modelo, anio, autonomiaKm, fechaRegistro);
    this.consumoCombustibleL100km = consumoCombustibleL100km;
    this.capacidadBateriaKwh = capacidadBateriaKwh;
  }

  // Polimorfismo: costo = (L * 1.5) + (kWh/km * 100 * 0.15)
  calcularCostoOperacion() {
    const costoCombustible = this.consumoCombustibleL100km * 1.5;
    const costoElectrico = this.autonomiaKm > 0
      ? (this.capacidadBateriaKwh / this.autonomiaKm) * 100 * 0.15
      : 0;
    return costoCombustible + costoElectrico;
  }

  // IAplicable
  aplicarDescuento() {
    return `Descuento híbrido 10%. Costo ajustado: ${(this.calcularCostoOperacion() * 0.90).toFixed(4)} USD/100km`;
  }
}

// Patrón Builder para AutoHibrido
export class AutoHibridoBuilder {
  constructor() { this._a = {}; }
  setId(v)                       { this._a.id = v;                       return this; }
  setMarca(v)                    { this._a.marca = v;                    return this; }
  setModelo(v)                   { this._a.modelo = v;                   return this; }
  setAnio(v)                     { this._a.anio = v;                     return this; }
  setAutonomiaKm(v)              { this._a.autonomiaKm = v;              return this; }
  setFechaRegistro(v)            { this._a.fechaRegistro = v;            return this; }
  setConsumoCombustibleL100km(v) { this._a.consumoCombustibleL100km = v; return this; }
  setCapacidadBateriaKwh(v)      { this._a.capacidadBateriaKwh = v;      return this; }
  build() {
    return new AutoHibrido(
      this._a.id, this._a.marca, this._a.modelo, this._a.anio,
      this._a.autonomiaKm, this._a.fechaRegistro,
      this._a.consumoCombustibleL100km, this._a.capacidadBateriaKwh
    );
  }
}
