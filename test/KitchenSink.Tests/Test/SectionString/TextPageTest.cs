using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Ui.SectionString;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;

namespace KitchenSink.Tests.Test.SectionString
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    internal class TextPageTest : BaseTest
    {
        private TextPage _textPage;
        private MainPage _mainPage;

        public TextPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _textPage = _mainPage.GoToTextPage();
        }

        [Test]
        public void TextPage_TextPropagationOnUnfocus()
        {
            WaitUntil(x => _textPage.Input.Displayed);

            _textPage.FillInput("Krystian");
            Assert.IsTrue(WaitForText(_textPage.InputInfoLabel, "Hi, Krystian!", 5));
            _textPage.ClearInput();
            WaitUntil(x => _textPage.Input.Text == string.Empty);
            Assert.AreEqual("What's your name?", _textPage.InputInfoLabel.Text);
        }

        [Test]
        public void TextPage_TextPropagationWhileTyping()
        {
            WaitUntil(x => _textPage.InputDynamic.Displayed);

            _textPage.FillInputDynamic("K");
            Assert.IsTrue(WaitForText(_textPage.InputInfoLabelDynamic, "Hi, K!", 5));
            _textPage.ClearInputDynamic();
            WaitUntil(x => _textPage.InputDynamic.Text == string.Empty);
            Assert.AreEqual("What's your name?", _textPage.InputInfoLabelDynamic.Text);
        }
    }
}