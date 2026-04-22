using System;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Controllers;
using AutosCliente.Services;

namespace AutosCliente.Views
{
    public class FormBuscar : Form
    {
        private readonly VehiculoController _controller;

        public FormBuscar(AutoService service)
        {
            _controller = new VehiculoController(service);
            Text = "Buscar Vehículo";
            Size = new Size(440, 460);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var tabs = new TabControl { Left = 10, Top = 10, Width = 400, Height = 400 };
            tabs.TabPages.Add(TabElectrico());
            tabs.TabPages.Add(TabHibrido());
            Controls.Add(tabs);
        }

        private TabPage TabElectrico()
        {
            var tab = new TabPage("⚡ Auto Eléctrico");
            var (txtId, txtRes, btn) = CrearBuscador(tab);
            btn.Click += async (s, e) =>
            {
                txtRes.Text = "";
                var (ok, msg, a) = await _controller.BuscarElectrico(txtId.Text);
                if (!ok) { MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                txtRes.Text = $"ID            : {a!.Id}\nMarca         : {a.Marca}\nModelo        : {a.Modelo}\n" +
                              $"Año           : {a.Anio}\nAutonomía     : {a.AutonomiaKm} km\n" +
                              $"Fecha Registro: {a.FechaRegistro:yyyy-MM-dd HH:mm}\n\n── Batería ──\n" +
                              $"Tipo          : {a.Bateria?.Tipo}\nCapacidad     : {a.Bateria?.CapacidadKwh} kWh\n" +
                              $"Ciclos de vida: {a.Bateria?.CiclosVida}";
            };
            return tab;
        }

        private TabPage TabHibrido()
        {
            var tab = new TabPage("🔋 Auto Híbrido");
            var (txtId, txtRes, btn) = CrearBuscador(tab);
            btn.Click += async (s, e) =>
            {
                txtRes.Text = "";
                var (ok, msg, a) = await _controller.BuscarHibrido(txtId.Text);
                if (!ok) { MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                txtRes.Text = $"ID            : {a!.Id}\nMarca         : {a.Marca}\nModelo        : {a.Modelo}\n" +
                              $"Año           : {a.Anio}\nAutonomía     : {a.AutonomiaKm} km\n" +
                              $"Fecha Registro: {a.FechaRegistro:yyyy-MM-dd HH:mm}\n" +
                              $"Consumo       : {a.ConsumoCombustibleL100km} L/100km\n" +
                              $"Batería       : {a.CapacidadBateriaKwh} kWh";
            };
            return tab;
        }

        private static (TextBox txtId, RichTextBox txtRes, Button btn) CrearBuscador(TabPage tab)
        {
            var grp1 = new GroupBox { Text = "Buscar por ID", Left = 5, Top = 5, Width = 375, Height = 70, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            grp1.Controls.Add(new Label { Text = "ID:", Left = 10, Top = 28, Width = 30, Font = new Font("Segoe UI", 9f) });
            var txtId = new TextBox { Left = 45, Top = 26, Width = 80 };
            var btn = new Button { Text = "🔍 Buscar", Left = 135, Top = 24, Width = 90, Height = 28, BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            grp1.Controls.AddRange(new System.Windows.Forms.Control[] { txtId, btn });

            var grp2 = new GroupBox { Text = "Resultado", Left = 5, Top = 85, Width = 375, Height = 270, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            var txtRes = new RichTextBox { Left = 10, Top = 20, Width = 350, Height = 235, ReadOnly = true, BackColor = Color.WhiteSmoke, Font = new Font("Consolas", 9.5f), BorderStyle = BorderStyle.None };
            grp2.Controls.Add(txtRes);

            tab.Controls.AddRange(new System.Windows.Forms.Control[] { grp1, grp2 });
            return (txtId, txtRes, btn);
        }
    }
}
