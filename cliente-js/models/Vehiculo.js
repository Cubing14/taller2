// Modelo base — Clase A
export class Vehiculo {
  constructor(id, marca, modelo, anio, autonomiaKm, fechaRegistro) {
    this.id = id;
    this.marca = marca;
    this.modelo = modelo;
    this.anio = anio;
    this.autonomiaKm = autonomiaKm;
    this.fechaRegistro = fechaRegistro;
  }
}
