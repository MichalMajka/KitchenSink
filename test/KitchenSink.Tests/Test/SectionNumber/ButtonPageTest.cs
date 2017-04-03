using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Ui.SectionNumber;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;

namespace KitchenSink.Tests.Test.SectionNumber
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class ButtonPageTest : BaseTest
    {
        private ButtonPage _buttonPage;
        private MainPage _mainPage;

        public ButtonPageTest(Config.Browser browser) : base(browser)
        {
        }


        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _buttonPage = _mainPage.GoToButtonPage();
        }

        [Test]
        public void ButtonPage_RegularButton()
        {
            WaitUntil(x => _buttonPage.VegetablesButtonInfoLabel.Displayed);
            Assert.AreEqual("You don't have any carrots", _buttonPage.VegetablesButtonInfoLabel.Text);

            WaitUntil(x => _buttonPage.ButtonInlineScript.Displayed);
            _buttonPage.ClickButtonInlineScript();
            Assert.IsTrue(WaitForText(_buttonPage.VegetablesButtonInfoLabel, "You have 1 imaginary carrots", 5));

            WaitUntil(x => _buttonPage.ButtonFunction.Displayed);
            _buttonPage.ClickButtonFunction();
            Assert.IsTrue(WaitForText(_buttonPage.VegetablesButtonInfoLabel, "You have 2 imaginary carrots", 5));

            WaitUntil(x => _buttonPage.SpanFunction.Displayed);
            _buttonPage.ClickSpanFunction();
            Assert.IsTrue(WaitForText(_buttonPage.VegetablesButtonInfoLabel, "You have 3 imaginary carrots", 5));
        }

        [Test]
        public void ButtonPage_SelfButton()
        {
            _buttonPage.ClickButonTakeOneRegeneratingCarrot();
            Assert.IsTrue(WaitForText(_buttonPage.TakeOneRegeneratingCarrotLabel, "Currently Regenerating!", 5));
        }

        [Test]
        public void ButtonPage_SwitchButton()
        {
            Assert.IsTrue(WaitForText(_buttonPage.EnableCarrotEngineLabel, "Carrot engine is off", 5));

            _buttonPage.ClickEnableCarrotEngine();
            Assert.IsTrue(WaitForText(_buttonPage.EnableCarrotEngineLabel, "Carrot engine is on", 5));
            _buttonPage.ClickEnableCarrotEngine();
            Assert.IsTrue(WaitForText(_buttonPage.EnableCarrotEngineLabel, "Carrot engine is off", 5));
        }

        [Test]
        public void ButtonPage_DisabledButton()
        {
            Assert.IsTrue(WaitForText(_buttonPage.AddCarrotsLabel,
                "You don't have any carrots", 5));
            _buttonPage.ClickButtonAddCarrots();
            Assert.IsTrue(WaitForText(_buttonPage.AddCarrotsLabel,
                "You have 1 imaginary carrots", 5));
        }
    }
}
