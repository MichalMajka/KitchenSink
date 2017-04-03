using Starcounter;

namespace KitchenSink
{
    [Database]
    public class GeoCoordinates
    {
        public double Latitude;
        public double Longitude;
    }

    partial class GeoPage : Json
    {
        //Stockholm coordinates
        public readonly double DefaultLatitude = 59.3319913;
        public readonly double DefaultLongitude = 18.0765409;

        public void Init()
        {
            Position.Data = Db.SQL<GeoCoordinates>("SELECT gp FROM GeoCoordinates gp").First;
            if (Position.Data == null)
            {
                Position.Data = new GeoCoordinates()
                {
                    Latitude = DefaultLatitude,
                    Longitude = DefaultLongitude
                };
            }
        }
    }

    [GeoPage_json.Position]
    partial class GeoPagePosition : Json, IBound<GeoCoordinates>
    {
        static GeoPagePosition() {
            DefaultTemplate.Latitude.InstanceType = typeof(double);
            DefaultTemplate.Longitude.InstanceType = typeof(double);
        }

        public void Handle(Input.ResetTrigger action)
        {
            var geoPageParent = (GeoPage) Parent;
            Latitude = geoPageParent.DefaultLatitude;
            Longitude = geoPageParent.DefaultLongitude;
            PushChanges();
        }

        public void Handle(Input.Latitude action)
        {
            Latitude = action.Value;
            PushChanges();
        }

        public void Handle(Input.Longitude action)
        {
            Longitude = action.Value;
            PushChanges();
        }

        protected void PushChanges()
        {
            Transaction.Commit();
            Session.ForAll((s, sessionId) =>
            {
                var master = s.Data as MasterPage;
                var navpage = master?.CurrentPage as NavPage;
                if (!(navpage?.CurrentPage is GeoPage)) return;
                if ((GeoPage) navpage.CurrentPage != null)
                {
                    s.CalculatePatchAndPushOnWebSocket();
                }
            });
        }
    }
}