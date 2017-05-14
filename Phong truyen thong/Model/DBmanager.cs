using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phong_truyen_thong.Model
{
    class DBmanager
    {
        public static SQLiteConnection connection;
        public static async Task ConnectDatabase()
        {
            StaticVariable.listMenu.Clear();
            StaticVariable.listPost.Clear();
            string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            connection = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
           var menuQuery = connection.Table<Menu>();
          var postQuery = connection.Table<Post>();
            foreach(var menu in menuQuery)
            {
                menu.Background= "ms-appdata:///local/" + menu.Background + ".png";
                StaticVariable.listMenu.Add(menu);
            }
            foreach (var p in postQuery)
            {
                p.Thumbnail = "ms-appdata:///local/Media/"+p.Thumbnail+".png";
                if(p.TypePost==StaticVariable.POST_VIDEO) p.Media= "ms-appdata:///local/" + p.Media + ".png";
                else
                {
                    string[] res = p.Media.Split(',');
                    p.Media = "";
                    for (int i = 0; i < res.Length-1; i++)
                    {
                        p.Media += ("ms-appdata:///local/" + res[i] + ".png,");
                    }
                    p.Media += ("ms-appdata:///local/" + res[res.Length-1] + ".png");
                }
                //tmp.Media += ("ms-appdata:///local/images/" + res[i] + ".png");
                StaticVariable.listPost.Add(p);
            }
           // Debug.WriteLine("thanh cong " + StaticVariable.listMenu[0].ba);
        }
       
    }
}
