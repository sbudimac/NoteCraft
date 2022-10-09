using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftMAUIBlazor.Helpers
{
    public static class Resolver
    {
        private static IServiceProvider serviceProvider;
        public static IServiceProvider ServiceProvider => serviceProvider ?? throw new Exception("Service provider has not been initialized");

        public static void RegisterServiceProvider(IServiceProvider sp)
        {
            serviceProvider = sp;
        }

        public static T Resolve<T>() where T : class
            => ServiceProvider.GetRequiredService<T>();

        public static void UseResolver(this IServiceProvider sp)
        {
            Resolver.RegisterServiceProvider(sp);
        }
    }
}
