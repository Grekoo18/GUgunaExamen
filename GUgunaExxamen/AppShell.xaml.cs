using GUgunaExxamen.Pages;

namespace GUgunaExxamen
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegistroPage), typeof(RegistroPage));
            Routing.RegisterRoute(nameof(ResumenPage), typeof(ResumenPage));
        }
    }
}
