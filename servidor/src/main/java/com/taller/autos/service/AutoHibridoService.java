package com.taller.autos.service;

import com.taller.autos.model.AutoHibrido;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.stream.Collectors;

@Service
public class AutoHibridoService {

    private final List<AutoHibrido> autos = new ArrayList<>();
    private final AtomicInteger contador = new AtomicInteger(100);

    public AutoHibridoService() {
        autos.add(new AutoHibrido(contador.getAndIncrement(), "Toyota", "Prius", 2023, 800.0,
                LocalDateTime.of(2024, 2, 10, 8, 0), 4.5, 8.8));
        autos.add(new AutoHibrido(contador.getAndIncrement(), "Honda", "Accord Hybrid", 2022, 750.0,
                LocalDateTime.of(2023, 11, 5, 11, 0), 5.0, 1.3));
    }

    public AutoHibrido agregar(AutoHibrido auto) {
        auto.setId(contador.getAndIncrement());
        autos.add(auto);
        return auto;
    }

    public AutoHibrido buscarPorId(int id) {
        return autos.stream().filter(a -> a.getId() == id).findFirst().orElse(null);
    }

    public boolean eliminar(int id) {
        return autos.removeIf(a -> a.getId() == id);
    }

    public AutoHibrido actualizar(int id, AutoHibrido datos) {
        AutoHibrido existente = buscarPorId(id);
        if (existente == null) return null;
        existente.setMarca(datos.getMarca());
        existente.setModelo(datos.getModelo());
        existente.setAnio(datos.getAnio());
        existente.setAutonomiaKm(datos.getAutonomiaKm());
        existente.setFechaRegistro(datos.getFechaRegistro());
        existente.setConsumoCombustibleL100km(datos.getConsumoCombustibleL100km());
        existente.setCapacidadBateriaKwh(datos.getCapacidadBateriaKwh());
        return existente;
    }

    public List<AutoHibrido> listarTodos() { return autos; }

    public List<AutoHibrido> filtrarPorMarca(String marca) {
        return autos.stream().filter(a -> a.getMarca().equalsIgnoreCase(marca)).collect(Collectors.toList());
    }

    public List<AutoHibrido> filtrarPorAnio(int anio) {
        return autos.stream().filter(a -> a.getAnio() == anio).collect(Collectors.toList());
    }
}
