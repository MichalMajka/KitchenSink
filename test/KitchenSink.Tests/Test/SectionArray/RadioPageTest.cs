using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Ui.SectionArray;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;

namespace KitchenSink.Tests.Test.SectionArray
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class RadioPageTest : BaseTest
    {
        private RadioPage _radioPage;
        private MainPage _mainPage;

        public RadioPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _radioPage = _mainPage.GoToRadioPage();
        }

        [Test]
        public void ButtonPage_RegularButton()
        {
            WaitUntil(x => _radioPage.InfoLabel.Displayed);
            Assert.IsTrue(WaitForText(_radioPage.InfoLabel, "You like dogs", 5));

            _radioPage.SelectRadio("cats");
            Assert.IsTrue(WaitForText(_radioPage.InfoLabel, "You like cats", 5));

            _radioPage.SelectRadio("rabbit");
            Assert.IsTrue(WaitForText(_radioPage.InfoLabel, "You like rabbit", 5));

            _radioPage.SelectRadio("dogs");
            Assert.IsTrue(WaitForText(_radioPage.InfoLabel, "You like dogs", 5));
        }
    }
}
