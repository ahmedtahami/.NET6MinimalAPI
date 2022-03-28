namespace MinimalAPI.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly List<BooksDTO> booksDepot = new()
        {
            new BooksDTO { Id = 1, Author = "ABC", Title = "Book 1", Description = "Description 1" },
            new BooksDTO { Id = 2, Author = "ABC", Title = "Book 2", Description = "Description 2" },
            new BooksDTO { Id = 3, Author = "ABC", Title = "Book 3", Description = "Description 3" },
            new BooksDTO { Id = 4, Author = "ABC", Title = "Book 4", Description = "Description 4" },
            new BooksDTO { Id = 5, Author = "ABC", Title = "Book 5", Description = "Description 5" },
        };

        public BooksDTO Add(BooksDTO entity)
        {
            booksDepot.Add(entity);
            return entity;
        }
        public bool Delete(BooksDTO entity) => booksDepot.Remove(entity) ? true : false;


        public Tuple<BooksDTO, Boolean> Get(int id)
        {
            try
            {
                return new Tuple<BooksDTO, Boolean>(booksDepot.First(x => x.Id == id), true);
            }
            catch (Exception)
            {
                return new Tuple<BooksDTO, Boolean>(new BooksDTO(), false);
            }
        }

        public IEnumerable<BooksDTO> GetAll() => booksDepot;

        public BooksDTO Update(BooksDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}