// Patrón Singleton — información global del sistema
export class SistemaInfo {
  static #instancia = null;

  constructor() {
    this.nombreSistema = 'Gestión de Vehículos Eléctricos e Híbridos';
    this.version = '1.0';
    this.empresa = 'Taller 2 - Apps Empresariales';
    this.integrantes = ['Yaser Rondón', 'Ismael Cardozo', 'Juan Mancipe'];
  }

  static getInstance() {
    if (!SistemaInfo.#instancia) {
      SistemaInfo.#instancia = new SistemaInfo();
    }
    return SistemaInfo.#instancia;
  }
}
