using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool _carregando;

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

        public bool Carregando
        {
            get => _carregando;
            set => SetProperty(ref _carregando, value);
        }
    }
}
