using System.Collections.Generic;

namespace Blog.WebApi
{
    public class Blog {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<BlogPost> BlogPosts { get; set; }
    }
}