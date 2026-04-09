using System;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Models;
using AutosCliente.Services;

namespace AutosCliente.Forms
{
    public class FormAgregar : Form
    {
        private readonly AutoService _service = new();
        private TextBox txtMarca, txtModelo, txtAnio, txtAutonomia, txtBatTipo, txtBatCapacidad, txtBatCiclos;

        public FormAgregar()
        {
            Text = "Agregar Auto Eléctrico";
            Size = new Size(420, 500);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // ── Grupo datos del auto ──────────────────────────────────────
            var grpAuto = new GroupBox
            {
                Text = "Datos del Auto",
                Left = 15, Top = 10, Width = 375, Height = 200,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };

            txtMarca      = Campo(grpAuto, "Marca:",          20,  30);
            txtModelo     = Campo(grpAuto, "Modelo:",         20,  70);
            txtAnio       = Campo(grpAuto, "Año (ej: 2023):", 20, 110);
            txtAutonomia  = Campo(grpAuto, "Autonomía (km):", 20, 150);

            // ── Grupo batería ─────────────────────────────────────────────
            var grpBat = new GroupBox
            {
                Text = "Batería",
                Left = 15, Top = 220, Width = 375, Height = 170,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };

            txtBatTipo      = Campo(grpBat, "Tipo (ej: Li-Ion):",    20,  30);
            txtBatCapacidad = Campo(grpBat, "Capacidad (kWh):",      20,  70);
            txtBatCiclos    = Campo(grpBat, "Ciclos de vida:",        20, 110);

            var btnGuardar = new Button
            {
                Text = "💾  Guardar Auto",
                Left = 15, Top = 400, Width = 375, Height = 40,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnGuardar.Click += async (s, e) =>
            {
                try
                {
                    var auto = new AutoElectrico
                    {
                        Marca        = txtMarca.Text.Trim(),
                        Modelo       = txtModelo.Text.Trim(),
                        Anio         = int.Parse(txtAnio.Text),
                        AutonomiaKm  = double.Parse(txtAutonomia.Text),
                        FechaRegistro = DateTime.Now,
                        Bateria = new Bateria
                        {
                            Tipo         = txtBatTipo.Text.Trim(),
                            CapacidadKwh = double.Parse(txtBatCapacidad.Text),
                            CiclosVida   = int.Parse(txtBatCiclos.Text)
                        }
                    };
                    var resultado = await _service.Agregar(auto);
                    MessageBox.Show($"✅ Auto guardado correctamente.\nID asignado: {resultado?.Id}",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            Controls.AddRange(new Control[] { grpAuto, grpBat, btnGuardar });
        }

        // Crea un Label + TextBox dentro de un contenedor en posición fija
        private static TextBox Campo(Control contenedor, string etiqueta, int x, int y)
        {
            contenedor.Controls.Add(new Label
            {
                Text = etiqueta, Left = x, Top = y,
                Width = 150, Font = new Font("Segoe UI", 9f)
            });
            var txt = new TextBox { Left = x + 155, Top = y - 2, Width = 190 };
            contenedor.Controls.Add(txt);
            return txt;
        }
    }
}
