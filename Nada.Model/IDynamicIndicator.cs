using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nada.Model
{
    public interface IDynamicIndicator
    {
        int Id { get; set; }
        int DataTypeId { get; set; }
        string DisplayName { get; set; }
        int SortOrder { get; set; }
    }
}
