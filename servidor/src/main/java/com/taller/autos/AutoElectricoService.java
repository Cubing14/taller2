package com.taller.autos;

import org.springframework.stereotype.Service;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.stream.Collectors;

@Service
public class AutoElectricoService {

    private final List<AutoElectrico> autos = new ArrayList<>();
    private final AtomicInteger contador = new AtomicInteger(1);

    public AutoElectrico agregar(AutoElectrico auto) {
        auto.setId(contador.getAndIncrement());
        autos.add(auto);
        return auto;
    }

    public AutoElectrico buscarPorId(int id) {
        return autos.stream().filter(a -> a.getId() == id).findFirst().orElse(null);
    }

    public boolean eliminar(int id) {
        return autos.removeIf(a -> a.getId() == id);
    }

    public AutoElectrico actualizar(int id, AutoElectrico datos) {
        AutoElectrico existente = buscarPorId(id);
        if (existente == null) return null;
        existente.setMarca(datos.getMarca());
        existente.setModelo(datos.getModelo());
        existente.setAnio(datos.getAnio());
        existente.setAutonomiaKm(datos.getAutonomiaKm());
        existente.setFechaRegistro(datos.getFechaRegistro());
        existente.setBateria(datos.getBateria());
        return existente;
    }

    public List<AutoElectrico> listarTodos() {
        return autos;
    }

    public List<AutoElectrico> filtrarPorMarca(String marca) {
        return autos.stream()
                .filter(a -> a.getMarca().equalsIgnoreCase(marca))
                .collect(Collectors.toList());
    }

    public List<AutoElectrico> filtrarPorAnio(int anio) {
        return autos.stream()
                .filter(a -> a.getAnio() == anio)
                .collect(Collectors.toList());
    }
}
