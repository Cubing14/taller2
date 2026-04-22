package com.taller.autos.model;

import java.time.LocalDateTime;

// Clase B: hereda de Vehiculo (A), tiene Bateria (D), sobreescribe calcularCostoOperacion
public class AutoElectrico extends Vehiculo {
    private Bateria bateria;

    public AutoElectrico() {}

    public AutoElectrico(int id, String marca, String modelo, int anio, double autonomiaKm,
                         LocalDateTime fechaRegistro, Bateria bateria) {
        super(id, marca, modelo, anio, autonomiaKm, fechaRegistro);
        this.bateria = bateria;
    }

    // Costo = (kWh / km) * 100km * 0.15 USD/kWh
    @Override
    public double calcularCostoOperacion() {
        if (bateria == null || autonomiaKm == 0) return 0;
        return (bateria.getCapacidadKwh() / autonomiaKm) * 100 * 0.15;
    }

    public Bateria getBateria() { return bateria; }
    public void setBateria(Bateria bateria) { this.bateria = bateria; }
}
