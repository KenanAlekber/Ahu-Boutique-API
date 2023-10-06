using Ahu.API.Extension;
using Ahu.Business;
using Ahu.DataAccess;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAutoMapper(option =>
//{
//    option.AddProfile(new ProductMapper());
//});

builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddExcepitonHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();