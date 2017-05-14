using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phong_truyen_thong.Model
{
    
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdGroup { get; set; }
        public int TypePost { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Content { get; set; }
        public string Media { get; set; }
    }

}
