using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using clothesWebSite.Models;

namespace clothesWebSite.DAO
{
    public class PostDAO
    {

        private MyDBContext db;
        public PostDAO()
        {
            db = new MyDBContext();
        }


        //lay ra num san pham giam theo ngay
        public IEnumerable<Post> getNewPost(int num)
        {
            IEnumerable<Post> listProduct = db.Posts.Where(m => m.state == 1).OrderByDescending(m => m.last_update).Take(num);
            return listProduct;
        }
    }
}