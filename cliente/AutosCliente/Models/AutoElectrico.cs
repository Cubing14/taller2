using System;

namespace AutosCliente.Models
{
    public class Bateria
    {
        public string Tipo { get; set; } = "";
        public double CapacidadKwh { get; set; }
        public int CiclosVida { get; set; }
    }

    public class AutoElectrico
    {
        public int Id { get; set; }
        public string Marca { get; set; } = "";
        public string Modelo { get; set; } = "";
        public int Anio { get; set; }
        public double AutonomiaKm { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Bateria Bateria { get; set; } = new();
    }
}
