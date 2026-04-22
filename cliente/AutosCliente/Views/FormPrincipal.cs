using System;
using System.Drawing;
using System.Windows.Forms;
using AutosCliente.Services;

namespace AutosCliente.Views
{
    public class FormPrincipal : Form
    {
        // Instancia única compartida — necesaria para que el Observer funcione entre ventanas
        private readonly AutoService _service = new();

        public FormPrincipal()
        {
            Text = "Gestión de Vehículos Eléctricos e Híbridos";
            Size = new Size(540, 420);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(245, 245, 245);

            var menu = new MenuStrip { BackColor = Color.FromArgb(30, 30, 30) };

            var mnuVehiculos = Item("⚡ Vehículos");
            mnuVehiculos.DropDownItems.Add("➕ Agregar",    null, (s, e) => new FormAgregar(_service).ShowDialog());
            mnuVehiculos.DropDownItems.Add("🔍 Buscar",     null, (s, e) => new FormBuscar(_service).ShowDialog());
            mnuVehiculos.DropDownItems.Add("✏️ Actualizar", null, (s, e) => new FormActualizar(_service).ShowDialog());
            mnuVehiculos.DropDownItems.Add("🗑️ Eliminar",   null, (s, e) => new FormEliminar(_service).ShowDialog());
            mnuVehiculos.DropDownItems.Add("📋 Listar",     null, (s, e) => new FormListar(_service).ShowDialog());

            var mnuBaterias = Item("🔋 Baterías");
            mnuBaterias.DropDownItems.Add("⚙️ Gestionar Baterías", null, (s, e) => new FormGestionBateria(_service).ShowDialog());

            var mnuCalcular = Item("📊 Calcular");
            mnuCalcular.DropDownItems.Add("⚙️ Costo de Operación", null, (s, e) => new FormCalcular(_service).ShowDialog());

            var mnuAyuda = Item("❓ Ayuda");
            mnuAyuda.DropDownItems.Add("Acerca de...", null, (s, e) =>
                MessageBox.Show(
                    "Gestión de Vehículos Eléctricos e Híbridos\nVersión 1.0\n\nIntegrantes:\n• Yaser Rondón\n• Ismael Cardozo\n• Juan Mancipe",
                    "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information));

            menu.Items.AddRange(new ToolStripItem[] { mnuVehiculos, mnuBaterias, mnuCalcular, mnuAyuda });

            Controls.Add(new Label { Text = "⚡ Gestión de Vehículos", Font = new Font("Segoe UI", 16f, FontStyle.Bold), ForeColor = Color.FromArgb(0, 100, 180), Left = 30, Top = 55, Width = 470, Height = 40 });

            string[] info = {
                "Model → Models/: AutoElectrico (B), AutoHibrido (C), Bateria (D), Vehiculo (A)",
                "View  → Views/: FormAgregar, FormBuscar, FormActualizar, FormEliminar, FormListar...",
                "Controller → Controllers/: VehiculoController, BateriaController",
                "Service → Services/: AutoService (HTTP), IAutoObserver (Observer)",
                "Singleton: SistemaInfo  |  Observer: FormListar se actualiza automáticamente"
            };
            int y = 110;
            foreach (var s in info)
            {
                Controls.Add(new Label { Text = s, Font = new Font("Segoe UI", 8.5f), Left = 30, Top = y, Width = 470, Height = 20, ForeColor = Color.FromArgb(60, 60, 60) });
                y += 22;
            }

            Controls.Add(new Label { Text = "Servidor: http://localhost:8080", Font = new Font("Segoe UI", 8f, FontStyle.Italic), ForeColor = Color.Gray, Left = 30, Top = 370, Width = 300 });
            Controls.Add(menu);
            MainMenuStrip = menu;
        }

        private static ToolStripMenuItem Item(string texto) =>
            new ToolStripMenuItem(texto) { ForeColor = Color.White };
    }
}
