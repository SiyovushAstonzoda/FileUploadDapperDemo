using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<EmployeeService>();
builder.Services.AddSingleton<SalaryService>();
builder.Services.AddSingleton<DepartmentService>();
builder.Services.AddSingleton<DepartmentEmployeeService>();
builder.Services.AddSingleton<DepartmentManagerService>();
builder.Services.AddSingleton<ManagerService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();//for wwwroot images

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
