using System;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;
using AutosCliente.Controllers;
using AutosCliente.Services;

namespace AutosCliente.Views
{
    public class FormCalcular : Form
    {
        private readonly VehiculoController _controller;

        public FormCalcular(AutoService service)
        {
            _controller = new VehiculoController(service);
            Text = "Calcular Costo de Operación (Polimorfismo)";
            Size = new Size(500, 420);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            Controls.Add(new Label
            {
                Text = "Mismo método calcularCostoOperacion() — resultado distinto según AutoElectrico o AutoHibrido.",
                Left = 15, Top = 10, Width = 460, Height = 35,
                Font = new Font("Segoe UI", 9f), ForeColor = Color.FromArgb(80, 80, 80)
            });

            var tabs = new TabControl { Left = 10, Top = 55, Width = 460, Height = 310 };
            tabs.TabPages.Add(TabElectrico());
            tabs.TabPages.Add(TabHibrido());
            Controls.Add(tabs);
        }

        private TabPage TabElectrico()
        {
            var tab = new TabPage("⚡ Auto Eléctrico");
            var (txtId, txtRes, btn) = CrearCalculador(tab);
            btn.Click += async (s, e) =>
            {
                txtRes.Text = "";
                if (!int.TryParse(txtId.Text, out int id)) { MessageBox.Show("ID inválido."); return; }
                var json = await _controller.ObtenerCostoElectrico(id);
                if (json == null) { MessageBox.Show("No encontrado."); return; }
                var r = JsonDocument.Parse(json).RootElement;
                txtRes.Text = $"Tipo   : {r.GetProperty("tipo").GetString()}\n" +
                              $"Marca  : {r.GetProperty("marca").GetString()}\n" +
                              $"Modelo : {r.GetProperty("modelo").GetString()}\n\n" +
                              $"Costo  : {r.GetProperty("costoOperacionUSD100km").GetDouble():F4} USD/100km\n\n" +
                              $"Fórmula: (kWh / km) × 100 × 0.15 USD/kWh";
            };
            return tab;
        }

        private TabPage TabHibrido()
        {
            var tab = new TabPage("🔋 Auto Híbrido");
            var (txtId, txtRes, btn) = CrearCalculador(tab);
            btn.Click += async (s, e) =>
            {
                txtRes.Text = "";
                if (!int.TryParse(txtId.Text, out int id)) { MessageBox.Show("ID inválido."); return; }
                var json = await _controller.ObtenerCostoHibrido(id);
                if (json == null) { MessageBox.Show("No encontrado."); return; }
                var r = JsonDocument.Parse(json).RootElement;
                txtRes.Text = $"Tipo   : {r.GetProperty("tipo").GetString()}\n" +
                              $"Marca  : {r.GetProperty("marca").GetString()}\n" +
                              $"Modelo : {r.GetProperty("modelo").GetString()}\n\n" +
                              $"Costo  : {r.GetProperty("costoOperacionUSD100km").GetDouble():F4} USD/100km\n" +
                              $"Descuento: {r.GetProperty("descuento").GetString()}\n\n" +
                              $"Fórmula: (L × 1.5) + (kWh/km × 100 × 0.15)";
            };
            return tab;
        }

        private static (TextBox, RichTextBox, Button) CrearCalculador(TabPage tab)
        {
            var grp1 = new GroupBox { Text = "ID del vehículo", Left = 5, Top = 5, Width = 435, Height = 60, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            grp1.Controls.Add(new Label { Text = "ID:", Left = 10, Top = 24, Width = 30, Font = new Font("Segoe UI", 9f) });
            var txtId = new TextBox { Left = 45, Top = 22, Width = 80 };
            var btn = new Button { Text = "⚙️ Calcular", Left = 135, Top = 20, Width = 100, Height = 28, BackColor = Color.FromArgb(100, 60, 180), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            grp1.Controls.AddRange(new System.Windows.Forms.Control[] { txtId, btn });

            var grp2 = new GroupBox { Text = "Resultado", Left = 5, Top = 75, Width = 435, Height = 185, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            var txtRes = new RichTextBox { Left = 10, Top = 20, Width = 410, Height = 150, ReadOnly = true, BackColor = Color.FromArgb(245, 245, 255), Font = new Font("Consolas", 9.5f), BorderStyle = BorderStyle.None };
            grp2.Controls.Add(txtRes);

            tab.Controls.AddRange(new System.Windows.Forms.Control[] { grp1, grp2 });
            return (txtId, txtRes, btn);
        }
    }
}
