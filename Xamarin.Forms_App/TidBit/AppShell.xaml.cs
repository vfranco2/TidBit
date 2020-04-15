using Xamarin.Forms;

namespace TidBit
{
    public partial class AppShell : Shell
    {
        protected override bool OnBackButtonPressed()
        {
            Current.Navigation.PopAsync(false); return true;
        }

        public AppShell()
        {
            InitializeComponent();
        }
    }
}