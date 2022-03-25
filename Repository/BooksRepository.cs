public class BooksRepository : IInMemoryRepository<BooksDTO>
{
    private readonly List<BooksDTO> booksDepot = new(){
        new BooksDTO{Id = 1, Author = "ABC", Title = "Book 1", Description = "Description 1"},
        new BooksDTO{Id = 2, Author = "ABC", Title = "Book 2", Description = "Description 2"},
        new BooksDTO{Id = 3, Author = "ABC", Title = "Book 3", Description = "Description 3"},
        new BooksDTO{Id = 4, Author = "ABC", Title = "Book 4", Description = "Description 4"},
        new BooksDTO{Id = 5, Author = "ABC", Title = "Book 5", Description = "Description 5"},
    };
        
    public BooksDTO Add(BooksDTO entity)
    {
        booksDepot.Add(entity);
        return entity;
    }
    public BooksDTO Delete(BooksDTO entity) => booksDepot.Remove(entity) ? entity : null;
    

    public BooksDTO Get(int id) => booksDepot.FirstOrDefault(x => x.Id == id);

    public IEnumerable<BooksDTO> GetAll() => booksDepot;

    public BooksDTO Update(BooksDTO entity)
    {
        throw new NotImplementedException();
    }
}