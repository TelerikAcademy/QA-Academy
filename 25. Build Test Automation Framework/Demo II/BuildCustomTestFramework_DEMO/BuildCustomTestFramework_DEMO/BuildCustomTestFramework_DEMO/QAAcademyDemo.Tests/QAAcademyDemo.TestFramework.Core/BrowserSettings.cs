using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestFramework.Selenium.Core
{
    public class BrowserSettings
    {
        public BrowserSettings(Browsers browserType, TimeSpan executionTimeout)
        {
            this.BrowserType = browserType;
            this.ExecutionTimeout = executionTimeout;
        }

        public Browsers BrowserType { get; set; }

        public TimeSpan ExecutionTimeout { get; set; }

        public string ExecutionDir { get; set; }
    }
}