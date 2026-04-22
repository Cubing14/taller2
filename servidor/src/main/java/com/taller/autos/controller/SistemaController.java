package com.taller.autos.controller;

import com.taller.autos.model.SistemaInfo;
import org.springframework.web.bind.annotation.*;

import java.util.Map;

@RestController
@RequestMapping("/sistema")
@CrossOrigin(origins = "*")
public class SistemaController {

    @GetMapping
    public Map<String, Object> info() {
        SistemaInfo info = SistemaInfo.getInstance();
        return Map.of(
                "nombre", info.getNombreSistema(),
                "version", info.getVersion(),
                "empresa", info.getEmpresa(),
                "integrantes", info.getIntegrantes()
        );
    }
}
