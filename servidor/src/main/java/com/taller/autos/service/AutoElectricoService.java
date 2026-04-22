package com.taller.autos.service;

import com.taller.autos.model.AutoElectrico;
import com.taller.autos.model.Bateria;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.stream.Collectors;

@Service
public class AutoElectricoService {

    private final List<AutoElectrico> autos = new ArrayList<>();
    private final AtomicInteger contador = new AtomicInteger(1);

    public AutoElectricoService() {
        autos.add(new AutoElectrico(contador.getAndIncrement(), "Tesla", "Model 3", 2023, 500.0,
                LocalDateTime.of(2024, 1, 15, 10, 30), new Bateria("Li-Ion", 75.0, 1500)));
        autos.add(new AutoElectrico(contador.getAndIncrement(), "BMW", "iX", 2024, 630.0,
                LocalDateTime.of(2024, 3, 20, 9, 0), new Bateria("Li-Ion", 105.0, 2000)));
        autos.add(new AutoElectrico(contador.getAndIncrement(), "Tesla", "Model S", 2022, 650.0,
                LocalDateTime.of(2023, 6, 10, 14, 0), new Bateria("Li-Po", 100.0, 1800)));
    }

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

    public List<AutoElectrico> listarTodos() { return autos; }

    public List<AutoElectrico> filtrarPorMarca(String marca) {
        return autos.stream().filter(a -> a.getMarca().equalsIgnoreCase(marca)).collect(Collectors.toList());
    }

    public List<AutoElectrico> filtrarPorAnio(int anio) {
        return autos.stream().filter(a -> a.getAnio() == anio).collect(Collectors.toList());
    }
}
