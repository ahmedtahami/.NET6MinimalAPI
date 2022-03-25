public interface IInMemoryRepository<T>
{
    T Add(T entity);
    T Update(T entity);
    bool Delete(T entity);
    Tuple<T, bool> Get(int id);
    IEnumerable<T> GetAll();
}