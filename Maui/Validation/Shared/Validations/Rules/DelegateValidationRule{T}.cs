using System.Diagnostics.CodeAnalysis;

namespace Shared.Validations.Rules
{
    /// <summary>
    /// The delegate rule.
    /// </summary>
    /// <typeparam name="T">The type of the target object to which the rule applies.</typeparam>
    public class DelegateValidationRule<T> : IValidationRule<T>
    {
        private readonly Predicate<T> applyRule;
        private readonly Func<T, string> getError;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateValidationRule{T}"/> class.
        /// </summary>
        /// <param name="ruleName">The rule name.</param>
        /// <param name="applyRule">The check function that the target object satisfy a rule.</param>
        /// <param name="getError">The function to get error if rule fails.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="ruleName"/> or <paramref name="applyRule"/> or <paramref name="getError"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="ruleName"/> is an empty string.</exception>
        public DelegateValidationRule(string ruleName, Predicate<T> applyRule, Func<T, string> getError)
        {
            ArgumentException.ThrowIfNullOrEmpty(ruleName);
            ArgumentNullException.ThrowIfNull(applyRule);
            ArgumentNullException.ThrowIfNull(getError);

            this.Name = ruleName;
            this.applyRule = applyRule;
            this.getError = getError;
        }

        /// <inheritdoc />
        [NotNull]
        public string Name { get; }

        /// <inheritdoc />
        public ValidationRuleResult<string> Apply(T? target)
        {
            var passed = applyRule(target);
            if (passed)
            {
                return ValidationRuleResult<string>.Passed(Name);
            }

            return ValidationRuleResult<string>.Failed(Name, getError(target));
        }
    }
}