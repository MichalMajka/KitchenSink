using Starcounter;

namespace KitchenSink
{
    partial class MultiselectPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            this.Countries.Add().Name = "Sweden";
            this.Countries.Add().Name = "China";
            this.Countries.Add().Name = "India";
        }
    }
}