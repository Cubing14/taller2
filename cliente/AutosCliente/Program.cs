using System;
using System.Windows.Forms;
using AutosCliente.Forms;

namespace AutosCliente
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormPrincipal());
        }
    }
}
