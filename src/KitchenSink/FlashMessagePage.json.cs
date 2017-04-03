using Starcounter;

namespace KitchenSink
{
    partial class FlashMessagePage : Json
    {
        public void Handle(Input.ShowMessageTrigger Action)
        {
            Action.Cancel();
            this.ServerMessage = "This Message was set on the Server side!";

            Starcounter.Scheduling.ScheduleTask(() =>
            {
                System.Threading.Thread.CurrentThread.Join(3000);

                Starcounter.Session.ScheduleTask(this.Session.SessionId, (s, id) =>
                {
                    if (s == null)
                    {
                        return;
                    }

                    this.ServerMessage = null;
                    s.CalculatePatchAndPushOnWebSocket();
                });
            });
        }
    }
}
