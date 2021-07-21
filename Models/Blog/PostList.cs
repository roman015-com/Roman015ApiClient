using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roman015API.Models.Blog
{
    public class PostList
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalPosts { get; set; }
        public List<Post> posts { get; set; }
    }
}
