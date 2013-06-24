using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Nada.UI.AppLogic;

namespace Nada.UI
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture.ClearCachedData();
            var thread = new Thread(
                s => ((CultureState)s).Result = Thread.CurrentThread.CurrentCulture);
            var state = new CultureState();
            thread.Start(state);
            thread.Join();
            CultureInfo culture = state.Result;
            Localizer.SetCulture(culture);
            Localizer.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Shell());
        }
    }
}
