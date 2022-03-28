namespace MinimalAPI.Repositories
{
    public interface IBooksRepository
    {
        BooksDTO Add(BooksDTO entity);
        BooksDTO Update(BooksDTO entity);
        bool Delete(BooksDTO entity);
        Tuple<BooksDTO, bool> Get(int id);
        IEnumerable<BooksDTO> GetAll();
    }
}