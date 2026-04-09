using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Models;
using AutosCliente.Services;

namespace AutosCliente.Forms
{
    public class FormListar : Form
    {
        private readonly AutoService _service = new();
        private DataGridView grid;
        private TextBox txtFiltroMarca, txtFiltroAnio;

        public FormListar()
        {
            Text = "Listar Autos Eléctricos";
            Size = new Size(860, 600);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var grpFiltros = new GroupBox
            {
                Text = "Filtros de búsqueda",
                Left = 10, Top = 10, Width = 820, Height = 135,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };

            // Fila 1 — Mostrar todos
            var btnTodos = new Button
            {
                Text = "📋 Mostrar Todos",
                Left = 10, Top = 20, Width = 150, Height = 30,
                BackColor = Color.FromArgb(80, 80, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
            };

            // Fila 2 — Filtro por marca
            var lblMarca = new Label
            {
                Text = "Filtrar por Marca:",
                Left = 10, Top = 65, Width = 130,
                Font = new Font("Segoe UI", 9f, FontStyle.Regular)
            };
            txtFiltroMarca = new TextBox { Left = 145, Top = 63, Width = 200 };
            var btnFiltroMarca = new Button
            {
                Text = "🔍 Buscar por Marca",
                Left = 355, Top = 61, Width = 160, Height = 28,
                BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
            };

            // Fila 3 — Filtro por año
            var lblAnio = new Label
            {
                Text = "Filtrar por Año:",
                Left = 10, Top = 103, Width = 130,
                Font = new Font("Segoe UI", 9f, FontStyle.Regular)
            };
            txtFiltroAnio = new TextBox { Left = 145, Top = 101, Width = 100 };
            var btnFiltroAnio = new Button
            {
                Text = "🔍 Buscar por Año",
                Left = 355, Top = 99, Width = 160, Height = 28,
                BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
            };

            grpFiltros.Controls.AddRange(new Control[]
            {
                btnTodos,
                lblMarca, txtFiltroMarca, btnFiltroMarca,
                lblAnio, txtFiltroAnio, btnFiltroAnio
            });

            grid = new DataGridView
            {
                Left = 10, Top = 155, Width = 820, Height = 395,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9f)
            };
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);

            btnTodos.Click += async (s, e) => CargarGrid(await _service.ListarTodos());

            btnFiltroMarca.Click += async (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtFiltroMarca.Text))
                { MessageBox.Show("Ingrese una marca para filtrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                CargarGrid(await _service.FiltrarPorMarca(txtFiltroMarca.Text.Trim()));
            };

            btnFiltroAnio.Click += async (s, e) =>
            {
                if (!int.TryParse(txtFiltroAnio.Text, out int anio))
                { MessageBox.Show("Ingrese un año válido (ej: 2023).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                CargarGrid(await _service.FiltrarPorAnio(anio));
            };

            Controls.AddRange(new Control[] { grpFiltros, grid });
        }

        private void CargarGrid(List<AutoElectrico>? autos)
        {
            grid.DataSource = null;
            if (autos == null || autos.Count == 0)
            { MessageBox.Show("No se encontraron autos con ese criterio.", "Sin resultados"); return; }

            var tabla = new System.Data.DataTable();
            tabla.Columns.Add("ID");
            tabla.Columns.Add("Marca");
            tabla.Columns.Add("Modelo");
            tabla.Columns.Add("Año");
            tabla.Columns.Add("Autonomía (km)");
            tabla.Columns.Add("Fecha Registro");
            tabla.Columns.Add("Batería Tipo");
            tabla.Columns.Add("Capacidad kWh");
            tabla.Columns.Add("Ciclos Vida");

            foreach (var a in autos)
                tabla.Rows.Add(
                    a.Id, a.Marca, a.Modelo, a.Anio, a.AutonomiaKm,
                    a.FechaRegistro.ToString("yyyy-MM-dd HH:mm"),
                    a.Bateria?.Tipo, a.Bateria?.CapacidadKwh, a.Bateria?.CiclosVida);

            grid.DataSource = tabla;
        }
    }
}
