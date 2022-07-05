using System.ComponentModel;

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