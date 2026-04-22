package com.taller.autos.controller;

import com.taller.autos.model.Bateria;
import com.taller.autos.service.BateriaService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/baterias")
@CrossOrigin(origins = "*")
public class BateriaController {

    private final BateriaService service;

    public BateriaController(BateriaService service) {
        this.service = service;
    }

    @PostMapping
    public ResponseEntity<Bateria> agregar(@RequestBody Bateria bateria) {
        return ResponseEntity.ok(service.agregar(bateria));
    }

    @GetMapping("/{id}")
    public ResponseEntity<Bateria> buscar(@PathVariable int id) {
        Bateria b = service.buscarPorId(id);
        return b != null ? ResponseEntity.ok(b) : ResponseEntity.notFound().build();
    }

    @PutMapping("/{id}")
    public ResponseEntity<Bateria> actualizar(@PathVariable int id, @RequestBody Bateria bateria) {
        Bateria actualizada = service.actualizar(id, bateria);
        return actualizada != null ? ResponseEntity.ok(actualizada) : ResponseEntity.notFound().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> eliminar(@PathVariable int id) {
        return service.eliminar(id) ? ResponseEntity.noContent().build() : ResponseEntity.notFound().build();
    }

    @GetMapping
    public List<Bateria> listar() {
        return service.listarTodas();
    }
}
