using Starcounter;

namespace KitchenSink
{
    partial class TextareaPage : Json
    {
        public string CalculatedBioReaction
        {
            get { return "Length of your bio: " + Bio.Length + " chars"; }
        }
    }
}