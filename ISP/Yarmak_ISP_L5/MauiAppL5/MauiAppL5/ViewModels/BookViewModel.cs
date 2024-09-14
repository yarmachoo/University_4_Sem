using _253503_Yarmak.Application.BookUseCases.Commands;
using _253503_Yarmak.Application.BookUseCases.Queries;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppL5.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppL5.ViewModels
{
    [QueryProperty(nameof(Book), "Book")]
    public partial class BookViewModel: ObservableObject
    {
        private readonly IMediator _mediator;
        public BookViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [ObservableProperty]
        Book book;
        [ObservableProperty]
        string bookName;
        [ObservableProperty]
        double bookRate;
        [ObservableProperty]
        string bookDescription;
        [ObservableProperty]
        string bookAuthorName;
        [ObservableProperty]
        int bookId;
        [RelayCommand]
        async void GetBookById()
        {
            Book = await _mediator.Send(new GetBookByIdQuery(Book.Id));
            if (Book is null)
                return;
            BookName = Book.Name;
            BookDescription = Book.Description;
            BookAuthorName = Book.Author.Name;
            BookRate = Book.Rate;
        }
        [RelayCommand]
        async Task UpdateBook() =>
            await GotoAddOrUpdatePage<AddBookPage>(new UpdateBookCommand() { Book = Book });
        private async Task GotoAddOrUpdatePage<Page>(IAddOrUpdateBookRequest request)
            where Page : ContentPage
        {
            IDictionary<string, object> parameters =
                new Dictionary<string, object>()
                {
                { "Request", request },
                {"Author", request.Book.Author!}
                };
            await Shell.Current
                .GoToAsync(nameof(AddBookPage), parameters);
        }
        [RelayCommand]
        async Task DeleteBook() =>
            await RemoveBook(Book);
        private async Task RemoveBook(Book book)
        {
            await _mediator.Send(new DeleteBookCommand(book));
            await Shell.Current.GoToAsync("..");
        }
    }
}
