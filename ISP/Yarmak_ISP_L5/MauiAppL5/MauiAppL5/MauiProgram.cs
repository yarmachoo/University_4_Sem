using _253503_Yarmak.Application;
using _253503_Yarmak.Application.BookUseCases;
using _253503_Yarmak.Persistense;
using _253503_Yarmak.Persistense.Data;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MauiAppL5
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            string settingStream = "MauiAppL5/appsettings.json";
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FontAwesome.ttf", "FontAwesome");
                });
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit();

            var connStr = " Data Source = {0}sqlite.db";
            string dataDirectory = FileSystem.Current.AppDataDirectory + "/";
            connStr = String.Format(connStr, dataDirectory);
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connStr)
                .Options;

            builder.Services
                .AddApplication()
                .AddPersistense(options)
                .RegisterPages()
                .RegisterViewModels()
                .CreateImageFolders();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            DbInitializer
            .Initialize(builder.Services.BuildServiceProvider())
            .Wait();

            return builder.Build();
        }
    }
}
