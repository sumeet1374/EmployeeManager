using Common.Data;
using Common.Services;
using Employee.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Employee.Api
{
    public class Startup
    {
        private const string CORS_POLICY = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                        options.Filters.Add(new ErrorFilter())).AddNewtonsoftJson();

            services.AddSingleton(typeof(IUnitOfWorkFactory),new EmployeeUnitOfWorkFactory(Configuration[AppConstants.CONNECTION_STRING]));
            services.AddSingleton(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<IEmployeeService, EmployeeService>();

            // Register the Swagger services
            services.AddSwaggerDocument();

            // Coors for development it is for all
            services.AddCors(options => options.AddPolicy(CORS_POLICY, policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();

            }));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "employee-app";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(CORS_POLICY);
            app.UseDeveloperExceptionPage();
            //if (env.IsDevelopment())
            //{
               
            //}



            app.UseHttpsRedirection();

            // Important for SPA
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();



            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(sp=>
            {
               
            });

           

            //app.UseSpa((spa)=>
            //{
            //    spa.Options.SourcePath = "employee-app";
            //});

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
