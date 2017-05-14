using SQLite.Net;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phong_truyen_thong.Model
{
    
    public class Menu
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int IdParent { get; set; }
        public int Theme { get; set; }
        public string Name { get; set; }
        public string Background { get; set; }
    }
}
