using Microsoft.EntityFrameworkCore;
using PEP;
using PEP.Data;
using PEP.Mappings;
using PEP.Repositories.Implement;
using PEP.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FinalDesignContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("PEPString")));
builder.Services.AddScoped<ICourseRepository, ImpCourseRepository>();
builder.Services.AddScoped<IUserRepository, ImpUserRepository>();
builder.Services.AddScoped<IProblemRepository, ImpProblemRepository>();
builder.Services.AddScoped<IPostRepository, ImpPostRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
var app = builder.Build();

// Enable CORS
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
