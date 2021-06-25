using clothesWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clothesWebSite.DAO
{
    public class BannerDAO
    {
        private MyDBContext db;
        public BannerDAO()
        {
            db = new MyDBContext();
        }

        //Lay slider phia tren
        public IEnumerable<Banner> getBannerTop()
        {
            IEnumerable<Banner> list = db.Banners.Where(m => m.state == Banner.VISIBLE_TOP);
            return list;
        }
        //lay slider phia duoi muc inspired 
        public IEnumerable<Banner> getBannerBottom()
        {
            IEnumerable<Banner> list = db.Banners.Where(m => m.state == Banner.VISIBLE_BOTTOM);
            return list;
        }
    }
}