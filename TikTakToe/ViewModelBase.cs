using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace TikTakToe
{
    /// <summary>
    /// The base class of ViewModel which implements
    /// INotifyPropertyChanged interface.
    /// Also implements dispatcher method for multithreaded property access.
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Helper function to dispatch property access so that it can be done
        /// from background thread.
        /// </summary>
        /// <param name="f">side effect action to be performed.</param>
        protected void Dispatch(Action f) => Application.Current.Dispatcher.Invoke(f);

        /// <summary>
        /// Helper function to dispatch property access so that it can be done
        /// from background thread.
        /// </summary>
        /// <param name="f">side effect action to be performed.</param>
        protected TResult Dispatch<TResult>(Func<TResult> f) => Application.Current.Dispatcher.Invoke(f);
    }
}
