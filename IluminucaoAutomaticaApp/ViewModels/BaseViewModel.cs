using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string nomePropriedade = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string nomePropriedade = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(nomePropriedade);
            return true;
        }
    }
}
