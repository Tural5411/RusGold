﻿using System;
using RusGold.Shared.Entities.Abstract;
using RusGold.Shared.Entities.Concrete;

namespace RusGold.Entities.Concrete
{
    public class Article:EntityBase,IEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ThumbNail { get; set; }
        public DateTime Date { get; set; }
        public int ViewsCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public string SeoAuthor { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTags { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
