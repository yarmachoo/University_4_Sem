using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _253503_Yarmak.Application.BookUseCases;

public class DbInitializer
{
    public static async Task Initialize(IServiceProvider services)
    {
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();
        await unitOfWork.RemoveDatabaseAsync();
        await unitOfWork.CreateDatabaseAsync();
        IReadOnlyList<Author> authors = new List<Author>()
        {
            new (){Name="Eugene", Description="romantic"},
            new (){Name="Veronika", Description="scientific"},
            new (){Name="Kseniya", Description="future"}
        };
        foreach (var author in authors)
            await unitOfWork.AuthorRepository.AddAsync(author);
        await unitOfWork.SaveAllAsync();
        var counter = 1;
        foreach (var author in authors)
        {
            for (int j = 0; j < 5; j++)
            {
                var random = new Random();
                int rate = random.Next(1, 11);
                await unitOfWork.BookRepository.AddAsync(new()
                {
                    Author = author,
                    Name = "Name",
                    Description = "Description",
                    Rate = rate
                });
                counter++;
            }
        }
        await unitOfWork.SaveAllAsync();
    }
}
