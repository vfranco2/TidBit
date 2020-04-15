using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TidBit.Interfaces;
using Xamarin.Forms;

namespace TidBit.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        internal readonly ITBService TBService;

        public event PropertyChangedEventHandler PropertyChanged;

        public Action<bool> IsBusyChanged { get; set; }

        public bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                if (IsBusyChanged != null)
                    IsBusyChanged(isBusy);
            }

        }

        public ViewModelBase()
        {
            TBService = DependencyService.Get<ITBService>();
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }

        protected virtual async Task ExecuteLoadMoreCommand()
        {
        }
    }
}