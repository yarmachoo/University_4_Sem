using MauiAppL5.ViewModels;

namespace MauiAppL5.Pages;

public partial class BookPage : ContentPage
{
	public BookPage(BookViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}