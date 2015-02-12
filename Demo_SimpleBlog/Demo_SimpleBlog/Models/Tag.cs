using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Demo_SimpleBlog.Models
{
    public class Tag
    {
        public virtual int Id { get; set; }

        public virtual string Slug { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Post> Posts { get; set; }
    }

    public class TagMap : ClassMapping<Tag>
    {
        public TagMap()
        {
            Table("tags");

            Id(tg => tg.Id, tg => tg.Generator(Generators.Identity));

            Property(tg => tg.Slug, tg => tg.NotNullable(true));

            Property(tg => tg.Name, tg => tg.NotNullable(true));

            Bag(tg => tg.Posts, tg =>
            {
              tg.Key(pt => pt.Column("tag_id"));
                tg.Table("post_tags");
            }, tg => tg.ManyToMany(pt => pt.Column("post_id")));
        }
    }
}