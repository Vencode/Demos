using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Demo_SimpleBlog.Infrastructure;
using Demo_SimpleBlog.Models;

namespace Demo_SimpleBlog.Areas.Admin.ViewModels
{
    public class PostsIndex
    {
        public PageData<Post> Posts { get; set; }
    }
}