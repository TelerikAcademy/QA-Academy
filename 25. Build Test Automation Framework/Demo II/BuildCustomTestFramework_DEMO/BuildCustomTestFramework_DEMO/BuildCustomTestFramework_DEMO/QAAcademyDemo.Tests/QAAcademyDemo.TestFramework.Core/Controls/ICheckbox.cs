namespace QAAcademyDemo.TestFramework.Core.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public interface ICheckbox
    {
        bool IsChecked { get; set; }

        void Check(bool shouldCheck);
    }
}
