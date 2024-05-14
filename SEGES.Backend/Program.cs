using Microsoft.EntityFrameworkCore;
using SEGES.Backend.Repositories.Implementations;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Backend.UnitsOfWork.Implementations;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Backend;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SIEGESConnection"));
});

builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<ICountriesUnitOfWork, CountriesUnitOfWork>();

builder.Services.AddScoped<IStateRepository, StatesRepository>();
builder.Services.AddScoped<IStateUnitOfWork, StatesUnitOfWork>();

builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();
builder.Services.AddScoped<ICitiesUnitOfWork, CitiesUnitOfWork>();

builder.Services.AddScoped<IDocTraceabilityTypesRepository, DocTraceabilityTypesRepository>();
builder.Services.AddScoped<IDocTraceabilityTypesUnitOfWork, DocTraceabilityTypesUnitOfWork>();

builder.Services.AddScoped<IHuAppruvalStatusRepository, HuAppruvalStatusRepository>();
builder.Services.AddScoped<IHuApprovalStatusUnitOfWork, HuApprovalStatusUnitOfWork>();

builder.Services.AddScoped<IHUPrioritiesRepository, HUPrioritiesRepository>();
builder.Services.AddScoped<IHUPrioritiesUnitOfWork, HUPrioritiesUnitOfWork>();

builder.Services.AddScoped<IHUStatusRepository, HUStatusRepository>();
builder.Services.AddScoped<IHUStatusUnitOfWork, HUStatusUnitOfWork>();

builder.Services.AddScoped<IProjectStatusesRepository, ProjectStatusesRepository>();
builder.Services.AddScoped<IProjectStatusesUnitOfWork, ProjectStatusesUnitOfWork>();

builder.Services.AddTransient<SeedDb>();

var app = builder.Build();
app.UseCors(x => x
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(o => true)
.AllowCredentials());

SeedData(app);

void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}
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