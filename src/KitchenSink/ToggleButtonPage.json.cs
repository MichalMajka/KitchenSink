using Starcounter;

namespace KitchenSink
{
    partial class ToggleButtonPage : Json
    {
        public string CalculatedAcceptTermsAndConditionsReaction =>
            AcceptTermsAndConditions ? "I accept terms and conditions" : "I don't accept terms and conditions";
    }
}