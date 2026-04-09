using System;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Models;
using AutosCliente.Services;

namespace AutosCliente.Forms
{
    public class FormActualizar : Form
    {
        private readonly AutoService _service = new();
        private TextBox txtBuscarId;
        private TextBox txtMarca, txtModelo, txtAnio, txtAutonomia, txtBatTipo, txtBatCapacidad, txtBatCiclos;
        private Button btnActualizar;
        private int _idActual;

        public FormActualizar()
        {
            Text = "Actualizar Auto Eléctrico";
            Size = new Size(440, 580);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // Paso 1 — Buscar
            var grpBuscar = new GroupBox
            {
                Text = "Paso 1 — Buscar el auto a modificar",
                Left = 15, Top = 10, Width = 395, Height = 75,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            grpBuscar.Controls.Add(new Label { Text = "ID del auto:", Left = 10, Top = 30, Width = 100, Font = new Font("Segoe UI", 9f) });
            txtBuscarId = new TextBox { Left = 115, Top = 28, Width = 80 };
            grpBuscar.Controls.Add(txtBuscarId);
            var btnBuscar = new Button
            {
                Text = "🔍 Buscar",
                Left = 205, Top = 26, Width = 90, Height = 28,
                BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
            };
            grpBuscar.Controls.Add(btnBuscar);

            // Paso 2 — Editar datos del auto
            var grpAuto = new GroupBox
            {
                Text = "Paso 2 — Modificar datos del auto",
                Left = 15, Top = 95, Width = 395, Height = 200,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            txtMarca     = Campo(grpAuto, "Marca:",           10, 30);
            txtModelo    = Campo(grpAuto, "Modelo:",          10, 70);
            txtAnio      = Campo(grpAuto, "Año:",             10, 110);
            txtAutonomia = Campo(grpAuto, "Autonomía (km):",  10, 150);

            // Paso 2b — Editar batería
            var grpBat = new GroupBox
            {
                Text = "Batería",
                Left = 15, Top = 305, Width = 395, Height = 165,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            txtBatTipo      = Campo(grpBat, "Tipo (ej: Li-Ion):", 10, 30);
            txtBatCapacidad = Campo(grpBat, "Capacidad (kWh):",   10, 70);
            txtBatCiclos    = Campo(grpBat, "Ciclos de vida:",    10, 110);

            // Botón guardar
            btnActualizar = new Button
            {
                Text = "💾  Guardar Cambios",
                Left = 15, Top = 480, Width = 395, Height = 40,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 150, 80), ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat, Enabled = false
            };

            btnBuscar.Click += async (s, e) =>
            {
                if (!int.TryParse(txtBuscarId.Text, out int id))
                {
                    MessageBox.Show("Ingrese un ID numérico válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    var auto = await _service.BuscarPorId(id);
                    if (auto == null) { MessageBox.Show("No se encontró ningún auto con ese ID."); return; }
                    _idActual = auto.Id;
                    txtMarca.Text        = auto.Marca;
                    txtModelo.Text       = auto.Modelo;
                    txtAnio.Text         = auto.Anio.ToString();
                    txtAutonomia.Text    = auto.AutonomiaKm.ToString();
                    txtBatTipo.Text      = auto.Bateria?.Tipo ?? "";
                    txtBatCapacidad.Text = auto.Bateria?.CapacidadKwh.ToString() ?? "";
                    txtBatCiclos.Text    = auto.Bateria?.CiclosVida.ToString() ?? "";
                    btnActualizar.Enabled = true;
                    MessageBox.Show("✅ Auto cargado. Modifique los campos y presione 'Guardar Cambios'.",
                        "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnActualizar.Click += async (s, e) =>
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
                    var resultado = await _service.Actualizar(_idActual, auto);
                    MessageBox.Show(resultado != null ? "✅ Auto actualizado correctamente." : "❌ Error al actualizar.");
                    if (resultado != null) Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            Controls.AddRange(new Control[] { grpBuscar, grpAuto, grpBat, btnActualizar });
        }

        private static TextBox Campo(Control contenedor, string etiqueta, int x, int y)
        {
            contenedor.Controls.Add(new Label { Text = etiqueta, Left = x, Top = y, Width = 155, Font = new Font("Segoe UI", 9f) });
            var txt = new TextBox { Left = x + 160, Top = y - 2, Width = 200 };
            contenedor.Controls.Add(txt);
            return txt;
        }
    }
}
