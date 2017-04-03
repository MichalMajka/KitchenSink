using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace KitchenSink
{
    public class ValidatorHelper<T> : AbstractValidator<T>
    {
        public override ValidationResult Validate(T instance)
        {
            var result = base.Validate(instance);
            foreach (PropertyRule rule in this)
            {
                var error = (BindErrorProperty<T>) rule.Validators.FirstOrDefault(e => e is BindErrorProperty<T>);
                if (error != null)
                {
                    var errorValidation = result.Errors.FirstOrDefault(e => e.PropertyName == rule.PropertyName);
                    error.ErrorAction(instance, errorValidation != null ? errorValidation.ErrorMessage : string.Empty);
                }
            }
            return result;
        }
    }

    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> BindError<T, TProperty>(this IRuleBuilder<T, TProperty> rule,
            Action<T, string> setupError)
        {
            return rule
                .SetValidator(new BindErrorProperty<T>(setupError))
                .Configure(config => { config.CascadeMode = CascadeMode.StopOnFirstFailure; });
        }
    }

    public class BindErrorProperty<T> : PropertyValidator
    {
        public readonly Action<T, string> ErrorAction;

        public BindErrorProperty(Action<T, string> errorAction) : base(string.Empty)
        {
            ErrorAction = errorAction;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return true;
        }
    }
}