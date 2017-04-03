using Starcounter;

namespace KitchenSink
{
    partial class MarkdownPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            this.Bio = @"# This is a strucured text

It supports **markdown** *syntax*.";
        }
    }
}