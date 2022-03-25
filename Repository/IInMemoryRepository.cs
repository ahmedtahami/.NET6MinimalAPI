public interface IInMemoryRepository<T>
{
    T Add(T entity);
    T Update(T entity);
    T Delete(T entity);
    T Get(int id);
    IEnumerable<T> GetAll();
}