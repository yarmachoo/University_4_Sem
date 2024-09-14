using MauiAppL5.Pages;

namespace MauiAppL5
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(BookPage),
            typeof(BookPage));
            Routing.RegisterRoute(nameof(AddBookPage),
                typeof(AddBookPage));
            Routing.RegisterRoute(nameof(AddAuthorPage),
                typeof(AddAuthorPage));
        }
    }
}
