using System.Reflection;
using GoVisit.Settings;
using GoVisit.Infrastructure;
using GoVisit.Middleware;


var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
var mongoSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

builder.Services.AddSingleton<IMongoClientFactory>(_ => new MongoClientFactory(mongoSettings.ConnectionString));
builder.Services.AddScoped<IAppointmentRepository, MongoAppointmentRepository>();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.UseGlobalExceptionHandling();
app.MapControllers();

app.Run();
