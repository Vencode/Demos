using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_SimpleBlog.ViewModels
{
    public class SidebarTag
    {
        public int Id { get; private set; }
        
        public string Name { get; private set; }

        public string Slug { get; private set; }

        public int PostCount { get; private set; }

        public SidebarTag(int id, string name, string slug, int postCount)
        {
            Id = id;
            Name = name;
            Slug = slug;
            PostCount = postCount;
        }
    }

    public class LayoutSideBar
    {
        public bool IsLoggedIn { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<SidebarTag> Tags { get; set; }
    }
}