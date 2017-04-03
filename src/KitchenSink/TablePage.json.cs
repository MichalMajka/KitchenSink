using Starcounter;

namespace KitchenSink
{
    partial class TablePage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            TablePage.PetsElementJson pet;
            pet = this.Pets.Add();
            pet.Name = "Rocky";
            pet.Kind = "Dog";

            pet = this.Pets.Add();
            pet.Name = "Tigger";
            pet.Kind = "Cat";

            pet = this.Pets.Add();
            pet.Name = "Bella";
            pet.Kind = "Rabbit";
        }

        void Handle(Input.AddPetTrigger action)
        {
            var p = Pets.Add();
            p.Name = "Cecil";
            p.Kind = "Hamster";
        }
    }
}