public class BooksDTO : IDisposable
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Description { get; set; }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}