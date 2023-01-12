using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace System.WebApi.Extensions
{
    public static class HostDatabaseExtension
    {
        public static IHost InitDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            //var context = services.GetRequiredService<AplicationDataContext>();

            // now we have the DbContext. Run migrations
            //Log.Warning("Applying migrations");
            //context.Database.Migrate();
            //Log.Warning("Applying migrations done");

            // -------------
            //  Seeder here
            // -------------
            Log.Warning("Applying Seeads");

            //StatusSeeder.SeeadData(context);
            //LanguageSeeder.SeeadData(context);
            //ModalitySeeder.SeeadData(context);
            //ProvinceSeeder.SeeadData(context);
            //TandaSeeder.SeeadData(context);
            //UniversitySeeder.SeeadData(context);
            //CareerSeeder.SeeadData(context);
            //RolSeeder.SeeadData(context);
            //PermissionSeeder.SeeadData(context);
            //ModulesSeeder.SeeadData(context);
            //TypeAnswerSeeder.SeeadData(context);

            Log.Warning("Applying Seeads done");
            return host;
        }
    }
}
