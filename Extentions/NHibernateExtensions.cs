using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using StudentInformation.Helper;

namespace StudentInformation.Extentions
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNhibernate<T>(this IServiceCollection services, string connectionString) where T : IMappingProvider, new()
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    var configureSessionFactory = BuildSessionFactory<T>(connectionString);
                    services.AddSingleton(configureSessionFactory);
                    services.AddScoped(_ => configureSessionFactory.OpenSession());
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static ISessionFactory BuildSessionFactory<T>(this string connectionString)
        {
            ISessionFactory sessionFactory = null;
            var pauseBetweenFailures = TimeSpan.FromSeconds(5);
            RetryHelper.RetryOnException(10, pauseBetweenFailures, () => sessionFactory = CreateSession<T>(connectionString));
            return sessionFactory;
        }

        /// <summary>
        /// Will create an ORM session
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static ISessionFactory CreateSession<T>(string connectionString)
        {

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                  .Mappings(m =>
                  {
                      m.FluentMappings.AddFromAssemblyOf<T>();
                  })
                  .BuildConfiguration()
                  .BuildSessionFactory();
        }
    }
}