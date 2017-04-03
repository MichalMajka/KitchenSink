using Starcounter;
using System;

namespace KitchenSink
{
    //normally this is be a [Database] class
    public class AnyData
    {
        public string Name = "Marcin";
    }

    partial class NestedPartial : Json, IBound<AnyData>
    {
        void Handle(Input.AddChildTrigger action)
        {
            this.ChildPartial = new NestedPartial()
            {
                Data = new AnyData(),
                Html = NestedPartial.DefaultTemplate.Html.DefaultValue + "?" + DateTime.Now
                //normally you don't need this
            };
        }
    }
}