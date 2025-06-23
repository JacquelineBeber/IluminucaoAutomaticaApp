using CommunityToolkit.Mvvm.Messaging;
using IluminucaoAutomaticaApp.Views.Componentes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace IluminucaoAutomaticaApp.Views
{
    public partial class AppFlyout : FlyoutPage
    {
        public AppFlyout()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}