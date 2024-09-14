using MauiAppL5.Pages;
using MauiAppL5.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppL5
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterPages(this IServiceCollection services)
        {
            services
                .AddTransient<AuthorPage>()
                .AddTransient<BookPage>()
                .AddTransient<AddBookPage>()
                .AddTransient<AddAuthorPage>();
            ;
            return services;
        }
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services
                .AddTransient<AuthorViewModel>()
                .AddTransient<BookViewModel>()
                .AddTransient<AddBookViewModel>()
                .AddTransient<AddAuthorViewModel>();
            return services;
        }
        public static IServiceCollection CreateImageFolders(this IServiceCollection services)
        {
            string imagesDir = System.IO.Path.Combine(FileSystem.AppDataDirectory, "Images");
            string workImagesDir = Path.Combine(FileSystem.AppDataDirectory, "Images", "Book");
            string brigadeImagesDir = Path.Combine(FileSystem.AppDataDirectory, "Images", "Author");
            System.IO.Directory.CreateDirectory(imagesDir);
            System.IO.Directory.CreateDirectory(workImagesDir);
            System.IO.Directory.CreateDirectory(brigadeImagesDir);
            return services;
        }
    }
}
