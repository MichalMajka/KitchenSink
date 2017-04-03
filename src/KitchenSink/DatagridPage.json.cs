using Starcounter;

namespace KitchenSink
{
    partial class DatagridPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            DatagridPagePetsElementJson pet;
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

    [DatagridPage_json.Pets]
    partial class DatagridPagePetsElementJson : Json
    {
        public string CalculatedSound
        {
            get
            {
                switch (Kind.ToLower())
                {
                    case "dog":
                        return "Woof";

                    case "cat":
                        return "Meow";

                    case "rabbit":
                        return "Jump";

                    case "hamster":
                        return "Squeak";

                    default:
                        return "";
                }
            }
        }
    }
}