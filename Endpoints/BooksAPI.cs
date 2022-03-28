namespace MinimalAPI.Endpoints
{
    public static class BooksAPI
    {
        public static void ConfigureBooksAPI(this WebApplication app)
        {
            var booksRepository = app.Services.GetService<IBooksRepository>();
            app.MapGet("books", () =>
            {
                GetAllBooks(booksRepository!);
            }).Produces<IEnumerable<BooksDTO>>(StatusCodes.Status200OK).WithTags("Books");
        }
        private static IResult GetAllBooks(IBooksRepository booksRepository)
        {
            try
            {
                var books = booksRepository.GetAll();
                return Results.Ok(books);
            }
            catch (System.Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}