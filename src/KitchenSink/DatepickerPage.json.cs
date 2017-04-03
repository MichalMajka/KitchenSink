using System;
using Starcounter;

namespace KitchenSink
{
    partial class DatepickerPage : Json
    {
        protected override void OnData()
        {
            base.OnData();
            this.Value = DateTime.Now.ToString("yyyy-MM-dd");
            this.ParseDate(this.Value);
        }

        void Handle(Input.Value Action)
        {
            this.ParseDate(Action.Value);
        }

        protected void ParseDate(string Value)
        {
            DateTime? date = GetDate(Value);

            if (date == null)
            {
                this.Month = string.Empty;
                this.Day = string.Empty;
                this.Year = string.Empty;
            }
            else
            {
                this.Month = date.Value.ToString("MMMM");
                this.Day = date.Value.Day.ToString();
                this.Year = date.Value.Year.ToString();
            }
        }

        protected DateTime? GetDate(string Value)
        {
            if (string.IsNullOrEmpty(Value))
            {
                return null;
            }

            DateTime date;
            DateTime.TryParse(Value, out date);

            return date;
        }
    }
}