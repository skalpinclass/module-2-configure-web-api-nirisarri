using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BlogContext _db;

        public BlogController(BlogContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<Blog>> Get()
        {
            var blogs = _db.Blogs;
            return blogs.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ActionResult<Blog> GetById(int id)
        {
            var blog = _db.Blogs.Single(x => x.BlogId == id);
            _db.Entry(blog)
                .Collection(b => b.BlogPosts)
                .Load();

            return blog;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public void Post([FromBody] Blog value)
        {
            _db.Blogs.Add(value);
            _db.SaveChanges();
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public void Put(int id, [FromBody] Blog value)
        {
            var updatedBlog = _db.Blogs.Find(id);
            if (updatedBlog != null)
            {
                updatedBlog.BlogPosts = value.BlogPosts;
                updatedBlog.Rating = value.Rating;
                updatedBlog.Url = value.Url;
                _db.Blogs.Update(updatedBlog);
                _db.SaveChanges();
            }
            else
            {
                // return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public void Delete(int id)
        {
            var blogToRemove = _db.Blogs.Find(id);
            if (blogToRemove != null)
            {
                _db.Blogs.Remove(blogToRemove);
                _db.SaveChanges();
            }
        }
        
    }
}