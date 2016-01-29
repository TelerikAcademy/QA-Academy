using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using constants;

namespace businesslogic
{
    /// <summary>
    /// Summary description for RepairCardFilter
    /// </summary>
    public class RepairCardFilter
    {
        private int filterType;
        private string vinChassis;
        private DateTime startRepair;
        private DateTime fromFinishRepair;
        private DateTime toFinishRepair;

        public int Type
        {
            get { return filterType; }
            set { filterType = value; }
        }

        public string VinChassis
        {
            get { return vinChassis; }
            set { vinChassis = value; }
        }

        public DateTime StartRepair
        {
            get { return startRepair; }
            set { startRepair = value; }
        }

        public DateTime FromFinishRepair
        {
            get { return fromFinishRepair; }
            set { fromFinishRepair = value; }
        }

        public DateTime ToFinishRepair
        {
            get { return toFinishRepair; }
            set { toFinishRepair = value; }
        }

        public RepairCardFilter(int filterType)
        {
            this.filterType = filterType;
        }
    }
}
