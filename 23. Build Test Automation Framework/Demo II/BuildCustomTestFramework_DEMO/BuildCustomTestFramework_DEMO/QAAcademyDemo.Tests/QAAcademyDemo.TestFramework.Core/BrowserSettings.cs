namespace QAAcademyDemo.TestFramework.Core
{
    using System;

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