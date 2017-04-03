using FluentValidation;
using Starcounter;

namespace KitchenSink
{
    partial class ValidationPage : Json
    {
        private SettingsValidator _settingsValidator;

        protected override void OnData()
        {
            base.OnData();
            _settingsValidator = new SettingsValidator();
        }

        void Handle(Input.ValidateTrigger action)
        {
            _settingsValidator.Validate(this);
        }
    }

    public class SettingsValidator : ValidatorHelper<ValidationPage>
    {
        public SettingsValidator()
        {
            RuleFor(item => item.Name).NotEmpty().BindError((item, err) => { item.ErrorName = err; });
            RuleFor(item => item.LastName).NotEmpty().BindError((item, err) => { item.ErrorLastName = err; });
        }
    }
}