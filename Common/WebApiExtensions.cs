using SimpleInjector;
using SimpleInjector.Lifestyles;
using Microsoft.Extensions.DependencyInjection;


namespace Common
{
    public static class WebApiExtensions
    {
        public static void ConfigureSimpleInjector(this IServiceCollection services, Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Options.DefaultLifestyle = Lifestyle.Scoped;

            // Ensures the container gets disposed
            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore().AddControllerActivation();
                options.AutoCrossWireFrameworkComponents = true;
            });
            container.Options.ResolveUnregisteredConcreteTypes = true;
#if DEBUG
            container.ResolveUnregisteredType += (s, e) =>
            {
                if (!e.Handled && !e.UnregisteredServiceType.IsAbstract)
                {
                    System.Console.Error.WriteLine($"UnregisteredServiceType: {e.UnregisteredServiceType}");
                }
            };
#endif
          
        }

    }
}