using Main;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Models;
using Repositories;
using Services;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

// USER INIT DEPENDANCY
builder.Services.AddScoped<InitializeUser>();


// -------------------- SERVICE DEPENDANCY

builder.Services.AddScoped<AuthService>();

// -------------------- REPOSITORY DEPENDANCY


builder.Services.AddScoped<AuthRepository>();



// -------------- AJOUT IDENTITY 

builder.Services.AddDbContext<BlogDBContext>();

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BlogDBContext>();


builder.Services.AddAuthorization();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerOptions =>
{
    swaggerOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogAPI", Version = "V1" });
    string? xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string? xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swaggerOptions.IncludeXmlComments(xmlPath);

    swaggerOptions.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    swaggerOptions.OperationFilter<SecurityRequirementsOperationFilter>();
});



var app = builder.Build();
// USER INIT 
using (var scope = app.Services.CreateAsyncScope())
{
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var context = scope.ServiceProvider.GetRequiredService<BlogDBContext>();
	await InitializeUser.adminInit(context, userManager, roleManager);
	await InitializeUser.UserInit(context, userManager, roleManager);// check InitializerUserClass
}



app.UseAuthorization();

app.MapIdentityApi<AppUser>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
