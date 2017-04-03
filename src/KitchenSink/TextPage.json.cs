using Starcounter;

namespace KitchenSink
{
    partial class TextPage : Json
    {
        public string CalculatedNameReaction
        {
            get
            {
                if (Name == "")
                {
                    return "What's your name?";
                }
                else
                {
                    return "Hi, " + Name + "!";
                }
            }
        }

        public string CalculatedNameLiveReaction
        {
            get
            {
                if (NameLive == "")
                {
                    return "What's your name?";
                }
                else
                {
                    return "Hi, " + NameLive + "!";
                }
            }
        }
    }
}