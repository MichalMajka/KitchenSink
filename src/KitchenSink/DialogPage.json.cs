using Starcounter;

namespace KitchenSink
{
    partial class DialogPage : Json
    {
        void Handle(Input.OpenTrigger Action)
        {
            Action.Cancel();

            this.Opened = true;
        }

        void Handle(Input.ConfirmTrigger Action)
        {
            this.Opened = false;
            this.Message = "You have accepted the dialog box";
            this.MessageCss = "alert alert-success";
        }

        void Handle(Input.RejectTrigger Action)
        {
            this.Opened = false;
            this.Message = "You have rejected the dialog box";
            this.MessageCss = "alert alert-danger";
        }
    }
}