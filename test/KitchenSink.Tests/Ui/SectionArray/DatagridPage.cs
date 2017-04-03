using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui.SectionArray
{
    public class DatagridPage : BasePage
    {
        public DatagridPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Add a pet']")]
        public IWebElement AddPetButton { get; set; }

        public bool CheckTableVisible()
        {
            var shadowRoot = ExpandShadowRoot(Driver.FindElement(By.XPath("//hot-table")));
            return shadowRoot.FindElement(By.ClassName("htCore")).Displayed;
        }

        public int GetTableRowsCount()
        {
            var shadowRoot = ExpandShadowRoot(Driver.FindElement(By.XPath("//hot-table")));
            return shadowRoot.FindElement(By.ClassName("htCore")).FindElements(By.XPath("tbody//tr")).Count;
        }

        public void AddPet()
        {
            ClickOn(AddPetButton);
        }
    }
}
