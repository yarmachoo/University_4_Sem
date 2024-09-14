using MauiAppL5.ViewModels;

namespace MauiAppL5.Pages;

public partial class AddBookPage : ContentPage
{
	public AddBookPage(AddBookViewModel viewModel)

    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}