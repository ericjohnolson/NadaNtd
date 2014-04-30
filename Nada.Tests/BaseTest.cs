using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Nada.Model;
using Nada.UI.AppLogic;

namespace Nada.Tests
{
    public class BaseTest
    {
        public BaseTest()
        {
            var thread = new Thread(
                s => ((CultureState)s).Result = Thread.CurrentThread.CurrentCulture);
            var state = new CultureState();
            thread.Start(state);
            thread.Join();
            CultureInfo culture = state.Result;

            Localizer.SetCulture(culture);
            Localizer.Initialize();
            DatabaseData.Instance.AccessConnectionString = ConfigurationManager.ConnectionStrings["AccessFileName"].ConnectionString;
        }
    }
}
