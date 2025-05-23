using CommunityToolkit.Mvvm.ComponentModel;
using Shared.Validations.Rules;

namespace Shared.Mvvm;

/// <summary>
/// The observable property with validation.
/// </summary>
/// <typeparam name="T">The type of a property.</typeparam>
public class ValidatableObserveProperty<T> : ObservableObject
{
    private readonly List<string> errors = new List<string>();
    private readonly ValidationRuleList<T> validations = new ValidationRuleList<T>();
    private T? value;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatableObserveProperty{T}" /> class.
    /// </summary>
    /// <param name="initialValue">The initial value.</param>
    public ValidatableObserveProperty(T initialValue = default)
    {
        this.Value = initialValue;
    }

    /// <summary>
    /// Gets all property rules.
    /// </summary>
    public IEnumerable<IValidationRule<T>> Validations => validations;

    /// <summary>
    /// Gets all error message.
    /// </summary>
    public IEnumerable<string> Errors => this.errors;

    /// <summary>
    /// Gets a value indicating the property has error.
    /// </summary>
    public bool HasError => this.Errors.Any();

    /// <summary>
    /// Gets or sets the property value.
    /// </summary>
    public T? Value
    {
        get => this.value;
        set
        {
            if (SetProperty(ref this.value, value))
            {
                // Validates a new value
                ValidateAll();
            }
        }
    }

    /// <summary>
    /// Adds a validation rule.
    /// </summary>
    /// <param name="rule">The rule to add.</param>
    /// <returns>The self.</returns>
    public ValidatableObserveProperty<T> AddRule(IValidationRule<T> rule)
    {
        if (rule is not null)
        {
            this.validations.Add(rule);
        }

        return this;
    }

    /// <summary>
    /// Validiates the current property value with the specified rule.
    /// If the given validation rule is <c>null</c> or an empty string, validate it with all rules.
    /// </summary>
    /// <param name="ruleName">The name of validation rule.</param>
    /// <returns>The enumerable to error if rule fails.</returns>
    public IEnumerable<string> Validate(string ruleName = null)
    {
        var currentValue = this.Value;

        var errors = string.IsNullOrEmpty(ruleName)
            ? this.validations.Apply(currentValue)
            : this.validations.Apply(ruleName, currentValue);

        return errors;
    }

    /// <summary>
    /// Valdiates the current property value.
    /// </summary>
    /// <returns><c>true</c> If all validation rule checks pass, otherwise, <c>false</c>.</returns>
    /// <remarks>The detected errors are stored in <see cref="Errors"/> property.</remarks>
    public bool ValidateAll()
    {
        var currentValue = this.Value;

        OnPropertyChanging(nameof(this.Errors));
        OnPropertyChanging(nameof(this.HasError));

        this.errors.Clear();
        this.errors.AddRange(this.validations.Apply(currentValue));

        OnPropertyChanged(nameof(this.HasError));
        OnPropertyChanged(nameof(this.Errors));

        return !this.errors.Any();
    }
}

