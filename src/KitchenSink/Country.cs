using System;

namespace KitchenSink
{
    public class Country
    {
        public Country(string name, string capitalName)
        {
            Name = name;
            CapitalName = capitalName;
        }

        public String Name { get; set; }
        public String CapitalName { get; set; }
    }
}