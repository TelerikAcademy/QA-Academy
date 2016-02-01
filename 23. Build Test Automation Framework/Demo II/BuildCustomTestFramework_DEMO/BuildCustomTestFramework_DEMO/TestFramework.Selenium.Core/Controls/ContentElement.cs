namespace TestFramework.Selenium.Core.Controls
{
    using OpenQA.Selenium;

    using QAAcademyDemo.TestFramework.Core.Controls;

    public class ContentElement : Element, IContentElement
    {
        public ContentElement(IWebElement element) : base(element)
        {
        }

        public string Content
        {
            get { return this.WebElement.Text; }
        }
    }
}