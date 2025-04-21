
using MAINPROJECT.AppDbContect;
using MAINPROJECT.AutoMapper;
using MAINPROJECT.DomaiLayer;
using MAINPROJECT.ExceptionHandling;
using MAINPROJECT.ExtensionMethods;
using MAINPROJECT.Servicelayer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Serilog;
using YourNamespace.ExtensionMethods;

namespace MAINPROJECT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(Aotomapping));
            builder.Services.AddScoped<IStudent, StudentRepo>();
            builder.Services.AddScoped<IEmployee, EmployeeRepo>();
            builder.Services.AddScoped<IDepartment, DepartmentRepo>();
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
            .WriteTo.Console()
             .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
              .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddControllers().AddOData(options =>
                    options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100)
                           .AddRouteComponents("odata", GetEdmModel()));
            builder.Services.AddSwaggerWithJwt();
            builder.Services.AddJwtToken(builder.Configuration);
            //builder.Services.AddExceptionHandler<GlobalException>();
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

           

            app.UseHttpsRedirection();
            

            app.UseAuthentication();

            app.UseAuthorization();

            //app.UseMiddleware<CustomMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapControllers();

            app.Run();

              static IEdmModel GetEdmModel()
            {
                var odataBuilder = new ODataConventionModelBuilder();
                odataBuilder.EntitySet<Employee>("Employees");
                odataBuilder.EntitySet<Student>("Students");
                return odataBuilder.GetEdmModel();
            }
        }
    }
}
