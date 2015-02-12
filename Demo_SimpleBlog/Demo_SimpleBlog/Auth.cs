using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Demo_SimpleBlog.Models;
using NHibernate.Linq;

namespace Demo_SimpleBlog
{
    public static class Auth
    {
        private const string UserKey = "Demo_SimpleBlog.Auth.UserKey";

        public static User User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as User;

                if (user == null)
                {
                    user = Database.Session.Query<User>().FirstOrDefault(usr => usr.Username == HttpContext.Current.User.Identity.Name);

                    if(user == null)
                        return null;

                    HttpContext.Current.Items[UserKey] = user;
                }

                return user;
            }
        }
    }
}