namespace API;

public class Startup
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        var token = Configuration.
        serviceCollection.AddControllers();
        serviceCollection.AddEndpointsApiExplorer();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();
    }
}