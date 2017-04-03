using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui.SectionCustom
{
    public class AutoCompletePage : BasePage
    {
        public AutoCompletePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@title = 'products']//input")]
        public IWebElement ProductsInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@title = 'places']//input")]
        public IWebElement PlaceInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@title = 'products']//ul//li")]
        public IList<IWebElement> ProductsAutoComplete { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@title = 'places']//ul//li")]
        public IList<IWebElement> PlacesAutoComplete { get; set; }

        [FindsBy(How = How.Id, Using = "kitchensink-autocomplete-capital")]
        public IWebElement PlaceInfoLabel { get; set; }

        [FindsBy(How = How.Id, Using = "kitchensink-autocomplete-price")]
        public IWebElement ProductsInfoLabel { get; set; }

        public void ChoosePlace(string place)
        {
           ClickOn(PlacesAutoComplete.First(x => x.Text == place));
        }

        public void ChooseProducts(string product)
        {
            ClickOn(ProductsAutoComplete.First(x => x.Text == product));
        }
    }
}
