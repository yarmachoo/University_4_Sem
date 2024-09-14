using _253503_Yarmak.Domain.Abstractions;
using _253503_Yarmak.Persistense.Data;
using _253503_Yarmak.Persistense.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Yarmak.Persistense.UnitOfWork;
public class EFUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Lazy<IRepository<Author>> _authorRepository;
    private readonly Lazy<IRepository<Book>> _bookRepository;
    public EFUnitOfWork(AppDbContext context)
    {
        _context = context;
        _authorRepository = new Lazy<IRepository<Author>>(() =>
            new EFRepository<Author>(context));
        _bookRepository = new Lazy<IRepository<Book>>(() =>
            new EFRepository<Book>(context));
    }

    public IRepository<Author> AuthorRepository => _authorRepository.Value;

    public IRepository<Book> BookRepository => _bookRepository.Value;

    public async Task CreateDatabaseAsync() =>
        await _context.Database.EnsureCreatedAsync();
    public async Task RemoveDatabaseAsync() =>
        await _context.Database.EnsureDeletedAsync();
    public async Task SaveAllAsync() =>
        await _context.SaveChangesAsync();
}
