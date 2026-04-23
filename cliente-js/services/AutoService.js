const BASE = 'http://localhost:8080';

// Patrón Observer — los observadores se suscriben para recibir notificaciones
class AutoService {
  #observers = [];

  suscribir(observer)   { this.#observers.push(observer); }
  desuscribir(observer) { this.#observers = this.#observers.filter(o => o !== observer); }
  #notificar()          { this.#observers.forEach(o => o.onAutosActualizados()); }

  async #get(url) {
    const r = await fetch(`${BASE}${url}`);
    if (!r.ok) return null;
    return r.json();
  }

  async #post(url, body) {
    const r = await fetch(`${BASE}${url}`, { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(body) });
    return r.ok ? r.json() : null;
  }

  async #put(url, body) {
    const r = await fetch(`${BASE}${url}`, { method: 'PUT', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(body) });
    return r.ok ? r.json() : null;
  }

  async #delete(url) {
    const r = await fetch(`${BASE}${url}`, { method: 'DELETE' });
    return r.ok;
  }

  // ── AutoElectrico (B) ─────────────────────────────────────────────────
  async agregarElectrico(auto)        { const r = await this.#post('/autos', auto);       if (r) this.#notificar(); return r; }
  async buscarElectricoPorId(id)      { return this.#get(`/autos/${id}`); }
  async actualizarElectrico(id, auto) { const r = await this.#put(`/autos/${id}`, auto);  if (r) this.#notificar(); return r; }
  async eliminarElectrico(id)         { const ok = await this.#delete(`/autos/${id}`);    if (ok) this.#notificar(); return ok; }
  async listarElectricos()            { return this.#get('/autos'); }
  async filtrarElectricosPorMarca(m)  { return this.#get(`/autos/filtrar/marca/${m}`); }
  async filtrarElectricosPorAnio(a)   { return this.#get(`/autos/filtrar/anio/${a}`); }
  async costoElectrico(id)            { return this.#get(`/autos/${id}/costo`); }

  // ── AutoHibrido (C) ───────────────────────────────────────────────────
  async agregarHibrido(auto)        { const r = await this.#post('/hibridos', auto);       if (r) this.#notificar(); return r; }
  async buscarHibridoPorId(id)      { return this.#get(`/hibridos/${id}`); }
  async actualizarHibrido(id, auto) { const r = await this.#put(`/hibridos/${id}`, auto);  if (r) this.#notificar(); return r; }
  async eliminarHibrido(id)         { const ok = await this.#delete(`/hibridos/${id}`);    if (ok) this.#notificar(); return ok; }
  async listarHibridos()            { return this.#get('/hibridos'); }
  async filtrarHibridosPorMarca(m)  { return this.#get(`/hibridos/filtrar/marca/${m}`); }
  async filtrarHibridosPorAnio(a)   { return this.#get(`/hibridos/filtrar/anio/${a}`); }
  // ── Bateria ──────────────────────────────────────────────────────────
  async agregarBateria(bateria)        { const r = await this.#post('/baterias', bateria);       return r; }
  async buscarBateriaPorId(id)         { return this.#get(`/baterias/${id}`); }
  async actualizarBateria(id, bateria) { const r = await this.#put(`/baterias/${id}`, bateria);  return r; }
  async eliminarBateria(id)            { const ok = await this.#delete(`/baterias/${id}`);       return ok; }
  async listarBaterias()               { return this.#get('/baterias'); }
  async filtrarBateriasPorTipo(tipo)   { return this.#get(`/baterias/filtrar/tipo/${tipo}`); }

  // ── Bateria (D) ───────────────────────────────────────────────────────
  async agregarBateria(b)        { return this.#post('/baterias', b); }
  async buscarBateriaPorId(id)   { return this.#get(`/baterias/${id}`); }
  async actualizarBateria(id, b) { return this.#put(`/baterias/${id}`, b); }
  async eliminarBateria(id)      { return this.#delete(`/baterias/${id}`); }
  async listarBaterias()         { return this.#get('/baterias'); }
}

// Instancia única exportada — usada por todos los controllers y vistas
export const autoService = new AutoService();
