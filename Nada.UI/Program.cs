﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Deployment.Application;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Nada.Model;
using Nada.UI.AppLogic;
using Nada.UI.View.Modals;

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

            SetUpDatabase();
            Localizer.SetCulture(culture);
            Localizer.Initialize();

            Application.Run(new Shell());
        }

        private static void SetUpDatabase()
        {
            string localAppData =
                Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);
            string userFilePath = Path.Combine(localAppData, "IotaInk");

            if (!Directory.Exists(userFilePath))
                Directory.CreateDirectory(userFilePath);

            //if it's not already there, 
            //copy the file from the deployment location to the folder
            string sourceFilePath = Path.Combine(
              System.Windows.Forms.Application.StartupPath, "NewNationalDatabaseTemplate.accdb");
            string destFilePath = Path.Combine(userFilePath, "NewNationalDatabaseTemplate.accdb");

            UpdateDatabase dialog = new UpdateDatabase();
            if (!File.Exists(destFilePath) || ApplicationDeployment.IsNetworkDeployed && dialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(sourceFilePath, destFilePath, true);
            }

            // Set runtime connection string
            string connection = ConfigurationManager.ConnectionStrings["AccessFileName"].ConnectionString;
            if(ApplicationDeployment.IsNetworkDeployed)
                ModelData.Instance.AccessConnectionString = connection.Replace("Source=NewNationalDatabaseTemplate.accdb", "Source=" + destFilePath);
            else
                ModelData.Instance.AccessConnectionString = connection;

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
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
