using _253503_Yarmak.Application.AuthorUseCases.Queries;
using _253503_Yarmak.Application.BookUseCases.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppL5.ViewModels
{
    public partial class AddBookViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IMediator _mediator;
        public AddBookViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [ObservableProperty]
        string errText;
        Author _author;
        [ObservableProperty]
        Author author = new();
        [ObservableProperty]
        FileResult image;
        [ObservableProperty]
        IAddOrUpdateBookRequest request;
        public ObservableCollection<Author> Authors { get; set; } = new();
        [RelayCommand]
        public async void PickImage()
        {
            PickOptions options = new()
            {
                PickerTitle = "Please select a png image",
                FileTypes = FilePickerFileType.Images
            };
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    Image = result;
                }
            }
            catch (Exception ex)
            {
                return;
            }
            return;
        }
        [RelayCommand]
        async Task AddOrUpdateBook()
        {
            await Task.Delay(1);
            if (request.Book.Name is null || Request.Book.Name == string.Empty || Request.Book.Description is null ||
                Request.Book.Description == string.Empty)
            {
                return;
            }
            Request.Book.Author = Author;
            await _mediator.Send(Request);
            if (Image != null)
            {
                using var stream = await Image.OpenReadAsync();
                var image = ImageSource.FromStream(() => stream);
                string filename = Path.Combine(FileSystem.AppDataDirectory,
                    "Images", "Songs", $"{Request.Book.Id}.png");
                using var fileStream = File.Create(filename);
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
                stream.Seek(0, SeekOrigin.Begin);
            }
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        async Task UpdateAuthorsList() => await GetAuthors();
        public async Task GetAuthors()
        {
            var authors = await _mediator.Send(new GetAuthorsQuery());
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Authors.Clear();
                foreach (var author in authors)
                    Authors.Add(author);
            });
            Author = _author;
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Request = query["Request"] as IAddOrUpdateBookRequest;
            _author = query["Author"] as Author;
        }
    }
}
