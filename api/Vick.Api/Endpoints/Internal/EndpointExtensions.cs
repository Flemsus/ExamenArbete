
namespace Vick.Api.Endpoints.Internal
{
    public static class EndpointExtensions
    {
        public static void UseEndpoints<T>(this IApplicationBuilder app)
        {
            UseEndpoints(app, typeof(T));
        }

        private static void UseEndpoints(IApplicationBuilder app, Type T)
        {
            var endpointTypes = T.Assembly.DefinedTypes
                .Where(x => !x.IsAbstract && !x.IsInterface && typeof(IEndpoints)
                .IsAssignableFrom(x));

            foreach (var endpointType in endpointTypes)
            {
                endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!
                    .Invoke(null, new object[] { app });
            }
        }
    }
}
