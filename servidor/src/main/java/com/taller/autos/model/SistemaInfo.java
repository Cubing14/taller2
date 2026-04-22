package com.taller.autos.model;

public class SistemaInfo {
    private static SistemaInfo instancia;

    private final String nombreSistema = "Gestión de Vehículos Eléctricos e Híbridos";
    private final String version = "1.0";
    private final String empresa = "Taller 2 - Apps Empresariales";
    private final String[] integrantes = {"Yaser Rondón", "Ismael Cardozo", "Juan Mancipe"};

    private SistemaInfo() {}

    public static SistemaInfo getInstance() {
        if (instancia == null) instancia = new SistemaInfo();
        return instancia;
    }

    public String getNombreSistema() { return nombreSistema; }
    public String getVersion() { return version; }
    public String getEmpresa() { return empresa; }
    public String[] getIntegrantes() { return integrantes; }
}
