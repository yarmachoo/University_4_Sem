using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Persistense.Repository;
public class FakeBookRepository : IRepository<Book>
{
    private readonly List<Book> _books;
    public FakeBookRepository()
    {
        _books = new List<Book>();
        for (int i = 1; i <= 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                _books.Add(new Book
                {
                    Name = $"Work {j}",
                    Id = (i + 1) * 2 + j + 2,
                    Description = "some works",
                    AuthorId = i,
                    Rate = i * 2 - 1
                });
            }
        }
    }
    public Task AddAsync(Book entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Book entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Book> FirstOrDefaultAsync(Expression<Func<Book, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Book> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Book, object>>[]? includesProperties)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Book>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.Run(() => _books as IReadOnlyList<Book>);
    }

    public Task<IReadOnlyList<Book>> ListAsync(Expression<Func<Book, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<Book, object>>[] includesProperties)
    {
        return Task.Run(() => _books as IReadOnlyList<Book>);
    }

    public Task UpdateAsync(Book entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
