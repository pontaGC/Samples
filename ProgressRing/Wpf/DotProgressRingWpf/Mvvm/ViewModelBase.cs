using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mvvm
{
    /// <summary>
    /// The view-model base class.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region INotifyPropertyChanged

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => this.propertyChangedEvent += value;
            remove => this.propertyChangedEvent -= value;
        }

        private PropertyChangedEventHandler propertyChangedEvent;

        #endregion

        #region INotifyPropertyChanging

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging
        {
            add => this.propertyChangingEvent += value;
            remove => this.propertyChangingEvent -= value;
        }

        private PropertyChangingEventHandler propertyChangingEvent;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Sets the property and notify property changed
        /// </summary>
        /// <param name="backingStore">The backing field of the property.</param>
        /// <param name="value">The setting value.</param>
        /// <param name="onChanged">The action that is called after the property value has been changed.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns><c>true</c> if the value was changed, <c>false</c> if the existing value matched the desired value.</returns>
        protected virtual bool SetProperty<T>(ref T backingStore, T value, Action onChanged, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            this.OnPropertyChanging(propertyName);

            backingStore = value;
            onChanged();

            this.OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Sets the property and notify property changed
        /// </summary>
        /// <param name="backingStore">The backing field of the property.</param>
        /// <param name="value">The setting value.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns><c>true</c> if the value was changed, <c>false</c> if the existing value matched the desired value.</returns>
        protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            this.OnPropertyChanging(propertyName);

            backingStore = value;

            this.OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Raises property changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notifies that a property is changed.
        /// </summary>
        /// <param name="e">The property changed event args.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this.propertyChangedEvent?.Invoke(this, e);
        }

        /// <summary>
        /// Raises property changing event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            this.OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Notifies that a property is changing.
        /// </summary>
        /// <param name="e">The property changing event args.</param>
        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            this.propertyChangingEvent?.Invoke(this, e);
        }

        #endregion
    }
}

