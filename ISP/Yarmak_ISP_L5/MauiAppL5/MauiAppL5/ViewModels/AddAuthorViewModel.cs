using _253503_Yarmak.Application.AuthorUseCases.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppL5.ViewModels
{
    public partial class AddAuthorViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IMediator _mediator;
        public AddAuthorViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [ObservableProperty]
        private IAddAuthorRequest request;
        [ObservableProperty]
        FileResult image;
        [RelayCommand]
        public async Task PickImage()
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                { DevicePlatform.Android, new[] { ".png" } },
                });
            PickOptions options = new()
            {
                PickerTitle = "Please select a png image",
                FileTypes = customFileType,
            };
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        Image = result;
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
            return;
        }
        [RelayCommand]
        async Task AddOrUpdateAuthor()
        {
            await _mediator.Send(Request);
            await Shell.Current.GoToAsync("..");
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Request = query["Request"] as IAddAuthorRequest;
        }
    }
}
