using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Demo_SimpleBlog.Models
{
    public class Role
    {
        public virtual int Id { get; set; }
       
        public virtual string Name { get; set; }
    }

    public class RoleMap : ClassMapping<Role>
    {
        public RoleMap()
        {
            Table("roles");

            Id(rl => rl.Id, rl => rl.Generator(Generators.Identity));

            Property(rl => rl.Name, rl => rl.NotNullable(true));
        }
    }
}