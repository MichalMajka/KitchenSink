using Starcounter;
using System.Threading;
using System;

namespace KitchenSink
{
    partial class LazyLoadingPage : Json
    {
        protected override void OnData()
        {
            base.OnData();
            FillDummyData();
        }

        private void FillDummyData()
        {
            CreatePerson(1, "Alicia", "Alcott");
            CreatePerson(2, "Beatrice", "Black");
            CreatePerson(3, "Claire", "Clancy");
            CreatePerson(4, "Delilah", "Darcy");
            CreatePerson(5, "Ellie", "Earnhart");
            CreatePerson(6, "Faith", "Fahrlander");
            CreatePerson(7, "Grace", "Gather");
        }

        private void CreatePerson(int order, string firstName, string lastName, string favoriteGame = "")
        {
            LazyLoadingPagePeople person;
            person = People.Add();
            person.Order = order;
            person.FirstName = firstName;
            person.LastName = lastName;
            person.FavoriteGame = favoriteGame;
        }

        [LazyLoadingPage_json.People]
        partial class LazyLoadingPagePeople : Json
        {
            protected int minDataRetrievalDelay = 300;
            protected int maxDataRetrievalDelay = 1000;

            public LazyLoadingPage ParentPage
            {
                get
                {
                    return this.Parent.Parent as LazyLoadingPage;
                }
            }

            // IsHovered is set on the client side. Every time a person gets hovered, isHovered is set to 1.
            // And the value is set to 0 on blur.
            public void Handle(Input.IsHovered action)
            {
                if (!this.DataIsLoaded && action.Value != 0)
                {
                    Random rnd = new Random();
                    StartDataRetrieval(rnd.Next(minDataRetrievalDelay, maxDataRetrievalDelay), Session.SessionId);
                }
            }

            void StartDataRetrieval(int delay, string sessionId)
            {
                Scheduling.ScheduleTask(() =>
                {
                    string data = this.RetrieveDataFromFakeDataBase(this.FirstName);

                    Thread.CurrentThread.Join(delay);
                    DataRetrievalUpate(sessionId, data);
                }, false); // wait for completion: false = run in background.
            }

            void DataRetrievalUpate(string sessionId, string data)
            {
                Session.ScheduleTask(sessionId, (session, id) =>
                {
                    if (session == null)
                    {
                        return;
                    }

                    this.DataIsLoaded = true;
                    this.DataToShow = data;
                    this.FavoriteGame = data;

                    if (this.IsHovered == 1)
                    {
                        this.ParentPage.DisplayedData.DataContent = data;
                    }

                    session.CalculatePatchAndPushOnWebSocket();
                });
            }

            // Acts as a fake database, and provide each person with a favorite game
            public string RetrieveDataFromFakeDataBase(string firstName)
            {
                switch (firstName)
                {
                    case "Alicia":
                        return this.FavoriteGame = "The Last of Us";

                    case "Beatrice":
                        return this.FavoriteGame = "Dragon Age: Inquisition";

                    case "Claire":
                        return this.FavoriteGame = "Final Fantasy XIII";

                    case "Delilah":
                        return this.FavoriteGame = "World of Warcraft";

                    case "Ellie":
                        return this.FavoriteGame = "Overwatch";

                    case "Faith":
                        return this.FavoriteGame = "Pokemon X";

                    case "Grace":
                        return this.FavoriteGame = "Counter Strike";
                    default:
                        throw new ArgumentException();
                }
            }
        }
    }
}
