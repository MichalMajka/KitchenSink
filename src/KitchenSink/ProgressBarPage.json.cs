using Starcounter;

namespace KitchenSink
{
    partial class ProgressBarPage : Json
    {
        protected override void OnData()
        {
            base.OnData();
        }

        void Handle(Input.StartProgressTrigger action)
        {
            if (this.TaskIsRunnning)
            {
                return;
            }

            this.Progress = 0;
            this.TaskIsRunnning = true;
            StartSimpleProgressBar(30, Session.SessionId);
        }

        void StartSimpleProgressBar(int delay, string sessionId)
        {
            long tempProgress = this.Progress;
            this.FileDownloadText = "Downloading File";
            
            Scheduling.ScheduleTask(() =>
            {
                while (tempProgress < 100)
                {
                    System.Threading.Thread.CurrentThread.Join(delay); // sleep function - handles the delay between the incrementation of this.Progress
                    tempProgress++;
                    SimpleProgressUpdate(sessionId, tempProgress);
                }
            }, false); // Wait for completion - If false: it will continue to run the script even though the scheduled task is running in the background
        }

        void SimpleProgressUpdate(string sessionId, long progress)
        {
            Session.ScheduleTask(sessionId, (session, id) =>
            {
                // The session might be disposed if user disconnects before the task has change to execute
                if (session == null)
                {
                    return;
                }

                this.Progress = progress;

                if (this.Progress >= 100)
                {
                    this.FileDownloadText = "Download Complete";
                    this.DownloadButtonText = "Download another (imaginary) file!";
                    this.TaskIsRunnning = false;
                }

                session.CalculatePatchAndPushOnWebSocket();
            });
        }
    }
}
