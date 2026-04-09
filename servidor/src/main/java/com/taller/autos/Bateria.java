package com.taller.autos;

public class Bateria {
    private String tipo;
    private double capacidadKwh;
    private int ciclosVida;

    public Bateria() {}

    public Bateria(String tipo, double capacidadKwh, int ciclosVida) {
        this.tipo = tipo;
        this.capacidadKwh = capacidadKwh;
        this.ciclosVida = ciclosVida;
    }

    public String getTipo() { return tipo; }
    public void setTipo(String tipo) { this.tipo = tipo; }

    public double getCapacidadKwh() { return capacidadKwh; }
    public void setCapacidadKwh(double capacidadKwh) { this.capacidadKwh = capacidadKwh; }

    public int getCiclosVida() { return ciclosVida; }
    public void setCiclosVida(int ciclosVida) { this.ciclosVida = ciclosVida; }
}
