using System.Collections.Generic;   
using System.ComponentModel.DataAnnotations;
using Demo_SimpleBlog.Models;

namespace Demo_SimpleBlog.Areas.Admin.ViewModels
{
    public class RoleCheckBox
    {
        public int Id { get; set; }
        
        public bool IsChecked { get; set; }

        public string Name { get; set; }
    }

    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }

    public class UsersNew
    {
        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(256)]
        public string Email { get; set; }

        public IList<RoleCheckBox> Roles { get; set; }

    }

    public class UsersEdit
    {
        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(256)]
        public string Email { get; set; }

        public IList<RoleCheckBox> Roles { get; set; }
    }

    public class UsersResetPassword
    {
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}