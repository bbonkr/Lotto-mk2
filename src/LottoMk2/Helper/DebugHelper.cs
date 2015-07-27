using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Log;

namespace LottoMk2.Helper
{
    public class DebugHelper
    {
        public static void WriteLine(string message, bool start, bool end)
        {
            if (start) { Debug.WriteLine(String.Format("------<{0}>---------------------", "Debug Message Start")); }
            Debug.WriteLine(String.Format("{0}", message));
            if (end) { Debug.WriteLine(String.Format("------<{0}>---------------------", "Debug Message End")); }
        }

        public static void WriteLine(string message)
        {
            WriteLine(message, true, true);
        }

        public static void WriteLine(Exception ex)
        {
            WriteLine(String.Format("Message: {0}", ex.Message), true, false);
            WriteLine(String.Format("Stack Trace: {0}", ex.StackTrace), false, true);
        }
    }
}