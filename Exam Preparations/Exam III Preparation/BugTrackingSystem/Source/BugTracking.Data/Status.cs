using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Data
{
     public enum Status 
    { 
        [DescriptionAttribute("Нов")]
        New,

        [DescriptionAttribute("В процес на работа")]
        InProgress,

        [DescriptionAttribute("Поправен")]
        Fixed,

        [DescriptionAttribute("Приключен")]
        Closed,

        [DescriptionAttribute("Изтрит")]
        Deleted,
    }
}
