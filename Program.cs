using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using StudentInformation.Extentions;
using StudentInformation.Mapping;
using StudentInformation.Repository;
using StudentInformation.Services.Interface;
using StudentInformation.Services;


var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// NHibernate session factory setup
builder.Services.AddHttpClient();
string srConnectionString = configuration.GetConnectionString("ConnectionString") ?? "";
builder.Services.AddNhibernate<StudentMap>(srConnectionString);


// Register services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped(typeof(IStudentRepository<>), typeof(StudentRepository<>));
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

var app = builder.Build();

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
