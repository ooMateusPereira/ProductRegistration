using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductRegistration.Database;
using ProductRegistration.Services;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source = Products.db"));

var jwtconfig = builder.Configuration.GetSection("Jwt");

var key = Encoding.ASCII.GetBytes(jwtconfig["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtconfig["Issuer"],
        ValidAudience = jwtconfig["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    }; 
});

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

var products = new List<Product>()
{
    new Product() {Id = 1, Name = "mouse", Price = 99.99m, Stock = 50,},
    new Product() {Id = 2, Name = "keyboard", Price = 249.90m, Stock = 30,}
};

app.MapGet("/Products", () => 
{
    return products;

});

app.MapGet("/Products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(x => x.Id == id);

    return product is not null
        ? Results.Ok(product)
        : Results.NotFound($"Product {id} not found.");

});

app.MapPost("/Product", (Product newProduct) =>
{
    products.Add(newProduct);

    return Results.Created();
});

app.MapPut("/Product{id}", (int id, Product attProduct) =>
{
    var product = products.FirstOrDefault(x => x.Id == id);

    if (product is null)
    {
        return Results.NotFound($"Product {id} not found.");
    }

    product.Name = attProduct.Name;
    product.Price = attProduct.Price;
    product.Stock = attProduct.Stock;

    return Results.Ok(product);

});

app.MapDelete("/Product/{id}", (int id) =>
{
    var product = products.FirstOrDefault(x => x.Id == id);

    if (product is null)
    {
        return Results.NotFound($"Product {id} not found.");
    }

    products.Remove(product);

    return Results.NoContent();
});

app.Run();

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
