using Starcounter;

namespace KitchenSink
{
    partial class ChartPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            this.AddChartData("January", 4);
            this.AddChartData("February", 7);
            this.AddChartData("March", 9);
            this.AddChartData("April", 12);
            this.AddChartData("May", 15);
            this.AddChartData("June", 19);
        }

        public void AddChartData(string label, int value)
        {
            Json labelItem = Labels.Add();
            labelItem.StringValue = label;

            Json temperatureItem = Temperatures.Add();
            temperatureItem.IntegerValue = value;
        }
    }
}