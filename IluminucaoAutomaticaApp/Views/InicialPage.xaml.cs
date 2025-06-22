namespace IluminucaoAutomaticaApp.Views 
{
    public partial class InicialPage : ContentPage
    {
        public InicialPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void OnLigarClicked(object sender, EventArgs e)
        {
            // Implementar a lógica para ligar a lâmpada
            DisplayAlert("Ação", "Lâmpada ligada!", "OK");
        }

        private void OnDesligarClicked(object sender, EventArgs e)
        {
            // Implementar a lógica para desligar a lâmpada
            DisplayAlert("Ação", "Lâmpada desligada!", "OK");
        }
    }
}
