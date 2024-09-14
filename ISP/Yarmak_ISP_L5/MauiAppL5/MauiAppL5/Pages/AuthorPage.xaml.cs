using MauiAppL5.ViewModels;

namespace MauiAppL5.Pages;

public partial class AuthorPage : ContentPage
{
	public AuthorPage(AuthorViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}