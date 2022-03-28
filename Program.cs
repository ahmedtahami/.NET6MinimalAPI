// Couldn't see usings? Check out the obj/Debug/net6.0 folder to see the 
// hidden auto-generated file — [ProjectName].GlobalUsings.g.cs. You can define a separate class to keep all 
// your using statements in one place.

global using MinimalAPI.Endpoints;
global using MinimalAPI.Repositories;
global using MinimalAPI.DTO;
global using MinimalAPI.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MinimalAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { }}
    });
});

builder.Services.AddSingleton<IBooksRepository, BooksRepository>();
builder.Services.AddSingleton<TokenService>(new TokenService());
builder.Services.AddSingleton<IUserRepositoryService>(new UserRepositoryService());


builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

await using var app = builder.Build();

app.UseAuthentication(); 
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.ConfigureBooksAPI();
app.ConfigureDefaultAPI();

app.MapPost("/login", [AllowAnonymous] async ([FromBodyAttribute] UserModel userModel, TokenService tokenService, IUserRepositoryService userRepositoryService, HttpResponse response) => {
    var userDto = userRepositoryService.GetUser(userModel);
    if (userDto == null)
    { 
        response.StatusCode = 401; 
        return "Authorization Failed"; 
    }
    var token = tokenService.BuildToken(builder.Configuration["Jwt:Key"], builder.Configuration["Jwt:Issuer"], builder.Configuration["Jwt:Audience"], userDto);
    return token;
}).Produces(StatusCodes.Status200OK).WithName("Login").WithTags("Accounts");

app.MapGet("/AuthorizedResource", (Func<string>)(
    [Authorize] () => "Action Succeeded")
).Produces(StatusCodes.Status200OK)
.WithName("Authorized").WithTags("Accounts").RequireAuthorization();

// app.MapGet("books", () =>{
//     return BooksRepository.GetAll();
// }).Produces<IEnumerable<BooksDTO>>(StatusCodes.Status200OK).WithTags("Books");

// app.MapPost("books", (BooksDTO model) =>{
//     return BooksRepository.Add(model);
// }).Accepts<BooksDTO>("application/json").Produces<BooksDTO>(StatusCodes.Status201Created).WithTags("Books");

// app.MapGet("book", (int id) =>{
//     return BooksRepository.Get(id);
// }).Produces<BooksDTO>(StatusCodes.Status200OK).WithTags("Books");
app.Run();