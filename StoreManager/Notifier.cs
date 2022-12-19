using System;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;

namespace StoreManager
{
    public abstract class Notifier : INotifyPropertyChanged, IDataErrorInfo
    {
        private Dictionary<string, string> m_ErrorDictionary = new Dictionary<string, string>();
        //___________________________________________________________________________________________
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual void ShowError(string Message)
        {
            MessageBox.Show(Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected virtual void ShowInformation(string Message)
        {
            MessageBox.Show(Message, "گزارش", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        protected bool RemoveError(string columnName)
        {
            return m_ErrorDictionary.Remove(columnName);
        }

        public string Error { get; protected set; }

        public string this[string columnName]
        {
            get
            {
                if (m_ErrorDictionary.ContainsKey(columnName))
                    return m_ErrorDictionary[columnName];
                else return this.Error;
            }
            protected set
            {
                if (!m_ErrorDictionary.ContainsKey(columnName))
                    m_ErrorDictionary.Add(columnName, value);
                else m_ErrorDictionary[columnName] = value;
            }
        }
    }
}
