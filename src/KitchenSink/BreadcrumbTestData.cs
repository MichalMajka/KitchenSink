using Starcounter;

/*
Test data hierarchy:

Products
    Food
        Dairy
            Milk
                Coffee milk 5 ML
                Milk 1 L
        Meat
    Metal
        Screws
            Phillips flat head
*/

namespace KitchenSink
{
    static class BreadcrumbTestData
    {
        public static bool Exists()
        {
            var exists = Db.SQL<TreeItem>("SELECT i FROM TreeItem i FETCH ?", 1).First;
            if (exists == null)
            {
                return false;
            }
            return true;
        }

        public static void DeleteAll()
        {
            Db.SlowSQL("DELETE FROM TreeItem");
        }

        public static void Create()
        {
            Db.Transact(() =>
            {
                var Products = new TreeItem()
                {
                    Name = "Products"
                };

                var Food = new TreeItem()
                {
                    Name = "Food",
                    Parent = Products
                };

                var Dairy = new TreeItem()
                {
                    Name = "Dairy",
                    Parent = Food
                };

                var Milk = new TreeItem()
                {
                    Name = "Milk",
                    Parent = Dairy
                };

                var CoffeeMilk5ml = new TreeItem()
                {
                    Name = "Coffee milk 5 ML",
                    Parent = Milk
                };

                var Milk1l = new TreeItem()
                {
                    Name = "Milk 1 L",
                    Parent = Milk
                };

                var Meat = new TreeItem()
                {
                    Name = "Meat",
                    Parent = Food
                };

                var Metal = new TreeItem()
                {
                    Name = "Metal",
                    Parent = Products
                };

                var Screws = new TreeItem()
                {
                    Name = "Screws",
                    Parent = Metal
                };

                var PhillipsFlatHead = new TreeItem()
                {
                    Name = "Phillips flat head",
                    Parent = Screws
                };
            });
        }
    }
}