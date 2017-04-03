using Starcounter;

namespace KitchenSink {
    [Database]
    public class Person {
        public string Name;
        public int SequenceNumber;
    }

    partial class SortableListPage : Json {
        protected override void OnData() {
            base.OnData();
            
            var people = Db.SQL<Person>("SELECT p FROM KitchenSink.Person p ORDER BY p.SequenceNumber");
            foreach(var person in people) {
                var a = PersonItems.Add();
                a.Person.Data = person;
            }
        }

        public void MovePersonUp(PersonItemsElement personItem) {
            var toChange = this.PersonItems.IndexOf(personItem);
            if(toChange < 1) return;

            Db.Transact(() => {
                (this.PersonItems[toChange - 1].Person.Data as Person).SequenceNumber += 1;
                (personItem.Person.Data as Person).SequenceNumber -= 1;
            });
            PersonItems.RemoveAt(toChange);
            PersonItems.Insert(toChange - 1, personItem);
        }

        public void MovePersonDown(PersonItemsElement personItem) {
            var toChange = this.PersonItems.IndexOf(personItem);
            if(toChange >= this.PersonItems.Count - 1) return;

            Db.Transact(() => {
                (this.PersonItems[toChange + 1].Person.Data as Person).SequenceNumber -= 1;
                (personItem.Person.Data as Person).SequenceNumber += 1;
            });
            PersonItems.RemoveAt(toChange);
            PersonItems.Insert(toChange + 1, personItem);
        }

        public bool IsFirst(PersonItemsElement personItem) {
            return PersonItems.IndexOf(personItem) == 0;
        }

        public bool IsLast(PersonItemsElement personItem) {
            return PersonItems.IndexOf(personItem) == PersonItems.Count - 1;
        }
    }

    [SortableListPage_json.PersonItems]
    partial class PersonItemsElement : Json {

        void Handle(Input.MovePersonUpTrigger action) {
            var parent = this.Parent.Parent as SortableListPage;
            parent?.MovePersonUp(this);
        }

        void Handle(Input.MovePersonDownTrigger action) {
            var parent = this.Parent.Parent as SortableListPage;
            parent?.MovePersonDown(this);
        }
    }
}
