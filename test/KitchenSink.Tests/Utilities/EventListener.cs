using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace KitchenSink.Tests.Utilities
{
    class KitchenSinkTestEventListener : EventFiringWebDriver
    {
        public KitchenSinkTestEventListener(IWebDriver driver) : base(driver)
        {
            ExceptionThrown += (sender, e) =>
            {
                if (e != null && sender != null)
                {
                    //Screenshot.MakeScreenshot(e.Driver);
                }
            };
        }
    }
}