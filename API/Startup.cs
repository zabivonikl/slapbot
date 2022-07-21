using API.Extensions;

namespace API;

public class Startup
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTelegramSingleton(Secrets.Telegram.ApiKey);
        serviceCollection.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();  
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}