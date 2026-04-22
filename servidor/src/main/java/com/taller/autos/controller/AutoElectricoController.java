package com.taller.autos.controller;

import com.taller.autos.model.AutoElectrico;
import com.taller.autos.service.AutoElectricoService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/autos")
@CrossOrigin(origins = "*")
public class AutoElectricoController {

    private final AutoElectricoService service;

    public AutoElectricoController(AutoElectricoService service) {
        this.service = service;
    }

    @PostMapping
    public ResponseEntity<AutoElectrico> agregar(@RequestBody AutoElectrico auto) {
        return ResponseEntity.ok(service.agregar(auto));
    }

    @GetMapping("/{id}")
    public ResponseEntity<AutoElectrico> buscar(@PathVariable int id) {
        AutoElectrico auto = service.buscarPorId(id);
        return auto != null ? ResponseEntity.ok(auto) : ResponseEntity.notFound().build();
    }

    @PutMapping("/{id}")
    public ResponseEntity<AutoElectrico> actualizar(@PathVariable int id, @RequestBody AutoElectrico auto) {
        AutoElectrico actualizado = service.actualizar(id, auto);
        return actualizado != null ? ResponseEntity.ok(actualizado) : ResponseEntity.notFound().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> eliminar(@PathVariable int id) {
        return service.eliminar(id) ? ResponseEntity.noContent().build() : ResponseEntity.notFound().build();
    }

    @GetMapping
    public List<AutoElectrico> listar() {
        return service.listarTodos();
    }

    @GetMapping("/filtrar/marca/{marca}")
    public List<AutoElectrico> filtrarPorMarca(@PathVariable String marca) {
        return service.filtrarPorMarca(marca);
    }

    @GetMapping("/filtrar/anio/{anio}")
    public List<AutoElectrico> filtrarPorAnio(@PathVariable int anio) {
        return service.filtrarPorAnio(anio);
    }

    @GetMapping("/{id}/costo")
    public ResponseEntity<Map<String, Object>> calcularCosto(@PathVariable int id) {
        AutoElectrico auto = service.buscarPorId(id);
        if (auto == null) return ResponseEntity.notFound().build();
        return ResponseEntity.ok(Map.of(
                "tipo", "AutoElectrico",
                "marca", auto.getMarca(),
                "modelo", auto.getModelo(),
                "costoOperacionUSD100km", auto.calcularCostoOperacion()
        ));
    }
}
