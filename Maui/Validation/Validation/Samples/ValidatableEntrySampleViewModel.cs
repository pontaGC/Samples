using Shared.Mvvm;
using Shared.Validations.Rules;

namespace Validation.Samples
{
    internal class ValidatableEntrySampleViewModel
    {
        public ValidatableEntrySampleViewModel()
        {
            //
            // Sets Text property rules
            //
            Text.AddRule(new IsNotNullOrEmptyValidationRule());
            // Rule not to enter
            Text.AddRule(new DelegateValidationRule<string>(
                "NotContains# ",
                x => !x.Contains("#"),
                x => "This field must not contain #."));
        }

        public ValidatableObserveProperty<string> Text { get; } = new ValidatableObserveProperty<string>("Hello World!");
    }
}
