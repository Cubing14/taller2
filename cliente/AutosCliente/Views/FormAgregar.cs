using System;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Controllers;
using AutosCliente.Services;

namespace AutosCliente.Views
{
    public class FormAgregar : Form
    {
        private readonly VehiculoController _controller;
        private TextBox txtMarca, txtModelo, txtAnio, txtAutonomia, txtBatTipo, txtBatCapacidad, txtBatCiclos;
        private TextBox txtHMarca, txtHModelo, txtHAnio, txtHAutonomia, txtHConsumo, txtHBateria;

        public FormAgregar(AutoService service)
        {
            _controller = new VehiculoController(service);
            Text = "Agregar Vehículo";
            Size = new Size(440, 560);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var tabs = new TabControl { Left = 10, Top = 10, Width = 400, Height = 490 };
            tabs.TabPages.Add(TabElectrico());
            tabs.TabPages.Add(TabHibrido());
            Controls.Add(tabs);
        }

        private TabPage TabElectrico()
        {
            var tab = new TabPage("⚡ Auto Eléctrico");
            var grpAuto = Grupo("Datos del Auto", 5, 5, 375, 200);
            txtMarca     = Campo(grpAuto, "Marca:",          10, 30);
            txtModelo    = Campo(grpAuto, "Modelo:",         10, 70);
            txtAnio      = Campo(grpAuto, "Año:",            10, 110);
            txtAutonomia = Campo(grpAuto, "Autonomía (km):", 10, 150);

            var grpBat = Grupo("Batería", 5, 215, 375, 165);
            txtBatTipo      = Campo(grpBat, "Tipo (ej: Li-Ion):", 10, 30);
            txtBatCapacidad = Campo(grpBat, "Capacidad (kWh):",   10, 70);
            txtBatCiclos    = Campo(grpBat, "Ciclos de vida:",    10, 110);

            var btn = Boton("💾 Guardar Auto Eléctrico", 5, 390, 375, Color.FromArgb(0, 120, 215));
            btn.Click += async (s, e) =>
            {
                var (ok, msg, r) = await _controller.AgregarElectrico(
                    txtMarca.Text, txtModelo.Text, txtAnio.Text, txtAutonomia.Text,
                    txtBatTipo.Text, txtBatCapacidad.Text, txtBatCiclos.Text);
                MessageBox.Show(ok ? $"✅ {msg}" : $"❌ {msg}",
                    ok ? "Éxito" : "Error", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                if (ok) Close();
            };

            tab.Controls.AddRange(new System.Windows.Forms.Control[] { grpAuto, grpBat, btn });
            return tab;
        }

        private TabPage TabHibrido()
        {
            var tab = new TabPage("🔋 Auto Híbrido");
            var grp = Grupo("Datos del Auto Híbrido", 5, 5, 375, 280);
            txtHMarca     = Campo(grp, "Marca:",           10, 30);
            txtHModelo    = Campo(grp, "Modelo:",          10, 70);
            txtHAnio      = Campo(grp, "Año:",             10, 110);
            txtHAutonomia = Campo(grp, "Autonomía (km):",  10, 150);
            txtHConsumo   = Campo(grp, "Consumo L/100km:", 10, 190);
            txtHBateria   = Campo(grp, "Batería kWh:",     10, 230);

            var btn = Boton("💾 Guardar Auto Híbrido", 5, 300, 375, Color.FromArgb(0, 150, 80));
            btn.Click += async (s, e) =>
            {
                var (ok, msg, r) = await _controller.AgregarHibrido(
                    txtHMarca.Text, txtHModelo.Text, txtHAnio.Text,
                    txtHAutonomia.Text, txtHConsumo.Text, txtHBateria.Text);
                MessageBox.Show(ok ? $"✅ {msg}" : $"❌ {msg}",
                    ok ? "Éxito" : "Error", MessageBoxButtons.OK,
                    ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                if (ok) Close();
            };

            tab.Controls.AddRange(new System.Windows.Forms.Control[] { grp, btn });
            return tab;
        }

        private static GroupBox Grupo(string texto, int x, int y, int w, int h) =>
            new GroupBox { Text = texto, Left = x, Top = y, Width = w, Height = h, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };

        private static TextBox Campo(System.Windows.Forms.Control c, string label, int x, int y)
        {
            c.Controls.Add(new Label { Text = label, Left = x, Top = y, Width = 150, Font = new Font("Segoe UI", 9f) });
            var txt = new TextBox { Left = x + 155, Top = y - 2, Width = 190 };
            c.Controls.Add(txt);
            return txt;
        }

        private static Button Boton(string texto, int x, int y, int w, Color color) =>
            new Button { Text = texto, Left = x, Top = y, Width = w, Height = 38, Font = new Font("Segoe UI", 10f, FontStyle.Bold), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
    }
}
