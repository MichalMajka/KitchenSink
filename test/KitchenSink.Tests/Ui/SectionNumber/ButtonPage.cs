using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui.SectionNumber
{
    public class ButtonPage : BasePage
    {     
        public ButtonPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-carrots-reaction-label")]
        public IWebElement VegetablesButtonInfoLabel { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Button (inline script)']")]
        public IWebElement ButtonInlineScript { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Button (function)']")]
        public IWebElement ButtonFunction { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[text() = 'Span (function)']")]
        public IWebElement SpanFunction { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Enable carrot engine']")]
        public IWebElement ButtonEnableCarrotEngine { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Add carrots']")]
        public IWebElement ButtonAddCarrots { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-carrot-engine-reaction-label")]
        public IWebElement EnableCarrotEngineLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-one-carrot-reaction-label")]
        public IWebElement AddCarrotsLabel { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Take one Regenerating Carrot']")]
        public IWebElement ButonTakeOneRegeneratingCarrot { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-self-button-label")]
        public IWebElement TakeOneRegeneratingCarrotLabel { get; set; }

        public void ClickButtonInlineScript()
        {
            ClickOn(ButtonInlineScript);
        }

        public void ClickButtonFunction()
        {
            ClickOn(ButtonFunction);
        }

        public void ClickSpanFunction()
        {
            ClickOn(SpanFunction);
        }

        public void ClickEnableCarrotEngine()
        {
            ClickOn(ButtonEnableCarrotEngine);
        }

        public void ClickButtonAddCarrots()
        {
            ClickOn(ButtonAddCarrots);
        }

        public void ClickButonTakeOneRegeneratingCarrot()
        {
            ClickOn(ButonTakeOneRegeneratingCarrot);
        }
    }
}
