using System;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Controllers;
using AutosCliente.Models;
using AutosCliente.Services;

namespace AutosCliente.Views
{
    public class FormEliminar : Form
    {
        private readonly VehiculoController _controller;

        public FormEliminar(AutoService service)
        {
            _controller = new VehiculoController(service);
            Text = "Eliminar Vehículo";
            Size = new Size(440, 500);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var tabs = new TabControl { Left = 10, Top = 10, Width = 400, Height = 440 };
            tabs.TabPages.Add(TabElectrico());
            tabs.TabPages.Add(TabHibrido());
            Controls.Add(tabs);
        }

        private TabPage TabElectrico()
        {
            var tab = new TabPage("⚡ Auto Eléctrico");
            AutoElectrico? encontrado = null;
            var (txtId, txtInfo, btnBuscar, btnEliminar) = CrearEliminador(tab);

            btnBuscar.Click += async (s, e) =>
            {
                txtInfo.Text = ""; btnEliminar.Enabled = false; encontrado = null;
                var (ok, msg, a) = await _controller.BuscarElectrico(txtId.Text);
                if (!ok) { MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                encontrado = a;
                txtInfo.Text = $"ID: {a!.Id}  |  {a.Marca} {a.Modelo}  |  Año: {a.Anio}\n" +
                               $"Autonomía: {a.AutonomiaKm} km\nBatería: {a.Bateria?.Tipo} / {a.Bateria?.CapacidadKwh} kWh";
                btnEliminar.Enabled = true;
            };

            btnEliminar.Click += async (s, e) =>
            {
                if (encontrado == null) return;
                if (MessageBox.Show($"¿Eliminar '{encontrado.Marca} {encontrado.Modelo}'?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                var (ok, msg) = await _controller.EliminarElectrico(encontrado.Id);
                MessageBox.Show(ok ? $"✅ {msg}" : $"❌ {msg}");
                if (ok) Close();
            };
            return tab;
        }

        private TabPage TabHibrido()
        {
            var tab = new TabPage("🔋 Auto Híbrido");
            AutoHibrido? encontrado = null;
            var (txtId, txtInfo, btnBuscar, btnEliminar) = CrearEliminador(tab);

            btnBuscar.Click += async (s, e) =>
            {
                txtInfo.Text = ""; btnEliminar.Enabled = false; encontrado = null;
                var (ok, msg, a) = await _controller.BuscarHibrido(txtId.Text);
                if (!ok) { MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                encontrado = a;
                txtInfo.Text = $"ID: {a!.Id}  |  {a.Marca} {a.Modelo}  |  Año: {a.Anio}\n" +
                               $"Autonomía: {a.AutonomiaKm} km  |  Consumo: {a.ConsumoCombustibleL100km} L/100km";
                btnEliminar.Enabled = true;
            };

            btnEliminar.Click += async (s, e) =>
            {
                if (encontrado == null) return;
                if (MessageBox.Show($"¿Eliminar '{encontrado.Marca} {encontrado.Modelo}'?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                var (ok, msg) = await _controller.EliminarHibrido(encontrado.Id);
                MessageBox.Show(ok ? $"✅ {msg}" : $"❌ {msg}");
                if (ok) Close();
            };
            return tab;
        }

        private static (TextBox, RichTextBox, Button, Button) CrearEliminador(TabPage tab)
        {
            var grp1 = new GroupBox { Text = "Paso 1 — Buscar", Left = 5, Top = 5, Width = 375, Height = 70, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            grp1.Controls.Add(new Label { Text = "ID:", Left = 10, Top = 28, Width = 30, Font = new Font("Segoe UI", 9f) });
            var txtId = new TextBox { Left = 45, Top = 26, Width = 80 };
            var btnBuscar = new Button { Text = "🔍 Buscar", Left = 135, Top = 24, Width = 90, Height = 28, BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            grp1.Controls.AddRange(new System.Windows.Forms.Control[] { txtId, btnBuscar });

            var grp2 = new GroupBox { Text = "Paso 2 — Verificar", Left = 5, Top = 85, Width = 375, Height = 200, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            var txtInfo = new RichTextBox { Left = 10, Top = 20, Width = 350, Height = 165, ReadOnly = true, BackColor = Color.FromArgb(255, 245, 245), Font = new Font("Consolas", 9.5f), BorderStyle = BorderStyle.None };
            grp2.Controls.Add(txtInfo);

            var btnEliminar = new Button { Text = "🗑️ Confirmar Eliminación", Left = 5, Top = 295, Width = 375, Height = 38, Font = new Font("Segoe UI", 10f, FontStyle.Bold), BackColor = Color.FromArgb(200, 50, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Enabled = false };

            tab.Controls.AddRange(new System.Windows.Forms.Control[] { grp1, grp2, btnEliminar });
            return (txtId, txtInfo, btnBuscar, btnEliminar);
        }
    }
}
