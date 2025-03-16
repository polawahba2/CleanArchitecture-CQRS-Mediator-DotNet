using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.Authentication;
using Infrastructure.Authentication.services;
using Infrastructure.Configurations;
using Infrastructure.Identity;
using Infrastructure.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<ApplicationDBContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("defaultMacCon")));
        builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
        builder.Services.AddControllers();
        builder.Services.AddScoped<IAuthServices, AuthServices>();
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();
        builder.Services.AddAutoMapper(typeof(IdentityProfile));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // builder.Services.AddMediatR(
        //     config => config.RegisterServicesFromAssembly(typeof(Program).Assembly)
        // );


        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDBContext>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();


            try
            {
                await context.Database.MigrateAsync();
                await ApplicationDBContextSeed.SeedRolesAsync(roleManager);
                await ApplicationDBContextSeed.SeedUserAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");
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
    }
}