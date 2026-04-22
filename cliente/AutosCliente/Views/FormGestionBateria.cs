using System;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Controllers;
using AutosCliente.Services;

namespace AutosCliente.Views
{
    public class FormGestionBateria : Form
    {
        private readonly BateriaController _controller;
        private DataGridView grid = new();
        private TextBox txtId = new(), txtTipo = new(), txtCapacidad = new(), txtCiclos = new();
        private Button btnActualizar = new(), btnEliminar = new();
        private int _idSeleccionado = 0;

        public FormGestionBateria(AutoService service)
        {
            _controller = new BateriaController(service);
            Text = "Gestión de Baterías";
            Size = new Size(700, 560);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var grp = new GroupBox { Text = "Datos de la Batería", Left = 10, Top = 10, Width = 660, Height = 130, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };

            grp.Controls.Add(new Label { Text = "ID (buscar/editar):", Left = 10, Top = 28, Width = 130, Font = new Font("Segoe UI", 9f) });
            txtId = new TextBox { Left = 145, Top = 26, Width = 70 };
            var btnBuscar = new Button { Text = "🔍 Buscar", Left = 225, Top = 24, Width = 85, Height = 26, BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            grp.Controls.Add(new Label { Text = "Tipo:", Left = 10, Top = 65, Width = 45, Font = new Font("Segoe UI", 9f) });
            txtTipo = new TextBox { Left = 60, Top = 63, Width = 130 };
            grp.Controls.Add(new Label { Text = "Capacidad (kWh):", Left = 205, Top = 65, Width = 115, Font = new Font("Segoe UI", 9f) });
            txtCapacidad = new TextBox { Left = 325, Top = 63, Width = 70 };
            grp.Controls.Add(new Label { Text = "Ciclos:", Left = 410, Top = 65, Width = 45, Font = new Font("Segoe UI", 9f) });
            txtCiclos = new TextBox { Left = 460, Top = 63, Width = 70 };

            var btnAgregar = new Button { Text = "➕ Agregar", Left = 10, Top = 95, Width = 95, Height = 26, BackColor = Color.FromArgb(0, 150, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnActualizar = new Button { Text = "✏️ Actualizar", Left = 115, Top = 95, Width = 100, Height = 26, BackColor = Color.FromArgb(200, 130, 0), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Enabled = false };
            btnEliminar = new Button { Text = "🗑️ Eliminar", Left = 225, Top = 95, Width = 95, Height = 26, BackColor = Color.FromArgb(200, 50, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Enabled = false };
            var btnListar = new Button { Text = "📋 Listar Todas", Left = 330, Top = 95, Width = 115, Height = 26, BackColor = Color.FromArgb(80, 80, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            grp.Controls.AddRange(new System.Windows.Forms.Control[] { txtId, btnBuscar, txtTipo, txtCapacidad, txtCiclos, btnAgregar, btnActualizar, btnEliminar, btnListar });

            grid = new DataGridView { Left = 10, Top = 150, Width = 660, Height = 360, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, AllowUserToAddRows = false, RowHeadersVisible = false, BackgroundColor = Color.White, BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9f) };

            btnBuscar.Click += async (s, e) =>
            {
                var (ok, msg, b) = await _controller.Buscar(txtId.Text);
                if (!ok) { MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                _idSeleccionado = b!.Id;
                txtTipo.Text = b.Tipo; txtCapacidad.Text = b.CapacidadKwh.ToString(); txtCiclos.Text = b.CiclosVida.ToString();
                btnActualizar.Enabled = true; btnEliminar.Enabled = true;
            };

            btnAgregar.Click += async (s, e) =>
            {
                var (ok, msg, _) = await _controller.Agregar(txtTipo.Text, txtCapacidad.Text, txtCiclos.Text);
                MessageBox.Show(ok ? $"✅ {msg}" : $"❌ {msg}");
                if (ok) { await Refrescar(); Limpiar(); }
            };

            btnActualizar.Click += async (s, e) =>
            {
                var (ok, msg, _) = await _controller.Actualizar(_idSeleccionado, txtTipo.Text, txtCapacidad.Text, txtCiclos.Text);
                MessageBox.Show(ok ? $"✅ {msg}" : $"❌ {msg}");
                if (ok) { await Refrescar(); Limpiar(); }
            };

            btnEliminar.Click += async (s, e) =>
            {
                if (MessageBox.Show($"¿Eliminar batería ID {_idSeleccionado}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                var (ok, msg) = await _controller.Eliminar(_idSeleccionado);
                MessageBox.Show(ok ? $"✅ {msg}" : $"❌ {msg}");
                if (ok) { await Refrescar(); Limpiar(); }
            };

            btnListar.Click += async (s, e) => await Refrescar();

            Controls.AddRange(new System.Windows.Forms.Control[] { grp, grid });
            _ = Refrescar();
        }

        private async System.Threading.Tasks.Task Refrescar()
        {
            var lista = await _controller.ListarTodas();
            grid.DataSource = null;
            if (lista == null || lista.Count == 0) return;
            var t = new System.Data.DataTable();
            t.Columns.Add("ID"); t.Columns.Add("Tipo"); t.Columns.Add("Capacidad (kWh)"); t.Columns.Add("Ciclos de Vida");
            foreach (var b in lista) t.Rows.Add(b.Id, b.Tipo, b.CapacidadKwh, b.CiclosVida);
            grid.DataSource = t;
        }

        private void Limpiar()
        {
            txtId.Text = ""; txtTipo.Text = ""; txtCapacidad.Text = ""; txtCiclos.Text = "";
            _idSeleccionado = 0; btnActualizar.Enabled = false; btnEliminar.Enabled = false;
        }
    }
}
