using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOMEGA
{
    static class Program
    {
        //static string eMan = "Manager";
        //static string eEm = "Staff";
        //static string mHr = "Hr";
        //"Electric");       
        //"Luxury");         
        //"HighPerformance");
        //"Wedding");        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new logIn());
        }
    }
}
