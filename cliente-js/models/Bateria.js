// Modelo Bateria — Clase B
export class Bateria {
  constructor(id, tipo, capacidadKwh, ciclosVida, fechaInstalacion, estado) {
    this.id = id;
    this.tipo = tipo;
    this.capacidadKwh = capacidadKwh;
    this.ciclosVida = ciclosVida;
    this.fechaInstalacion = fechaInstalacion;
    this.estado = estado;
  }
}

// Patrón Builder para Bateria
export class BateriaBuilder {
  constructor() { this._b = {}; }
  setId(v)              { this._b.id = v;              return this; }
  setTipo(v)            { this._b.tipo = v;            return this; }
  setCapacidadKwh(v)    { this._b.capacidadKwh = v;    return this; }
  setCiclosVida(v)      { this._b.ciclosVida = v;      return this; }
  setFechaInstalacion(v){ this._b.fechaInstalacion = v;return this; }
  setEstado(v)          { this._b.estado = v;          return this; }
  build() {
    return new Bateria(this._b.id, this._b.tipo, this._b.capacidadKwh, this._b.ciclosVida, this._b.fechaInstalacion, this._b.estado);
  }
}
