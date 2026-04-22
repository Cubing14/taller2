package com.taller.autos.model;

import java.time.LocalDateTime;

// Clase C: hereda de Vehiculo (A), implementa IAplicable, sobreescribe calcularCostoOperacion
public class AutoHibrido extends Vehiculo implements IAplicable {
    private double consumoCombustibleL100km;
    private double capacidadBateriaKwh;

    public AutoHibrido() {}

    public AutoHibrido(int id, String marca, String modelo, int anio, double autonomiaKm,
                       LocalDateTime fechaRegistro, double consumoCombustibleL100km, double capacidadBateriaKwh) {
        super(id, marca, modelo, anio, autonomiaKm, fechaRegistro);
        this.consumoCombustibleL100km = consumoCombustibleL100km;
        this.capacidadBateriaKwh = capacidadBateriaKwh;
    }

    // Costo = (combustible * 1.5 USD/L) + (kWh/km * 100 * 0.15 USD/kWh)
    @Override
    public double calcularCostoOperacion() {
        double costoCombustible = consumoCombustibleL100km * 1.5;
        double costoElectrico = (autonomiaKm > 0) ? (capacidadBateriaKwh / autonomiaKm) * 100 * 0.15 : 0;
        return costoCombustible + costoElectrico;
    }

    @Override
    public String aplicarDescuento() {
        return String.format("Descuento híbrido 10%%. Costo ajustado: %.4f USD/100km",
                calcularCostoOperacion() * 0.90);
    }

    public double getConsumoCombustibleL100km() { return consumoCombustibleL100km; }
    public void setConsumoCombustibleL100km(double v) { this.consumoCombustibleL100km = v; }
    public double getCapacidadBateriaKwh() { return capacidadBateriaKwh; }
    public void setCapacidadBateriaKwh(double v) { this.capacidadBateriaKwh = v; }
}
