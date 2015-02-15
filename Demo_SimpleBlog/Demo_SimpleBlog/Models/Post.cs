using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Demo_SimpleBlog.Models
{
    public class Post
    {
        public virtual int Id { get; set; }

        public virtual User User { get; set; }

        public virtual string Title { get; set; }

        public virtual string Slug { get; set; }

        public virtual string Content { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime? UpdatedAt { get; set; }

        public virtual DateTime? DeletedAt { get; set; }

        public virtual bool IsDeleted { get { return DeletedAt != null; } }

        public virtual IList<Tag> Tags { get; set; }

        public Post()
        {
            Tags = new List<Tag>();
        }
    }

    public class PostMap : ClassMapping<Post>
    {
        public PostMap()
        {
            Table("posts");

            Id(pt => pt.Id, pt => pt.Generator(Generators.Identity));

            ManyToOne(usr => usr.User, pt =>
            {
                pt.Column("user_id");
                pt.NotNullable(true);
            });

            Property(pt => pt.Title, pt => pt.NotNullable(true));
            Property(pt => pt.Slug, pt => pt.NotNullable(true));
            Property(pt => pt.Content, pt => pt.NotNullable(true));

            Property(pt => pt.CreatedAt, pt =>
            {
                pt.NotNullable(true);
                pt.Column("created_at");
            });
            
            Property(pt => pt.UpdatedAt, pt => pt.Column("updated_at"));
            Property(pt => pt.DeletedAt, pt => pt.Column("deleted_at"));


            Bag(tg => tg.Tags, tg =>
            {
                tg.Key(pt => pt.Column("post_id"));
                tg.Table("post_tags");
            }, tg => tg.ManyToMany(pt => pt.Column("tag_id")));
        }
        
    }
}