using Application.Command;
using Application.Command.Validation;
using Application.Query;
using Card2CardApi.Service;
using Card2CardApi.Service.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Card2CardApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblies(new[] { typeof(BaseCommandValidator<>).Assembly }));
            services.AddFluentValidation();
            services.AddTransient<IValidator<Card2CardCommand>, Card2CardRequestCommandValidator>();
            RegisterMediatorService(services);
            services.RegisterProviders(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Card2CardApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Card2CardApi v1"));
            }
            app.UseMiddleware<ApiExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterMediatorService(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(BaseCommandHandler<,>)), Assembly.GetAssembly(typeof(BaseQueryHandler<,>)));
        }
    }
}
