using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GeographyQuizAvalonia.ViewModels
{
    /// <summary>
    /// A base class for view models that implements INotifyPropertyChanged.
    /// (Simulating functionality similar to CommunityToolkit.Mvvm.ComponentModel.ObservableObject)
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.
        /// This is automatically provided by the C# compiler if called from a property setter.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the property and raises PropertyChanged if the value has changed.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="field">The backing field of the property.</param>
        /// <param name="newValue">The new value for the property.</param>
        /// <param name="propertyName">The name of the property.
        /// This is automatically provided by the C# compiler if called from a property setter.</param>
        /// <returns>True if the value changed, false otherwise.</returns>
        protected virtual bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }

        // Overload for cases where property name is different from caller member name, or for dependent properties
        protected virtual bool SetProperty<T>(ref T field, T newValue, string propertyName, params string[] additionalPropertyNamesToRaise)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            OnPropertyChanged(propertyName);
            foreach(var additionalPropertyName in additionalPropertyNamesToRaise)
            {
                OnPropertyChanged(additionalPropertyName);
            }
            return true;
        }
    }
}
