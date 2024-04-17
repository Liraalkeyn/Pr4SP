using Pr4SP.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Pr4SP.xml"));
    }); // добавление xml файла
builder.Services.AddDbContextFactory<Pr4SpContext>();
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();


var app = builder.Build();

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
