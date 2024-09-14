using MauiAppL5.ViewModels;

namespace MauiAppL5.Pages;

public partial class AddAuthorPage : ContentPage
{
	public AddAuthorPage(AddAuthorViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}