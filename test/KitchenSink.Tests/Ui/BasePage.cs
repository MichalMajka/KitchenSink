using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace KitchenSink.Tests.Ui
{
    public class BasePage
    {
        public IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'MainPage']")]
        public IWebElement MainPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Datepicker']")]
        public IWebElement DatepickerPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Dropdown']")]
        public IWebElement DropdownPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Nested partials']")]
        public IWebElement NestedPartialsPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Table']")]
        public IWebElement TablePageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Validation']")]
        public IWebElement ValidationPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Button']")]
        public IWebElement ButtonPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Checkbox']")]
        public IWebElement CheckboxPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Text']")]
        public IWebElement TextPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Textarea']")]
        public IWebElement TextareaPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Redirect']")]
        public IWebElement RedirectPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Password']")]
        public IWebElement PasswordPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Pagination']")]
        public IWebElement PaginationPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Autocomplete']")]
        public IWebElement AutoCompletePageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'File upload']")]
        public IWebElement FileUploadPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Togglebutton']")]
        public IWebElement ToggleButtonPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Datagrid']")]
        public IWebElement DataGridPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Breadcrumb']")]
        public IWebElement BreadcrumbPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Multiselect']")]
        public IWebElement MultiselectPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Radio']")]
        public IWebElement RadioPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Radiolist']")]
        public IWebElement RadiolistPageLink { get; set; }

        public IWebElement WaitForElementToBeClickable(IWebElement elementName, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(ExpectedConditions.ElementToBeClickable(elementName));
        }

        public void ClickOn(IWebElement elementName, int seconds = 10)
        {
            IWebElement element = WaitForElementToBeClickable(elementName, seconds);
            element.Click();
        }

        public IWebElement ExpandShadowRoot(IWebElement shadowRootElement)
        {
            IWebElement shadowTreeParent = (IWebElement)((IJavaScriptExecutor)Driver)
           .ExecuteScript("return arguments[0].shadowRoot", shadowRootElement);
            if (shadowTreeParent == null)
            {
                //Shadow DOM not supported, fall back to DOM
                return shadowRootElement;
            }
            return shadowTreeParent;
        }

        public void ScrollToTheTop()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, 0)");
        }
    }
}
