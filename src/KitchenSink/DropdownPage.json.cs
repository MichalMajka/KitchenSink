using Starcounter;

namespace KitchenSink
{
    [Database]
    public class SoftwareProduct
    {
        public string Name;
        public string Key
        {
            get
            {
                return this.GetObjectID();
            }
        }
    }

    partial class DropdownPage : Json
    {
        static DropdownPage()
        {
            DropdownPage.DefaultTemplate.SelectedProductKey.Bind = "SelectedProductKeyBind";
        }

        protected override void OnData()
        {
            base.OnData();

            DropdownPage.PetsElementJson pet;
            pet = this.Pets.Add();
            pet.Label = "dogs";

            pet = this.Pets.Add();
            pet.Label = "cats";

            pet = this.Pets.Add();
            pet.Label = "rabbit";

            this.SelectedPet = "dogs";

            this.Products.Data = Db.SQL("SELECT p FROM KitchenSink.SoftwareProduct p ORDER BY p.Name");
            this.SelectedProduct.Data = Db.SQL("SELECT p FROM KitchenSink.SoftwareProduct p ORDER BY p.Name DESC").First;
        }

        public string CalculatedPetReaction
        {
            get { return "You like " + SelectedPet; }
        }

        public string SelectedProductKeyBind
        {
            get
            {
                if (this.SelectedProduct == null)
                {
                    return null;
                }

                return this.SelectedProduct.Key;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.SelectedProduct.Data = null;
                    return;
                }

                this.SelectedProduct.Data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(value)) as SoftwareProduct;
            }
        }
    }
}