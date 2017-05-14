using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phong_truyen_thong.Model
{
    class StaticVariable
    {
        public static List<Menu> listMenu = new List<Menu>();
        public static List<Post> listPost = new List<Post>();
        public static int POST_VIDEO = 1;
        public static int POST_IMAGES = 0;
        public static int SUB_MENU = 0;
        public static int THEME1 = 1;
        public static int THEME2 = 2;
        public static int EditMode = 1;
    }
}
