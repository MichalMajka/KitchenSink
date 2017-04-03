using Starcounter;

namespace KitchenSink
{
    partial class PasswordPage : Json
    {
        public string CalculatedPasswordReaction
        {
            get
            {
                if (Password.Length < 6)
                {
                    return "Password must be at least 6 chars long";
                }
                else
                {
                    return "Good password!";
                }
            }
        }
    }
}