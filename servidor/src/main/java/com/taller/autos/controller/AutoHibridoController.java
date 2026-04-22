package com.taller.autos.controller;

import com.taller.autos.model.AutoHibrido;
import com.taller.autos.service.AutoHibridoService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/hibridos")
@CrossOrigin(origins = "*")
public class AutoHibridoController {

    private final AutoHibridoService service;

    public AutoHibridoController(AutoHibridoService service) {
        this.service = service;
    }

    @PostMapping
    public ResponseEntity<AutoHibrido> agregar(@RequestBody AutoHibrido auto) {
        return ResponseEntity.ok(service.agregar(auto));
    }

    @GetMapping("/{id}")
    public ResponseEntity<AutoHibrido> buscar(@PathVariable int id) {
        AutoHibrido auto = service.buscarPorId(id);
        return auto != null ? ResponseEntity.ok(auto) : ResponseEntity.notFound().build();
    }

    @PutMapping("/{id}")
    public ResponseEntity<AutoHibrido> actualizar(@PathVariable int id, @RequestBody AutoHibrido auto) {
        AutoHibrido actualizado = service.actualizar(id, auto);
        return actualizado != null ? ResponseEntity.ok(actualizado) : ResponseEntity.notFound().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> eliminar(@PathVariable int id) {
        return service.eliminar(id) ? ResponseEntity.noContent().build() : ResponseEntity.notFound().build();
    }

    @GetMapping
    public List<AutoHibrido> listar() {
        return service.listarTodos();
    }

    @GetMapping("/filtrar/marca/{marca}")
    public List<AutoHibrido> filtrarPorMarca(@PathVariable String marca) {
        return service.filtrarPorMarca(marca);
    }

    @GetMapping("/filtrar/anio/{anio}")
    public List<AutoHibrido> filtrarPorAnio(@PathVariable int anio) {
        return service.filtrarPorAnio(anio);
    }

    @GetMapping("/{id}/costo")
    public ResponseEntity<Map<String, Object>> calcularCosto(@PathVariable int id) {
        AutoHibrido auto = service.buscarPorId(id);
        if (auto == null) return ResponseEntity.notFound().build();
        return ResponseEntity.ok(Map.of(
                "tipo", "AutoHibrido",
                "marca", auto.getMarca(),
                "modelo", auto.getModelo(),
                "costoOperacionUSD100km", auto.calcularCostoOperacion(),
                "descuento", auto.aplicarDescuento()
        ));
    }
}
