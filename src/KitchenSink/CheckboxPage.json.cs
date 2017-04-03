using Starcounter;

namespace KitchenSink
{
    partial class CheckboxPage : Json
    {
        public string CalculatedDrivingLicenseReaction
        {
            get
            {
                if (DrivingLicense == true)
                {
                    return "You can drive";
                }
                else
                {
                    return "You can't drive";
                }
            }
        }
    }
}