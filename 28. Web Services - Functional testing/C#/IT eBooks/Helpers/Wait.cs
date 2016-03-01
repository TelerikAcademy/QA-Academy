using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;

namespace ITeBooks
{
    public static class Wait
    {
        /// <summary>
        /// Wait until condition is satisfied
        /// </summary>
        /// <param name="condition">Bool function</param>
        /// <param name="timeout">Timeout in seconds</param>
        /// <param name="debugFail">If set to false Wait do not fail in Debug mode</param>
        /// <param name="waitFail">If false wait do not fail</param>
        /// <param name="errorString">String with error message on failure</param>
        /// <param name="checkInterval">Interval between bool function execution in milliseconds</param>
        public static void Until(Func<bool> condition, int timeout = 10, bool debugFail = false, bool waitFail = false, string errorString = "Timeout exceeded.", int checkInterval=100)
        {
            DateTime start = DateTime.Now;
            while (!condition())
            {
                DateTime now = DateTime.Now;
                if ((now - start).TotalSeconds >= timeout)
                {
                    if (debugFail && Debugger.IsAttached || !Debugger.IsAttached)
                    {
                        if (waitFail)
                        {                            
                            Assert.Fail(errorString);
                        }
                        break;
                    }
                }
                Thread.Sleep(checkInterval);
            }
        }
    }
}