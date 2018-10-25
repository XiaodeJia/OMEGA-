using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOMEGA
{
    public partial class  TimeSheetIdentityEvent:System.EventArgs
    {
        int tSheetId;
        private string wHs;
        private string ovHs;
        public TimeSheetIdentityEvent(int tsId, string wt, string ot)
        {
            this.tSheetId = tsId;
            this.wHs = wt;
            this.ovHs = ot;
        }
        public int TimeSheetId
        {
            get
            {
                return tSheetId;
            }
        }
        public string WorkTime
        {
            get
            {
                return wHs;
            }
        }
        public string OverTime
        {
           get
           {
               return ovHs;
           }
        }
    }
}
