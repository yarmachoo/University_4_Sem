using _253503_Yarmak.Application.AuthorUseCases.Commands;
using _253503_Yarmak.Application.AuthorUseCases.Queries;
using _253503_Yarmak.Application.BookUseCases.Commands;
using _253503_Yarmak.Application.BookUseCases.Queries;
using _253503_Yarmak.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppL5.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppL5.ViewModels
{
    public partial class AuthorViewModel: ObservableObject
    {
        private readonly IMediator _mediator;
        public AuthorViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public ObservableCollection<Author> Authors { get; set; } = new();
        public ObservableCollection<Book> Books { get; set; } = new();
        [ObservableProperty] Author selectedAuthor = new();
        [ObservableProperty] Book selectedBook = new();
        [ObservableProperty] int booksCount;
        [ObservableProperty] string errorText;
        public async Task GetAuthors()
        {
            var authors = await _mediator.Send(new GetAuthorsQuery());
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Authors.Clear();
                foreach (var author in authors)
                {
                    Authors.Add(author);
                }
            });
        }
        public async Task GetBooks()
        {
            if (SelectedAuthor is null)
            {
                Books.Clear();
                return;
            }
            var books = await _mediator.Send(new
                GetBookByAuthorIdQuery(SelectedAuthor.Id));
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Books.Clear();
                foreach (var book in books)
                    Books.Add(book);
                BooksCount = Books.Count;
            });
        }
        [RelayCommand]
        async Task UpdateAuthorsList() => await GetAuthors();
        [RelayCommand]
        async Task UpdateBooksList() => await GetBooks();
        [RelayCommand]
        async Task ShowDetails(Book book) => await GotoDetailsPage(book);
        private async Task GotoDetailsPage(Book book)
        {
            IDictionary<string, Object> parameters =
                new Dictionary<string, Object>()
                {
                { "Book", book }
                };
            await Shell.Current
                .GoToAsync(nameof(BookPage), parameters);
        }
        [RelayCommand]
        private async Task AddAuthor()
        { 
            await GotoAddOrUpdatePage(nameof(AddAuthorPage),
                new AddAuthorCommand() { Author = new Author() });
        }
        [RelayCommand]
        private async Task UpdateAuthor()
        {
            if (SelectedAuthor is null)
                return;
            await GotoAddOrUpdatePage(nameof(AddAuthorPage),
                new UpdateAuthorCommand() { Author = SelectedAuthor });
        }
        [RelayCommand]
        private async Task AddBook()
        {
            if (SelectedAuthor is null)
                return;
            await GotoAddOrUpdatePage(nameof(AddBookPage),
                new AddBookCommand() { Book = new Book() });
        }
        private async Task GotoAddOrUpdatePage(string route, IRequest request)
        {
            IDictionary<string, object> parameters =
                new Dictionary<string, object>()
                {
                { "Request", request },
                { "Author", SelectedAuthor }
                };
            await Shell.Current
                .GoToAsync(route, parameters);
        }
        [RelayCommand]
        private async Task DeleteAuthor()
        {
            if (selectedAuthor is null)
                return;
            await DeleteAuthorAction();
        }
        private async Task DeleteAuthorAction()
        {
            await _mediator.Send(new DeleteAuthorCommand(SelectedAuthor));
            await GetAuthors();
        }
    }
}
