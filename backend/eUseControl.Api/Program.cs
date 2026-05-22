var builder = WebApplication.CreateBuilder(args);

eUseControl.DataAccess.DbSession.ConnectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// (optional, dar ok să fie)
app.UseAuthorization();

app.MapControllers();

app.Run();