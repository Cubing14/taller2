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
            Size = new Size(860, 560);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // Panel de filtros
            var grpFiltros = new GroupBox
            {
                Text = "Filtros de búsqueda",
                Left = 10, Top = 10, Width = 820, Height = 75,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };

            // Botón todos
            var btnTodos = new Button
            {
                Text = "📋 Mostrar Todos",
                Left = 10, Top = 28, Width = 130, Height = 30,
                BackColor = Color.FromArgb(80, 80, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
            };

            // Filtro marca
            grpFiltros.Controls.Add(new Label { Text = "Filtrar por Marca:", Left = 160, Top = 32, Width = 120, Font = new Font("Segoe UI", 9f) });
            txtFiltroMarca = new TextBox { Left = 285, Top = 30, Width = 130 };
            var btnFiltroMarca = new Button
            {
                Text = "🔍 Buscar",
                Left = 425, Top = 28, Width = 85, Height = 28,
                BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
            };

            // Filtro año
            grpFiltros.Controls.Add(new Label { Text = "Filtrar por Año:", Left = 530, Top = 32, Width = 110, Font = new Font("Segoe UI", 9f) });
            txtFiltroAnio = new TextBox { Left = 645, Top = 30, Width = 80 };
            var btnFiltroAnio = new Button
            {
                Text = "🔍 Buscar",
                Left = 735, Top = 28, Width = 75, Height = 28,
                BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
            };

            grpFiltros.Controls.AddRange(new Control[] { btnTodos, txtFiltroMarca, btnFiltroMarca, txtFiltroAnio, btnFiltroAnio });

            // Grid
            grid = new DataGridView
            {
                Left = 10, Top = 95, Width = 820, Height = 410,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9f)
            };
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);

            btnTodos.Click      += async (s, e) => CargarGrid(await _service.ListarTodos());
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
