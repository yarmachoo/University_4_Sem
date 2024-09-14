using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Persistense.Repository;
public class FakeAuthorRepository : IRepository<Author>
{
    private readonly List<Author> _authors;
    public FakeAuthorRepository()
    {
        _authors = new List<Author>()
        {
            new Author()
            {
                Name = "Ivan",
                Description = "Romantic",
                Books = new List<Book>()
            },
            new Author()
            {
                Name = "Katya",
                Description = "Science pop",
                Books = new List<Book>()
            }
        };
    }
    public Task AddAsync(Author entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Author entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Author> FirstOrDefaultAsync(Expression<Func<Author, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Author> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Author, object>>[]? includesProperties)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Author>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.Run(() => _authors as IReadOnlyList<Author>);
    }

    public Task<IReadOnlyList<Author>> ListAsync(Expression<Func<Author, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<Author, object>>[] includesProperties)
    {
        return Task.Run(() => _authors as IReadOnlyList<Author>);
    }

    public Task UpdateAsync(Author entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
