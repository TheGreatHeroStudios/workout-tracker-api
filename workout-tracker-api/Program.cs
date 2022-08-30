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

// Enable PNA preflight requests
/*app.Use
(
	async (ctx, next) =>
	{
		if 
		(
			ctx.Request.Method.Equals("options", StringComparison.InvariantCultureIgnoreCase) && 
			ctx.Request.Headers.ContainsKey("Access-Control-Request-Private-Network")
		)
		{
			ctx.Response.Headers.Add("Access-Control-Allow-Private-Network", "true");
		}

		await next();
	});*/

app.UseCors
(
	builder =>
	{
		builder
			.WithOrigins
			(
				"http://localhost:4002",
				"http://192.168.1.159:4002",
				"http://192.168.1.161:4002"
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