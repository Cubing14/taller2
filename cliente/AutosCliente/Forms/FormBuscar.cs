using System;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Services;

namespace AutosCliente.Forms
{
    public class FormBuscar : Form
    {
        private readonly AutoService _service = new();
        private TextBox txtId;
        private RichTextBox txtResultado;

        public FormBuscar()
        {
            Text = "Buscar Auto Eléctrico";
            Size = new Size(420, 400);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var grpBuscar = new GroupBox
            {
                Text = "Buscar por ID",
                Left = 15, Top = 10, Width = 375, Height = 80,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };

            grpBuscar.Controls.Add(new Label
            {
                Text = "Ingrese el ID del auto:",
                Left = 10, Top = 28, Width = 160,
                Font = new Font("Segoe UI", 9f)
            });

            txtId = new TextBox { Left = 175, Top = 26, Width = 80 };
            grpBuscar.Controls.Add(txtId);

            var btnBuscar = new Button
            {
                Text = "🔍 Buscar",
                Left = 265, Top = 24, Width = 90, Height = 28,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            grpBuscar.Controls.Add(btnBuscar);

            var grpResultado = new GroupBox
            {
                Text = "Información del Auto",
                Left = 15, Top = 100, Width = 375, Height = 250,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };

            txtResultado = new RichTextBox
            {
                Left = 10, Top = 20, Width = 350, Height = 215,
                ReadOnly = true, BackColor = Color.WhiteSmoke,
                Font = new Font("Consolas", 9.5f),
                BorderStyle = BorderStyle.None
            };
            grpResultado.Controls.Add(txtResultado);

            btnBuscar.Click += async (s, e) =>
            {
                txtResultado.Text = "";
                if (!int.TryParse(txtId.Text, out int id))
                {
                    MessageBox.Show("Ingrese un ID numérico válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    var auto = await _service.BuscarPorId(id);
                    if (auto == null) { MessageBox.Show("No se encontró ningún auto con ese ID.", "Sin resultados"); return; }
                    txtResultado.Text =
                        $"ID            : {auto.Id}\n" +
                        $"Marca         : {auto.Marca}\n" +
                        $"Modelo        : {auto.Modelo}\n" +
                        $"Año           : {auto.Anio}\n" +
                        $"Autonomía     : {auto.AutonomiaKm} km\n" +
                        $"Fecha Registro: {auto.FechaRegistro:yyyy-MM-dd HH:mm}\n\n" +
                        $"── Batería ──────────────────\n" +
                        $"Tipo          : {auto.Bateria?.Tipo}\n" +
                        $"Capacidad     : {auto.Bateria?.CapacidadKwh} kWh\n" +
                        $"Ciclos de vida: {auto.Bateria?.CiclosVida}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar con el servidor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            Controls.AddRange(new Control[] { grpBuscar, grpResultado });
        }
    }
}
