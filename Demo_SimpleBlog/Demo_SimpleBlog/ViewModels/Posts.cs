using Demo_SimpleBlog.Infrastructure;
using Demo_SimpleBlog.Models;

namespace Demo_SimpleBlog.ViewModels
{
    public class PostsIndex
    {
        public PageData<Post> Posts;
    }

    public class PostsShow
    {
        public Post Post { get; set; }
    }

    public class PostsTag
    {
        public Tag Tag { get; set; }

        public PageData<Post> Posts { get; set; } 
    }
}