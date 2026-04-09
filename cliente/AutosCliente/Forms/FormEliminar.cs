using System;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Models;
using AutosCliente.Services;

namespace AutosCliente.Forms
{
    public class FormEliminar : Form
    {
        private readonly AutoService _service = new();
        private TextBox txtId;
        private RichTextBox txtInfo;
        private Button btnEliminar;
        private AutoElectrico? _autoEncontrado;

        public FormEliminar()
        {
            Text = "Eliminar Auto Eléctrico";
            Size = new Size(420, 450);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // Paso 1
            var grpPaso1 = new GroupBox
            {
                Text = "Paso 1 — Buscar el auto a eliminar",
                Left = 15, Top = 10, Width = 375, Height = 80,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };

            grpPaso1.Controls.Add(new Label
            {
                Text = "ID del auto:",
                Left = 10, Top = 30, Width = 100,
                Font = new Font("Segoe UI", 9f)
            });

            txtId = new TextBox { Left = 115, Top = 28, Width = 80 };
            grpPaso1.Controls.Add(txtId);

            var btnBuscar = new Button
            {
                Text = "🔍 Buscar",
                Left = 205, Top = 26, Width = 90, Height = 28,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White, FlatStyle = FlatStyle.Flat
            };
            grpPaso1.Controls.Add(btnBuscar);

            // Paso 2
            var grpPaso2 = new GroupBox
            {
                Text = "Paso 2 — Verificar información antes de eliminar",
                Left = 15, Top = 100, Width = 375, Height = 250,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };

            txtInfo = new RichTextBox
            {
                Left = 10, Top = 20, Width = 350, Height = 215,
                ReadOnly = true, BackColor = Color.FromArgb(255, 245, 245),
                Font = new Font("Consolas", 9.5f), BorderStyle = BorderStyle.None
            };
            grpPaso2.Controls.Add(txtInfo);

            // Paso 3
            btnEliminar = new Button
            {
                Text = "🗑️  Confirmar Eliminación",
                Left = 15, Top = 360, Width = 375, Height = 40,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                BackColor = Color.FromArgb(200, 50, 50),
                ForeColor = Color.White, FlatStyle = FlatStyle.Flat,
                Enabled = false
            };

            btnBuscar.Click += async (s, e) =>
            {
                txtInfo.Text = "";
                btnEliminar.Enabled = false;
                _autoEncontrado = null;

                if (!int.TryParse(txtId.Text, out int id))
                {
                    MessageBox.Show("Ingrese un ID numérico válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    _autoEncontrado = await _service.BuscarPorId(id);
                    if (_autoEncontrado == null) { MessageBox.Show("No se encontró ningún auto con ese ID."); return; }

                    txtInfo.Text =
                        $"ID            : {_autoEncontrado.Id}\n" +
                        $"Marca         : {_autoEncontrado.Marca}\n" +
                        $"Modelo        : {_autoEncontrado.Modelo}\n" +
                        $"Año           : {_autoEncontrado.Anio}\n" +
                        $"Autonomía     : {_autoEncontrado.AutonomiaKm} km\n" +
                        $"Fecha Registro: {_autoEncontrado.FechaRegistro:yyyy-MM-dd HH:mm}\n\n" +
                        $"── Batería ──────────────────\n" +
                        $"Tipo          : {_autoEncontrado.Bateria?.Tipo}\n" +
                        $"Capacidad     : {_autoEncontrado.Bateria?.CapacidadKwh} kWh\n" +
                        $"Ciclos de vida: {_autoEncontrado.Bateria?.CiclosVida}";

                    btnEliminar.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnEliminar.Click += async (s, e) =>
            {
                if (_autoEncontrado == null) return;
                var confirm = MessageBox.Show(
                    $"¿Está seguro de eliminar el auto '{_autoEncontrado.Marca} {_autoEncontrado.Modelo}'?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;

                bool ok = await _service.Eliminar(_autoEncontrado.Id);
                MessageBox.Show(ok ? "✅ Auto eliminado correctamente." : "❌ Error al eliminar.");
                if (ok) Close();
            };

            Controls.AddRange(new Control[] { grpPaso1, grpPaso2, btnEliminar });
        }
    }
}
