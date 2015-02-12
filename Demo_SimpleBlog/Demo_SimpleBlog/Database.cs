using System.Web;
using Demo_SimpleBlog.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace Demo_SimpleBlog
{
    public static class Database
    {
        private const string SessionKey = "Demo_SimpleBlog.Database.SessionKey";
        private static ISessionFactory _sessionFactory;

        public static ISession Session
        {
            get { return (ISession) HttpContext.Current.Items[SessionKey]; }
        }

        public static void Configure()
        {
            var config = new Configuration();

            //Configure the Connection String
            config.Configure();

            //Add Our Mappings
            var mapper = new ModelMapper();
            mapper.AddMapping<UserMap>();
            mapper.AddMapping<RoleMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            //Create the session factory
            _sessionFactory = config.BuildSessionFactory();
        }

        public static void OpenSession()
        {
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;

            if (session != null)
                session.Close();

            HttpContext.Current.Items.Remove(SessionKey);
        }
    }
}