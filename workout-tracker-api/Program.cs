using Microsoft.EntityFrameworkCore;
using persistence.Context;
using repository.Implementation;
using repository.Interfaces;
using service.Implementation;
using service.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Add configuration for 'appsettings.json' file
builder.Configuration.AddConfiguration
(
	new ConfigurationBuilder()
		.AddJsonFile("appsettings.json")
		.Build()
);

//Register services in the DI container
ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder
	.Services
	.AddSwaggerGen
	(
		options =>
		{
			string? executableDirectory =
				Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			if (executableDirectory != null)
			{
				options
					.IncludeXmlComments
					(
						Path
							.Combine
							(
	
								executableDirectory,
								"workout-tracker-api.xml"
							),
						true
					);
			}
		}
	);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors
(
	builder =>
	{
		builder
			.WithOrigins
			(
				"http://localhost:3000",
				"http://192.168.1.159:3000",
				"http://192.168.1.161:3000"
			)
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowAnyOrigin();
	}
);

app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

app.Run();



void ConfigureServices
(
	ConfigurationManager configuration,
	IServiceCollection services
)
{
	//Configure the DBContext
	services
		.AddDbContext<WorkoutTrackerContext>
		(
			options =>
				options
					.UseSqlServer
					(
						configuration
							.GetConnectionString("WorkoutTrackerDatabase")
					)
		);

	//Configure Repositor(ies)
	services.AddScoped<IMuscleRepository, MuscleRepository>();
	services.AddScoped<IExerciseRepository, ExerciseRepository>();

	//Configure Business Logic Service(s)
	services.AddScoped<IMuscleService, MuscleService>();
	services.AddScoped<IExerciseService, ExerciseService>();

}