namespace Shared.Validations.Rules
{
    /// <summary>
    /// The rule that the target object is not null or empty or does not contains of only white spaces.
    /// </summary>
    public class IsNotNullOrEmptyValidationRule : IValidationRule<string>
    {
        private readonly string error = "This Field must not empty.";

        /// <summary>
        /// Initializes a new instance of the <see cref="IsNotNullOrEmptyValidationRule" /> class.
        /// </summary>
        /// <param name="error">The error message.</param>
        public IsNotNullOrEmptyValidationRule(string error = null)
        {
            if (!string.IsNullOrEmpty(error))
            {
                this.error = error;
            }
        }

        /// <inheritdoc />
        public string Name => "IsNotNullOrEmptyValidationRule";

        /// <inheritdoc />
        public ValidationRuleResult<string> Apply(string? target)
        {
            return string.IsNullOrWhiteSpace(target)
                ? ValidationRuleResult<string>.Failed(this.Name, this.error)
                : ValidationRuleResult<string>.Passed(this.Name);
        }
    }
}
