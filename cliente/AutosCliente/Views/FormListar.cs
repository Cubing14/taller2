using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Controllers;
using AutosCliente.Models;
using AutosCliente.Services;

namespace AutosCliente.Views
{
    // Vista — implementa IAutoObserver para actualizarse automáticamente (patrón Observer)
    public class FormListar : Form, IAutoObserver
    {
        private readonly VehiculoController _controller;
        private readonly AutoService _service;
        private DataGridView gridE = new(), gridH = new();
        private TextBox txtMarcaE = new(), txtAnioE = new(), txtMarcaH = new(), txtAnioH = new();

        public FormListar(AutoService service)
        {
            _service = service;
            _service.Suscribir(this);
            _controller = new VehiculoController(service);

            Text = "Listar Vehículos";
            Size = new Size(900, 650);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var tabs = new TabControl { Left = 10, Top = 10, Width = 860, Height = 590 };
            tabs.TabPages.Add(TabElectrico());
            tabs.TabPages.Add(TabHibrido());
            Controls.Add(tabs);

            FormClosed += (s, e) => _service.Desuscribir(this);
        }

        // Observer: se invoca automáticamente al insertar/actualizar/eliminar desde cualquier ventana
        public void OnAutosActualizados()
        {
            if (InvokeRequired) { Invoke(new Action(OnAutosActualizados)); return; }
            _ = CargarElectricosAsync();
            _ = CargarHibridosAsync();
        }

        private TabPage TabElectrico()
        {
            var tab = new TabPage("⚡ Autos Eléctricos");
            var grp = new GroupBox { Text = "Filtros", Left = 5, Top = 5, Width = 840, Height = 105, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };

            var btnTodos = Boton("📋 Mostrar Todos", 10, 20, Color.FromArgb(80, 80, 80));
            grp.Controls.Add(new Label { Text = "Marca:", Left = 10, Top = 62, Width = 50, Font = new Font("Segoe UI", 9f) });
            txtMarcaE = new TextBox { Left = 65, Top = 60, Width = 150 };
            var btnMarca = Boton("🔍 Por Marca", 225, 58, Color.FromArgb(0, 120, 215));
            grp.Controls.Add(new Label { Text = "Año:", Left = 420, Top = 62, Width = 35, Font = new Font("Segoe UI", 9f) });
            txtAnioE = new TextBox { Left = 460, Top = 60, Width = 80 };
            var btnAnio = Boton("🔍 Por Año", 550, 58, Color.FromArgb(0, 120, 215));
            grp.Controls.AddRange(new System.Windows.Forms.Control[] { btnTodos, txtMarcaE, btnMarca, txtAnioE, btnAnio });

            gridE = CrearGrid(5, 120, 840, 420);

            btnTodos.Click += async (s, e) => CargarGridE(await _controller.ListarElectricos());
            btnMarca.Click += async (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtMarcaE.Text)) return;
                CargarGridE(await _controller.FiltrarElectricosPorMarca(txtMarcaE.Text.Trim()));
            };
            btnAnio.Click += async (s, e) =>
            {
                if (!int.TryParse(txtAnioE.Text, out int anio)) return;
                CargarGridE(await _controller.FiltrarElectricosPorAnio(anio));
            };

            tab.Controls.AddRange(new System.Windows.Forms.Control[] { grp, gridE });
            return tab;
        }

        private TabPage TabHibrido()
        {
            var tab = new TabPage("🔋 Autos Híbridos");
            var grp = new GroupBox { Text = "Filtros", Left = 5, Top = 5, Width = 840, Height = 105, Font = new Font("Segoe UI", 9f, FontStyle.Bold) };

            var btnTodos = Boton("📋 Mostrar Todos", 10, 20, Color.FromArgb(80, 80, 80));
            grp.Controls.Add(new Label { Text = "Marca:", Left = 10, Top = 62, Width = 50, Font = new Font("Segoe UI", 9f) });
            txtMarcaH = new TextBox { Left = 65, Top = 60, Width = 150 };
            var btnMarca = Boton("🔍 Por Marca", 225, 58, Color.FromArgb(0, 120, 215));
            grp.Controls.Add(new Label { Text = "Año:", Left = 420, Top = 62, Width = 35, Font = new Font("Segoe UI", 9f) });
            txtAnioH = new TextBox { Left = 460, Top = 60, Width = 80 };
            var btnAnio = Boton("🔍 Por Año", 550, 58, Color.FromArgb(0, 120, 215));
            grp.Controls.AddRange(new System.Windows.Forms.Control[] { btnTodos, txtMarcaH, btnMarca, txtAnioH, btnAnio });

            gridH = CrearGrid(5, 120, 840, 420);

            btnTodos.Click += async (s, e) => CargarGridH(await _controller.ListarHibridos());
            btnMarca.Click += async (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtMarcaH.Text)) return;
                CargarGridH(await _controller.FiltrarHibridosPorMarca(txtMarcaH.Text.Trim()));
            };
            btnAnio.Click += async (s, e) =>
            {
                if (!int.TryParse(txtAnioH.Text, out int anio)) return;
                CargarGridH(await _controller.FiltrarHibridosPorAnio(anio));
            };

            tab.Controls.AddRange(new System.Windows.Forms.Control[] { grp, gridH });
            return tab;
        }

        private async System.Threading.Tasks.Task CargarElectricosAsync() =>
            CargarGridE(await _controller.ListarElectricos());

        private async System.Threading.Tasks.Task CargarHibridosAsync() =>
            CargarGridH(await _controller.ListarHibridos());

        private void CargarGridE(List<AutoElectrico>? lista)
        {
            gridE.DataSource = null;
            if (lista == null || lista.Count == 0) return;
            var t = new System.Data.DataTable();
            t.Columns.Add("ID"); t.Columns.Add("Marca"); t.Columns.Add("Modelo");
            t.Columns.Add("Año"); t.Columns.Add("Autonomía (km)"); t.Columns.Add("Fecha Registro");
            t.Columns.Add("Batería Tipo"); t.Columns.Add("Capacidad kWh"); t.Columns.Add("Ciclos Vida");
            foreach (var a in lista)
                t.Rows.Add(a.Id, a.Marca, a.Modelo, a.Anio, a.AutonomiaKm,
                    a.FechaRegistro.ToString("yyyy-MM-dd HH:mm"),
                    a.Bateria?.Tipo, a.Bateria?.CapacidadKwh, a.Bateria?.CiclosVida);
            gridE.DataSource = t;
        }

        private void CargarGridH(List<AutoHibrido>? lista)
        {
            gridH.DataSource = null;
            if (lista == null || lista.Count == 0) return;
            var t = new System.Data.DataTable();
            t.Columns.Add("ID"); t.Columns.Add("Marca"); t.Columns.Add("Modelo");
            t.Columns.Add("Año"); t.Columns.Add("Autonomía (km)"); t.Columns.Add("Fecha Registro");
            t.Columns.Add("Consumo L/100km"); t.Columns.Add("Batería kWh");
            foreach (var a in lista)
                t.Rows.Add(a.Id, a.Marca, a.Modelo, a.Anio, a.AutonomiaKm,
                    a.FechaRegistro.ToString("yyyy-MM-dd HH:mm"),
                    a.ConsumoCombustibleL100km, a.CapacidadBateriaKwh);
            gridH.DataSource = t;
        }

        private static DataGridView CrearGrid(int x, int y, int w, int h) =>
            new DataGridView { Left = x, Top = y, Width = w, Height = h, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, AllowUserToAddRows = false, RowHeadersVisible = false, BackgroundColor = Color.White, BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9f) };

        private static Button Boton(string texto, int x, int y, Color color) =>
            new Button { Text = texto, Left = x, Top = y, Width = 150, Height = 28, BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
    }
}
