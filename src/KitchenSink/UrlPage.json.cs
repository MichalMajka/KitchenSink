using Starcounter;

namespace KitchenSink
{
    partial class UrlPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            this.Url = "/KitchenSink";
            this.Label = "This a sample link";
        }
    }
}