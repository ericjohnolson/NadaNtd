using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.UI.AppLogic
{
    public interface IView
    {
        Action OnClose { get; set; }
        Action<string> StatusChanged { get; set; }     
    }
}
