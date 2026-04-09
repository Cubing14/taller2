package com.taller.autos;

import java.time.LocalDateTime;

public class AutoElectrico {
    private int id;
    private String marca;
    private String modelo;
    private int anio;
    private double autonomiaKm;
    private LocalDateTime fechaRegistro;
    private Bateria bateria;

    public AutoElectrico() {}

    public AutoElectrico(int id, String marca, String modelo, int anio, double autonomiaKm,
                         LocalDateTime fechaRegistro, Bateria bateria) {
        this.id = id;
        this.marca = marca;
        this.modelo = modelo;
        this.anio = anio;
        this.autonomiaKm = autonomiaKm;
        this.fechaRegistro = fechaRegistro;
        this.bateria = bateria;
    }

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

    public Bateria getBateria() { return bateria; }
    public void setBateria(Bateria bateria) { this.bateria = bateria; }
}
