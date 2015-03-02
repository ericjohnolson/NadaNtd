using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nada.Deployment
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread.CurrentThread.CurrentCulture.ClearCachedData();
            var thread = new Thread(
                s => ((CultureState)s).Result = Thread.CurrentThread.CurrentCulture);
            var state = new CultureState();
            thread.Start(state);
            thread.Join();
            CultureInfo culture = state.Result;

            Localizer.SetCulture(culture);
            Localizer.Initialize();

            Application.Run(new Shell());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Logger logger = new Logger();
                Exception ex = (Exception)e.ExceptionObject;
                logger.Error("Unhandled exception occured in application. " + ex.Message, ex);
                Console.WriteLine("MyHandler caught : " + ex.Message);
                Console.WriteLine("Runtime terminating: {0}", e.IsTerminating);
                ErrorModal form = new ErrorModal(ex.Message);
                form.ShowDialog();
            }
            catch (Exception)
            {
            }
        }
    }
}
