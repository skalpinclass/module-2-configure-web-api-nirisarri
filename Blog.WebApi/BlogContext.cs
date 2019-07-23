using Microsoft.EntityFrameworkCore;

namespace Blog.WebApi
{
    public class BlogContext: DbContext 
    {
        public DbSet<Blog> Blogs { get; set; }

        public BlogContext(DbContextOptions options):base(options)
        {
            
        }
    }
}