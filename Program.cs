// Couldn't see usings? Check out the obj/Debug/net6.0 folder to see the 
// hidden auto-generated file — [ProjectName].GlobalUsings.g.cs. You can define a separate class to keep all 
// your using statements in one place.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IInMemoryRepository<BooksDTO>, BooksRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

var BooksRepository = new BooksRepository();

app.MapGet("books", () =>{
    return BooksRepository.GetAll();
}).Produces<IEnumerable<BooksDTO>>(StatusCodes.Status200OK).WithTags("Books");

app.MapPost("books", (BooksDTO model) =>{
    return BooksRepository.Add(model);
}).Accepts<BooksDTO>("application/json").Produces<BooksDTO>(StatusCodes.Status201Created).WithTags("Books");

app.MapGet("book", (int id) =>{
    return BooksRepository.Get(id);
}).Produces<BooksDTO>(StatusCodes.Status200OK).WithTags("Books");


app.Urls.Add("http://localhost:3000"); //If you want to run the app on a different port.
app.Urls.Add("http://localhost:5001"); //You can also run the app on multiple ports.
app.Run();