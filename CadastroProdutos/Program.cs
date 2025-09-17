var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
