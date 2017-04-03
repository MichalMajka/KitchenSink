using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Ui.SectionArray;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace KitchenSink.Tests.Test.SectionArray
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class DropdownPageTest : BaseTest
    {
        private DropdownPage _dropDownPage;
        private MainPage _mainPage;

        public DropdownPageTest(Config.Browser browser) : base(browser)
        {
        }


        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _dropDownPage = _mainPage.GoToDropdownPage();
        }

        [Test]
        public void DropdownPage_PetsDropdown_SelectPets()
        {
            WaitUntil(x => _dropDownPage.PetsSelect.Displayed);
            _dropDownPage.SelectPet("dogs");
            Assert.IsTrue(WaitForText(_dropDownPage.PetLikeLabel, "You like dogs", 5));
            _dropDownPage.SelectPet("cats");
            Assert.IsTrue(WaitForText(_dropDownPage.PetLikeLabel, "You like cats", 5));

            _dropDownPage.SelectPet("rabbit");
            Assert.IsTrue(WaitForText(_dropDownPage.PetLikeLabel, "You like rabbit", 5));
        }

        [Test]
        public void DropdownPage_JuicyDropdown_SelectProduct()
        {
            WaitUntil(x => _dropDownPage.JuicySelect.Displayed);
            Assert.AreEqual(string.Empty, new SelectElement(_dropDownPage.ProductSelect).SelectedOption.Text);

            _dropDownPage.SelectJuicySelect("Polymer JavaScript library");
            Assert.IsTrue(WaitForText(_dropDownPage.JuicySelectLabel, "You have selected: Polymer JavaScript library", 5));
            Assert.IsTrue(WaitForText(new SelectElement(_dropDownPage.ProductSelect).SelectedOption, "Polymer JavaScript library", 5));

            _dropDownPage.SelectJuicySelect("Starcounter Database");
            Assert.IsTrue(WaitForText(_dropDownPage.JuicySelectLabel, "You have selected: Starcounter Database", 5));
            Assert.IsTrue(WaitForText(new SelectElement(_dropDownPage.ProductSelect).SelectedOption, "Starcounter Database", 5));
        }

        [Test]
        public void DropdownPage_Dropdown_SelectProduct()
        {
            WaitUntil(x => _dropDownPage.ProductSelect.Displayed);
            _dropDownPage.SelectProduct("Starcounter Database");
            Assert.IsTrue(WaitForText(_dropDownPage.JuicySelectLabel, "You have selected: Starcounter Database", 5));
            Assert.IsTrue(WaitForText(new SelectElement(_dropDownPage.JuicySelect).SelectedOption, "Starcounter Database", 5));

            _dropDownPage.SelectProduct("Polymer JavaScript library");
            Assert.IsTrue(WaitForText(_dropDownPage.JuicySelectLabel, "You have selected: Polymer JavaScript library", 5));
            Assert.IsTrue(WaitForText(new SelectElement(_dropDownPage.JuicySelect).SelectedOption, "Polymer JavaScript library", 5));
        }
    }
}

