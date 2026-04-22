package com.taller.autos.service;

import com.taller.autos.model.Bateria;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.atomic.AtomicInteger;

@Service
public class BateriaService {

    private final List<Bateria> baterias = new ArrayList<>();
    private final AtomicInteger contador = new AtomicInteger(1);

    public BateriaService() {
        baterias.add(new Bateria(contador.getAndIncrement(), "Li-Ion", 75.0, 1500));
        baterias.add(new Bateria(contador.getAndIncrement(), "Li-Po", 100.0, 1800));
        baterias.add(new Bateria(contador.getAndIncrement(), "NMC", 82.0, 2000));
    }

    public Bateria agregar(Bateria bateria) {
        bateria.setId(contador.getAndIncrement());
        baterias.add(bateria);
        return bateria;
    }

    public Bateria buscarPorId(int id) {
        return baterias.stream().filter(b -> b.getId() == id).findFirst().orElse(null);
    }

    public boolean eliminar(int id) {
        return baterias.removeIf(b -> b.getId() == id);
    }

    public Bateria actualizar(int id, Bateria datos) {
        Bateria existente = buscarPorId(id);
        if (existente == null) return null;
        existente.setTipo(datos.getTipo());
        existente.setCapacidadKwh(datos.getCapacidadKwh());
        existente.setCiclosVida(datos.getCiclosVida());
        return existente;
    }

    public List<Bateria> listarTodas() { return baterias; }
}
