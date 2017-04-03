using Starcounter;

namespace KitchenSink
{
    partial class RadiolistPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            MenuOptionsElement a;
            a = this.MenuOptions.Add();
            a.Label = "Dogs";
            a = this.MenuOptions.Add();
            a.Label = "Cats";
            this.SelectOption(0);
        }

        public void SelectOption(int index)
        {
            SelectedItemLabel = MenuOptions[index].Label;
        }

        void Handle(Input.SelectedItemIndex action)
        {
            SelectOption((int) action.Value);
        }
    }

    [RadiolistPage_json.MenuOptions]
    partial class MenuOptionsElement : Json
    {
        void Handle(Input.Choose action)
        {
            Label = "!";
        }
    }
}