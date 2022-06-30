using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Common
{
    public enum OrderEnum
    {
        [Description("Draft")]
        Draft = 0,
        [Description("Confirmed")]
        Confirmed = 1
    }
}
