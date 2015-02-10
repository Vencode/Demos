using System.Collections.Generic;
using Demo_SimpleBlog.Models;

namespace Demo_SimpleBlog.Areas.Admin.ViewModels
{
    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }
}