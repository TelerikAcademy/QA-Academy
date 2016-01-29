using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class EnumValues
    {
        private string _Name;
        private string _Value;

        public EnumValues(string name, string value)
        {
            this._Name = name;
            this._Value = value;
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
    }
}
