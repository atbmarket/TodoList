using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList;


namespace TodoListWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            { 
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Todo List", Version = "v1" });
            });

            var container = CreateContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        public static ContainerBuilder CreateContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            // here we register all interfaces and all types
            containerBuilder.RegisterAssemblyTypes(
                typeof(Startup).Assembly,
                typeof(IMapper<,>).Assembly)
                .AsImplementedInterfaces()
                .AsSelf();

            // we need to re-register repository, since it should be a singletone
            containerBuilder
                .RegisterType<TodoRepository>()
                .As<ITodoRepository>()
                .SingleInstance();

            return containerBuilder;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // we don't need to setup ASP.NET DI because we use autofac anyway

            //services.AddTransient<ITodoTask, TodoTask>();
            //services.AddSingleton<ITodoRepository, TodoRepository>();
            //services.AddSingleton<INotifier<ITodoTask>, NullTaskNotifier>();
            //services.AddTransient<TaskManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
