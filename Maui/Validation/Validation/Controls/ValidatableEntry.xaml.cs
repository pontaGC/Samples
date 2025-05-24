namespace Validation;

using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

/// <summary>
/// Interaction logic for <see cref="ValidatableEntry"/>.xaml.
/// </summary>
public partial class ValidatableEntry
{
    #region Entry Properties

    public static readonly BindableProperty ClearButtonVisibilityProperty =
        BindableProperty.Create(nameof(ClearButtonVisibility), typeof(ClearButtonVisibility), typeof(ValidatableEntry), ClearButtonVisibility.Never);

    /// <summary>
    /// Determines the behavior of the clear text button on this entry. This is a bindable property.
    /// </summary>
    public ClearButtonVisibility ClearButtonVisibility
    {
        get => (ClearButtonVisibility)this.GetValue(ClearButtonVisibilityProperty);
        set => this.SetValue(ClearButtonVisibilityProperty, value);
    }

    public static readonly BindableProperty HorizontalTextAlignmentProperty =
        BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(ValidatableEntry), TextAlignment.Start);

    /// <summary>
    /// Gets or sets the horizontal text alignment. This is a bindable property.
    /// </summary>
    public TextAlignment HorizontalTextAlignment
    {
        get => (TextAlignment)this.GetValue(HorizontalTextAlignmentProperty);
        set => this.SetValue(HorizontalTextAlignmentProperty, value);
    }    

    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(ValidatableEntry), false);

    /// <summary>
    /// Gets or sets a value that indicates if the entry should visually obscure typed text.
    /// Value is <c>true</c> if the element is a password box; otherwise, <c>false</c>. Default value is <c>false</c>.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>Toggling this value does not reset the contents of the entry, therefore it is advisable to be careful about setting <see cref="IsPassword"/> to false, as it may contain sensitive information.</remarks>
    public bool IsPassword
    {
        get => (bool)this.GetValue(IsPasswordProperty);
        set => this.SetValue(IsPasswordProperty, value);
    }

    public static readonly BindableProperty ReturnCommandProperty =
        BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(ValidatableEntry), default(ICommand));

    /// <summary>
    /// Gets or sets the command to run when the user presses the return key, either physically or on the on-screen keyboard.
    /// This is a bindable property.
    /// </summary>
    public ICommand ReturnCommand
    {
        get => (ICommand)this.GetValue(ReturnCommandProperty);
        set => this.SetValue(ReturnCommandProperty, value);
    }

    public static readonly BindableProperty ReturnCommandParameterProperty =
        BindableProperty.Create(nameof(ReturnCommandParameter), typeof(object), typeof(ValidatableEntry), default(object));

    /// <summary>
    /// Gets or sets the parameter object for the <see cref="ReturnCommand" /> that can be used to provide extra information.
    /// This is a bindable property.
    /// </summary>
    public object ReturnCommandParameter
    {
        get => this.GetValue(ReturnCommandParameterProperty);
        set => this.SetValue(ReturnCommandParameterProperty, value);
    }

    public static readonly BindableProperty ReturnTypeProperty =
        BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(ValidatableEntry), ReturnType.Default);

    /// <summary>
    /// Gets or sets the type of the return key on the software keyboard.
    /// </summary>
    public ReturnType ReturnType
    {
        get => (ReturnType)this.GetValue(ReturnTypeProperty);
        set => this.SetValue(ReturnTypeProperty, value);
    }

    public static readonly BindableProperty VerticalTextAlignmentProperty =
        BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(ValidatableEntry), TextAlignment.Center);

    /// <summary>
    /// Gets or sets the vertical text alignment. This is a bindable property.
    /// </summary>
    public TextAlignment VerticalTextAlignment
    {
        get => (TextAlignment)this.GetValue(VerticalTextAlignmentProperty);
        set => this.SetValue(VerticalTextAlignmentProperty, value);
    }

    #region InputView

    public static readonly BindableProperty CharacterSpacingProperty =
        BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(ValidatableEntry), 0.0d);

    /// <summary>
    /// Gets or sets a value that indicates the number of device-independent units that should be in between characters in the text displayed by the Entry.
    /// Applies to Text and Placeholder.
    /// </summary>
    public double CharacterSpacing
    {
        get => (double)this.GetValue(CharacterSpacingProperty);
        set => this.SetValue(CharacterSpacingProperty, value);
    }

    public static readonly BindableProperty CursorPositionProperty =
        BindableProperty.Create(nameof(CursorPosition), typeof(int), typeof(ValidatableEntry), 0, validateValue: (b, v) => (int) v >= 0);

    /// <summary>
    /// Gets or sets the position of the cursor. The value must be more than or equal to 0 and less or equal to the length of <see cref=" Text"/>.
    /// This is a bindable property.
    /// </summary>
    public int CursorPosition
    {
        get => (int)this.GetValue(CursorPositionProperty);
        set => this.SetValue(CursorPositionProperty, value);
    }   

    public static readonly BindableProperty FontAttributesProperty =
        BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(ValidatableEntry), FontAttributes.None);

    /// <summary>
    /// Gets or sets a value that indicates whether the font for the text of this entry is bold, italic, or neither. This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)this.GetValue(FontAttributesProperty);
        set => this.SetValue(FontAttributesProperty, value);
    }

    public static readonly BindableProperty FontAutoScalingEnabledProperty =
            BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(ValidatableEntry), true);

    /// <summary>
    /// Determines whether or not the font of this entry should scale automatically according to the operating system settings.
    /// Default value is <c>true</c>. This is a bindable property.
    /// </summary>
    public bool FontAutoScalingEnabled
    {
        get => (bool)this.GetValue(FontAutoScalingEnabledProperty);
        set => this.SetValue(FontAutoScalingEnabledProperty, value);
    }

    public static readonly BindableProperty FontFamilyProperty =
        BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ValidatableEntry), default(string));

    /// <summary>
    /// Gets or sets the font family to use for the text.
    /// </summary>
    public string FontFamily
    {
        get => (string)this.GetValue(FontFamilyProperty);
        set => this.SetValue(FontFamilyProperty, value);
    }

    public static readonly BindableProperty FontSizeProperty =
       BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ValidatableEntry), 11.0d);

    /// <summary>
    /// Gets or sets the font size of the text. The default font size is 11.0.
    /// </summary>
    public double FontSize
    {
        get => (double)this.GetValue(FontSizeProperty);
        set => this.SetValue(FontSizeProperty, value);
    }

    public static readonly BindableProperty IsReadOnlyProperty =
        BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(ValidatableEntry), false);

    /// <summary>
    /// Gets or sets a value that indicates whether user should be prevented from modifying the text. Default is <c>false</c>.
    /// </summary>
    public bool IsReadOnly
    {
        get => (bool)this.GetValue(IsReadOnlyProperty);
        set => this.SetValue(IsReadOnlyProperty, value);
    }

    public static readonly BindableProperty IsSpellCheckEnabledProperty =
        BindableProperty.Create(nameof(IsSpellCheckEnabled), typeof(bool), typeof(ValidatableEntry), true);

    /// <summary>
    /// Gets or sets a value that controls whether spell checking is enabled.
    /// </summary>
    /// <remarks>On Windows, spellchecking also turns on auto correction.</remarks>
    public bool IsSpellCheckEnabled
    {
        get => (bool)this.GetValue(IsSpellCheckEnabledProperty);
        set => this.SetValue(IsSpellCheckEnabledProperty, value);
    }

    public static readonly BindableProperty IsTextPredictionEnabledProperty =
        BindableProperty.Create(nameof(IsTextPredictionEnabled), typeof(bool), typeof(ValidatableEntry), true);

    /// <summary>
    /// Gets or sets a value that controls whether text prediction and automatic text correction are enabled.
    /// </summary>
    /// <remarks>On Windows, text prediction only affects touch keyboards and only affects keyboard word suggestions.</remarks>
    public bool IsTextPredictionEnabled
    {
        get => (bool)this.GetValue(IsTextPredictionEnabledProperty);
        set => this.SetValue(IsTextPredictionEnabledProperty, value);
    }

    public static readonly BindableProperty KeyboardProperty =
        BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(ValidatableEntry), Keyboard.Default, coerceValue: (o, v) => (v as Keyboard) ?? Keyboard.Default);

    /// <summary>
    /// Gets or sets the type of keyboard used for text input.   
    /// </summary>
    public Keyboard Keyboard
    {
        get => (Keyboard)this.GetValue(KeyboardProperty);
        set => this.SetValue(KeyboardProperty, value);
    }
  
    public static readonly BindableProperty MaxLengthProperty =
       BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(ValidatableEntry), int.MaxValue);

    /// <summary>
    /// Gets or sets the maximum allowed length of input.
    /// The default value is <c>Int.MaxValue</c>.
    /// </summary>
    public int MaxLength
    {
        get => (int)this.GetValue(MaxLengthProperty);
        set => this.SetValue(MaxLengthProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ValidatableEntry), default(string));

    /// <summary>
    /// Gets or sets the text that is displayed when the control is empty.
    /// </summary>
    public string Placeholder
    {
        get => (string)this.GetValue(PlaceholderProperty);
        set => this.SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty PlaceholderColorProperty =
        BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(ValidatableEntry), default(Color));

    /// <summary>
    /// Gets or sets the color of the placeholder text.
    /// </summary>
    public Color PlaceholderColor
    {
        get => (Color)this.GetValue(PlaceholderColorProperty);
        set => this.SetValue(PlaceholderColorProperty, value);
    }

    public static readonly BindableProperty SelectionLengthProperty =
      BindableProperty.Create(nameof(SelectionLength), typeof(int), typeof(ValidatableEntry), 0, validateValue: (b, v) => (int)v >= 0);

    /// <summary>
    /// Gets or sets the length of the selection. The selection will start at <see cref="CursorPosition"/>.
    /// This is a bindable property.
    /// </summary>
    public int SelectionLength
    {
        get => (int)this.GetValue(SelectionLengthProperty);
        set => this.SetValue(SelectionLengthProperty, value);
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(ValidatableEntry), defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    /// Gets or sets the text of the input view. This is a bindable property.
    /// </summary>
    public string Text
    {
        get => (string)this.GetValue(TextProperty);
        set => this.SetValue(TextProperty, value);
    }

    public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ValidatableEntry), Colors.Black);

    /// <summary>
    /// Gets or sets the color of the text.
    /// </summary>
    public Color TextColor
    {
        get => (Color)this.GetValue(TextColorProperty);
        set => this.SetValue(TextColorProperty, value);
    }

    public static readonly BindableProperty TextTransformProperty =
        BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(ValidatableEntry), TextTransform.Default);

    /// <summary>
    /// Determines the text transformation on an element.
    /// </summary>
    public TextTransform TextTransform
    {
        get => (TextTransform)this.GetValue(TextTransformProperty);
        set => this.SetValue(TextTransformProperty, value);
    }

    #endregion

    #endregion

    #region Custom Properties

    public static readonly BindableProperty HasErrorProperty =
        BindableProperty.Create(nameof(HasError), typeof(bool), typeof(ValidatableEntry), false, BindingMode.OneWay, propertyChanged:OnHasErrorChanged);

    /// <summary>
    /// Gets or sets a value indicating errors occurs.
    /// </summary>
    public  bool HasError
    {
        get => (bool)this.GetValue(HasErrorProperty);
        set => this.SetValue(HasErrorProperty, value);
    }

    public static readonly BindableProperty ErrorsProperty =
        BindableProperty.Create(nameof(Errors), typeof(IEnumerable<string>), typeof(ValidatableEntry), Enumerable.Empty<string>(), BindingMode.OneWay);

    /// <summary>
    /// Gets or sets all error message.
    /// </summary>
    public IEnumerable<string> Errors
    {
        get => (IEnumerable<string>)this.GetValue(ErrorsProperty);
        set => this.SetValue(ErrorsProperty, value ?? Enumerable.Empty<string>());
    }

    public static readonly BindableProperty ErrorColorProperty =
        BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(ValidatableEntry), Colors.Red, BindingMode.OneWay);

    /// <summary>
    /// Gets or sets an error color to use error display.
    /// </summary>
    public Color ErrorColor
    {
        get => (Color)this.GetValue(ErrorColorProperty);
        set => this.SetValue(ErrorColorProperty, value);
    }    

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a instance of the <see cref="ValidatableEntry" /> class.
    /// </summary>
    public ValidatableEntry()
	{
		InitializeComponent();
	}

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the user finalizes the text in an entry with the return key.
    /// </summary>
    public event EventHandler Completed
    {
        add { this.Field.Completed += value; }
        remove { this.Field.Completed -= value; }
    }

    #endregion

    #region Private Methods 

    private static void OnHasErrorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not ValidatableEntry validatableEntry || newValue is not bool newHasError)
        {
            return;
        }

        ChangeErrorContentLabel(validatableEntry, newHasError);
    }

    private static void ChangeErrorContentLabel(ValidatableEntry self, bool hasError)
    {
        if (hasError)
        {
            self.ErrorContentLabel.Text = self.Errors.FirstOrDefault();
            self.ErrorContentLabel.IsVisible = true;
        }
        else
        {
            self.ErrorContentLabel.Text = null;
            self.ErrorContentLabel.IsVisible = false;
        }
    }

    #endregion
}