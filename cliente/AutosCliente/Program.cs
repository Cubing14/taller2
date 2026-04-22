using System;
using System.Windows.Forms;
using AutosCliente.Views;

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
