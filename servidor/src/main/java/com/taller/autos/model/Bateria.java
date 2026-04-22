package com.taller.autos.model;

public class Bateria {
    private int id;
    private String tipo;
    private double capacidadKwh;
    private int ciclosVida;

    public Bateria() {}

    public Bateria(int id, String tipo, double capacidadKwh, int ciclosVida) {
        this.id = id; this.tipo = tipo; this.capacidadKwh = capacidadKwh; this.ciclosVida = ciclosVida;
    }

    public Bateria(String tipo, double capacidadKwh, int ciclosVida) {
        this.tipo = tipo; this.capacidadKwh = capacidadKwh; this.ciclosVida = ciclosVida;
    }

    public int getId() { return id; }
    public void setId(int id) { this.id = id; }
    public String getTipo() { return tipo; }
    public void setTipo(String tipo) { this.tipo = tipo; }
    public double getCapacidadKwh() { return capacidadKwh; }
    public void setCapacidadKwh(double v) { this.capacidadKwh = v; }
    public int getCiclosVida() { return ciclosVida; }
    public void setCiclosVida(int v) { this.ciclosVida = v; }
}
