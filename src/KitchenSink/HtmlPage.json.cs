using Starcounter;

namespace KitchenSink
{
    partial class HtmlPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            this.Bio = @"<h1>This is a markup text</h1>

You can put <strong>any</strong> <a href=""https://en.wikipedia.org/wiki/HTML"">HTML</a> in it.";
        }
    }
}