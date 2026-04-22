using System;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Controllers;
using AutosCliente.Services;

namespace AutosCliente.Views
{
    public class FormActualizar : Form
    {
        private readonly VehiculoController _controller;

        public FormActualizar(AutoService service)
        {
            _controller = new VehiculoController(service);
            Text = "Actualizar Vehículo";
            Size = new Size(460, 620);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var tabs = new TabControl { Left = 10, Top = 10, Width = 420, Height = 560 };
            tabs.TabPages.Add(TabElectrico());
            tabs.TabPages.Add(TabHibrido());
            Controls.Add(tabs);
        }

        private TabPage TabElectrico()
        {
            var tab = new TabPage("⚡ Auto Eléctrico");
            int idActual = 0;

            var grpBuscar = GrupoBuscar(5, 5);
            var txtId = (TextBox)grpBuscar.Controls["txtId"]!;
            var btnBuscar = (Button)grpBuscar.Controls["btnBuscar"]!;

            var grpAuto = Grupo("Paso 2 — Modificar datos", 5, 90, 395, 200);
            var txtMarca     = Campo(grpAuto, "Marca:",          10, 30);
            var txtModelo    = Campo(grpAuto, "Modelo:",         10, 70);
            var txtAnio      = Campo(grpAuto, "Año:",            10, 110);
            var txtAutonomia = Campo(grpAuto, "Autonomía (km):", 10, 150);

            var grpBat = Grupo("Batería", 5, 300, 395, 165);
            var txtBatTipo      = Campo(grpBat, "Tipo:",             10, 30);
            var txtBatCapacidad = Campo(grpBat, "Capacidad (kWh):",  10, 70);
            var txtBatCiclos    = Campo(grpBat, "Ciclos de vida:",   10, 110);

            var btnGuardar = new Button { Text = "💾 Guardar Cambios", Left = 5, Top = 475, Width = 395, Height = 38, Font = new Font("Segoe UI", 10f, FontStyle.Bold), BackColor = Color.FromArgb(0, 150, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Enabled = false };

            btnBuscar.Click += async (s, e) =>
            {
                var (ok, msg, a) = await _controller.BuscarElectrico(txtId.Text);
                if (!ok) { MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                idActual = a!.Id;
                txtMarca.Text = a.Marca; txtModelo.Text = a.Modelo; txtAnio.Text = a.Anio.ToString();
                txtAutonomia.Text = a.AutonomiaKm.ToString(); txtBatTipo.Text = a.Bateria?.Tipo ?? "";
                txtBatCapacidad.Text = a.Bateria?.CapacidadKwh.ToString() ?? ""; txtBatCiclos.Text = a.Bateria?.CiclosVida.ToString() ?? "";
                btnGuardar.Enabled = true;
            };

            btnGuardar.Click += async (s, e) =>
            {
                var (ok, msg, _) = await _controller.ActualizarElectrico(idActual,
                    txtMarca.Text, txtModelo.Text, txtAnio.Text, txtAutonomia.Text,
                    txtBatTipo.Text, txtBatCapacidad.Text, txtBatCiclos.Text);
                MessageBox.Show(ok ? $"✅ {msg}" : $"❌ {msg}");
                if (ok) Close();
            };

            tab.Controls.AddRange(new System.Windows.Forms.Control[] { grpBuscar, grpAuto, grpBat, btnGuardar });
            return tab;
        }

        private TabPage TabHibrido()
        {
            var tab = new TabPage("🔋 Auto Híbrido");
            int idActual = 0;

            var grpBuscar = GrupoBuscar(5, 5);
            var txtId = (TextBox)grpBuscar.Controls["txtId"]!;
            var btnBuscar = (Button)grpBuscar.Controls["btnBuscar"]!;

            var grp = Grupo("Paso 2 — Modificar datos", 5, 90, 395, 280);
            var txtMarca     = Campo(grp, "Marca:",           10, 30);
            var txtModelo    = Campo(grp, "Modelo:",          10, 70);
            var txtAnio      = Campo(grp, "Año:",             10, 110);
            var txtAutonomia = Campo(grp, "Autonomía (km):",  10, 150);
            var txtConsumo   = Campo(grp, "Consumo L/100km:", 10, 190);
            var txtBateria   = Campo(grp, "Batería kWh:",     10, 230);

            var btnGuardar = new Button { Text = "💾 Guardar Cambios", Left = 5, Top = 385, Width = 395, Height = 38, Font = new Font("Segoe UI", 10f, FontStyle.Bold), BackColor = Color.FromArgb(0, 150, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Enabled = false };

            btnBuscar.Click += async (s, e) =>
            {
                var (ok, msg, a) = await _controller.BuscarHibrido(txtId.Text);
                if (!ok) { MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                idActual = a!.Id;
                txtMarca.Text = a.Marca; txtModelo.Text = a.Modelo; txtAnio.Text = a.Anio.ToString();
                txtAutonomia.Text = a.AutonomiaKm.ToString(); txtConsumo.Text = a.ConsumoCombustibleL100km.ToString();
                txtBateria.Text = a.CapacidadBateriaKwh.ToString();
                btnGuardar.Enabled = true;
            };

            btnGuardar.Click += async (s, e) =>
            {
                var (ok, msg, _) = await _controller.ActualizarHibrido(idActual,
                    txtMarca.Text, txtModelo.Text, txtAnio.Text,
                    txtAutonomia.Text, txtConsumo.Text, txtBateria.Text);
                MessageBox.Show(ok ? $"✅ {msg}" : $"❌ {msg}");
                if (ok) Close();
            };

            tab.Controls.AddRange(new System.Windows.Forms.Control[] { grpBuscar, grp, btnGuardar });
            return tab;
        }

        private static GroupBox GrupoBuscar(int x, int y)
        {
            var grp = new GroupBox { Text = "Paso 1 — Buscar", Left = x, Top = y, Width = 395, Height = 70, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };
            grp.Controls.Add(new Label { Text = "ID:", Left = 10, Top = 28, Width = 30, Font = new Font("Segoe UI", 9f) });
            var txtId = new TextBox { Name = "txtId", Left = 45, Top = 26, Width = 80 };
            var btn = new Button { Name = "btnBuscar", Text = "🔍 Buscar", Left = 135, Top = 24, Width = 90, Height = 28, BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            grp.Controls.AddRange(new System.Windows.Forms.Control[] { txtId, btn });
            return grp;
        }

        private static GroupBox Grupo(string texto, int x, int y, int w, int h) =>
            new GroupBox { Text = texto, Left = x, Top = y, Width = w, Height = h, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };

        private static TextBox Campo(System.Windows.Forms.Control c, string label, int x, int y)
        {
            c.Controls.Add(new Label { Text = label, Left = x, Top = y, Width = 155, Font = new Font("Segoe UI", 9f) });
            var txt = new TextBox { Left = x + 160, Top = y - 2, Width = 200 };
            c.Controls.Add(txt);
            return txt;
        }
    }
}
