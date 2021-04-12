using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeKito.Models
{
    public static class Seogenerate
    {


        public static string SeoInitial(string seo)
        {
            string Seo = null;
            var seopage=Barayand.Common.Services.UtilesService.ParseSeoData(seo);
             Seo = 
                "<title>'"+ seopage.title + "'</title>" +
                "meta name='keywords' content='"+ seopage.keywords+ "'>" +
                "meta name='description' content='" + seopage.description+ "'>" +
                "<meta property = 'og:image' content = '"+ seopage.image+ "' />" +
                "<meta property = 'og:image:width' content='711'/>" +
                "<meta property = 'og:image:height' content = '309' />";
            return Seo;
        }

    }
}
