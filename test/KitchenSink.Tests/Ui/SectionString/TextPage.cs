using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui.SectionString
{
    public class TextPage : BasePage
    {
        public TextPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-name-input")]
        public IWebElement Input { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-name-input-dynamic")]
        public IWebElement InputDynamic { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-name-label")]
        public IWebElement InputInfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-name-label-dynamic")]
        public IWebElement InputInfoLabelDynamic { get; set; }

        public void FillInput(string input)
        {
            Input.Clear();
            Input.SendKeys(input);
            Input.SendKeys(Keys.Enter);
        }

        public void FillInputDynamic(string input)
        {
            InputDynamic.Clear();
            InputDynamic.SendKeys(input);
            InputDynamic.SendKeys(Keys.Enter);
        }

        public void ClearInput()
        {
            Input.Clear();
            Input.SendKeys(Keys.Enter);
        }

        public void ClearInputDynamic()
        {
            var inputLength = InputDynamic.GetAttribute("value").Length;

            //can't use Clear()
            for (var i = 0; i < inputLength; i++)
            {
                InputDynamic.SendKeys(Keys.Backspace);
            }

            InputDynamic.SendKeys(Keys.Enter);
        }
    }
}