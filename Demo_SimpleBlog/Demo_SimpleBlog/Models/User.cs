using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Demo_SimpleBlog.Models
{
    public class User
    {
        private const int Workfactor = 13;

        public static void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", Workfactor);
        }

        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }

        public virtual void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, Workfactor);
        }

        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("users");

            // Set the Primary Key
            Id(usr => usr.Id, usr => usr.Generator(Generators.Identity));

            //  Map the other properties
            Property(usr => usr.Username, usr => usr.NotNullable(true));
            Property(usr => usr.Email, usr => usr.NotNullable(true));
            
            Property(usr => usr.PasswordHash, usr =>
            {
                usr.Column("password_hash");
                usr.NotNullable(true);
            });
        }
    }
}