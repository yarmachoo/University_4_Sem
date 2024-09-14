using _253503_Yarmak.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Domain.Abstractions;
public interface IUnitOfWork
{
    IRepository<Author> AuthorRepository { get; }
    IRepository<Book> BookRepository { get; }
    public Task RemoveDatabaseAsync();
    public Task CreateDatabaseAsync();
    public Task SaveAllAsync();
}
