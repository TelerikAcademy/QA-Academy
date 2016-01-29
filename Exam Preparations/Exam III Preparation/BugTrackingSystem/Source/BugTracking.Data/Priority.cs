using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Data
{
    public enum Priority
    {
        [DescriptionAttribute("Нисък")]
        Low,

        [DescriptionAttribute("Нормален")]
        Normal,

        [DescriptionAttribute("Висок")]
        High,

        [DescriptionAttribute("Критичен")]
        Critical
    }
}
