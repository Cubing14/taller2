package com.taller.autos.model;

import java.time.LocalDateTime;

public abstract class Vehiculo {
    protected int id;
    protected String marca;
    protected String modelo;
    protected int anio;
    protected double autonomiaKm;
    protected LocalDateTime fechaRegistro;

    public Vehiculo() {}

    public Vehiculo(int id, String marca, String modelo, int anio, double autonomiaKm, LocalDateTime fechaRegistro) {
        this.id = id; this.marca = marca; this.modelo = modelo;
        this.anio = anio; this.autonomiaKm = autonomiaKm; this.fechaRegistro = fechaRegistro;
    }

    public abstract double calcularCostoOperacion();

    public int getId() { return id; }
    public void setId(int id) { this.id = id; }
    public String getMarca() { return marca; }
    public void setMarca(String marca) { this.marca = marca; }
    public String getModelo() { return modelo; }
    public void setModelo(String modelo) { this.modelo = modelo; }
    public int getAnio() { return anio; }
    public void setAnio(int anio) { this.anio = anio; }
    public double getAutonomiaKm() { return autonomiaKm; }
    public void setAutonomiaKm(double autonomiaKm) { this.autonomiaKm = autonomiaKm; }
    public LocalDateTime getFechaRegistro() { return fechaRegistro; }
    public void setFechaRegistro(LocalDateTime fechaRegistro) { this.fechaRegistro = fechaRegistro; }
}
