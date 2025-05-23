using System.Diagnostics.CodeAnalysis;

namespace Shared.Validations.Rules
{
    /// <summary>
    /// One rule that a target object must satisfy.
    /// The <c>Error</c> property can be used as an error message.
    /// </summary>
    /// <typeparam name="T">The type of the target object to which the rule applies.</typeparam>
    public interface IValidationRule<T>
    {
        /// <summary>
        /// Gets a rule name.
        /// </summary>
        [NotNull]
        string Name { get; }

        /// <summary>
        /// Checks whether the given object satisfies this rule.
        /// </summary>
        /// <param name="target">The target object to check.</param>
        /// <returns>The checked result.</returns>
        ValidationRuleResult<string> Apply(T? target);
    }
}
