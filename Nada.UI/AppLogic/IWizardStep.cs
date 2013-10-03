using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nada.Model.Reports;

namespace Nada.UI.AppLogic
{
    public interface IWizardStep
    {
        Action<ReportOptions> OnRunReport { get; set; }
        Action<IWizardStep> OnSwitchStep { get; set; }
        bool ShowNext { get;  }
        bool EnableNext { get;  }
        bool ShowPrev { get;  }
        bool EnablePrev { get; }
        bool ShowFinish { get;  }
        bool EnableFinish { get;  }
        string StepTitle { get; }

        void DoNext();
        void DoPrev();
        void DoFinish();
    }
}
