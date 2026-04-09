using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutosCliente.Forms
{
    public class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            Text = "Gestión de Autos Eléctricos";
            Size = new Size(500, 420);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(245, 245, 245);

            // ── Menú ─────────────────────────────────────────────────────
            var menu = new MenuStrip { BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White };

            var mnuAutos = new ToolStripMenuItem("⚡ Autos") { ForeColor = Color.White };
            mnuAutos.DropDownItems.Add("➕  Agregar auto",    null, (s, e) => new FormAgregar().ShowDialog());
            mnuAutos.DropDownItems.Add("🔍  Buscar auto",     null, (s, e) => new FormBuscar().ShowDialog());
            mnuAutos.DropDownItems.Add("✏️   Actualizar auto", null, (s, e) => new FormActualizar().ShowDialog());
            mnuAutos.DropDownItems.Add("🗑️   Eliminar auto",   null, (s, e) => new FormEliminar().ShowDialog());
            mnuAutos.DropDownItems.Add("📋  Listar autos",    null, (s, e) => new FormListar().ShowDialog());

            var mnuAyuda = new ToolStripMenuItem("❓ Ayuda") { ForeColor = Color.White };
            mnuAyuda.DropDownItems.Add("Acerca de...", null, (s, e) =>
                MessageBox.Show(
                    "⚡ Gestión de Autos Eléctricos\nVersión 1.0\n\nIntegrantes:\n• Yaser Rondón\n• Ismael Cardozo\n• Juan Mancipe",
                    "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information));

            menu.Items.Add(mnuAutos);
            menu.Items.Add(mnuAyuda);

            // ── Panel de bienvenida ───────────────────────────────────────
            var lblTitulo = new Label
            {
                Text = "⚡ Gestión de Autos Eléctricos",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 100, 180),
                Left = 30, Top = 60, Width = 430, Height = 40
            };

            var lblInstruccion = new Label
            {
                Text = "Use el menú superior para navegar entre las funciones:",
                Font = new Font("Segoe UI", 10f),
                Left = 30, Top = 110, Width = 430, Height = 25
            };

            string[] opciones = {
                "➕  Agregar auto       — Registrar un nuevo auto eléctrico",
                "🔍  Buscar auto        — Consultar un auto por su ID",
                "✏️   Actualizar auto   — Modificar datos de un auto existente",
                "🗑️   Eliminar auto     — Borrar un auto del sistema",
                "📋  Listar autos       — Ver todos los autos (con filtros)"
            };

            int yPos = 145;
            foreach (var op in opciones)
            {
                Controls.Add(new Label
                {
                    Text = op,
                    Font = new Font("Segoe UI", 9.5f),
                    Left = 40, Top = yPos, Width = 420, Height = 22,
                    ForeColor = Color.FromArgb(50, 50, 50)
                });
                yPos += 26;
            }

            Controls.Add(new Label
            {
                Text = "Servidor: http://localhost:8080",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic),
                ForeColor = Color.Gray,
                Left = 30, Top = 360, Width = 300
            });

            Controls.AddRange(new Control[] { menu, lblTitulo, lblInstruccion });
            MainMenuStrip = menu;
        }
    }
}
