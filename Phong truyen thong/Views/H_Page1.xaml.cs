using Phong_truyen_thong.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Phong_truyen_thong.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class H_Page1 : Page
    {
        string genImg, genTxt;
        List<Post> posts=new List<Post>();
        DispatcherTimer dispatcherTimer;
        DateTimeOffset lasttime;
        Menu pMenu;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            pMenu = e.Parameter as Menu;
            generalTxt.Text = pMenu.Name;
            generalImg.Source = new BitmapImage(new Uri(pMenu.Background));
        //    Debug.WriteLine(pMenu.Id);
            foreach(var p in StaticVariable.listPost)
            {
              //  Debug.WriteLine(p.IdGroup);
                if (p.IdGroup == pMenu.Id) posts.Add(p);
            }
           
        }
        public H_Page1()
        {
            this.InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
        }
        private void dispatcherTimer_Tick(object sender, object e){
            if (flipView.SelectedIndex < flipView.Items.Count - 1) flipView.SelectedIndex++;
            else flipView.SelectedIndex = 0;
        }
        private void menu_ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Post p = (Post)e.ClickedItem;
            DetailContent.Text = p.Content;
            flipView.Items.Clear();
            if (p.TypePost==StaticVariable.POST_VIDEO) {
                MediaElement me = new MediaElement();
                me.Source = new Uri(p.Media);
                me.Stretch = Stretch.Uniform;
                me.IsLooping = true;
                me.AutoPlay = true;
                flipView.Items.Add(me);
            }else
            {
                string[] res = p.Media.Split(',');
                for (int i = 0; i < res.Length; i++)
                {
                    if (res[i].Trim() == "") continue;
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(res[i]));
                    img.Stretch = Stretch.Uniform;
                    flipView.Items.Add(img);
                }
            }
            genTxt = p.Title;
            genImg = p.Thumbnail;
            ShowDetail.Begin();
            generalHide.Begin();
            dispatcherTimer.Start();
            lasttime = DateTimeOffset.Now;
        }

        private void generalHide_Completed(object sender, object e)
        {
            generalTxt.Text = genTxt;
            generalImg.Source = new BitmapImage(new Uri(genImg));
            generalShow.Begin();
        }

        private void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            if ((time - lasttime).Seconds != 2) dispatcherTimer.Stop();
            lasttime = time;
        }

        private void HideDetail_Completed(object sender, object e)
        {
            flipView.Items.Clear();
            dispatcherTimer.Stop();
        }

        private void navListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Post p = (Post)e.ClickedItem;
            ReplaceHide.Begin();
            genTxt = p.Title;
            genImg = p.Thumbnail;
            generalHide.Begin();
        }
        private void ReplaceHide_Completed(object sender, object e)
        {
            Post p = (Post)navListView.SelectedItem;
            flipView.Items.Clear();
            dispatcherTimer.Stop();
            DetailContent.Text = p.Content;
            flipView.Items.Clear();
            if (p.TypePost == 1)
            {
                MediaElement me = new MediaElement();
                me.Source = new Uri(p.Media);
                me.Stretch = Stretch.Uniform;
                me.IsLooping = true;
                me.AutoPlay = true;
                flipView.Items.Add(me);
            }
            else
            {
                string[] res = p.Media.Split(',');
                for (int i = 0; i < res.Length; i++)
                {
                    if (res[i].Trim() == "") continue;
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(res[i]));
                    img.Stretch = Stretch.Uniform;
                    flipView.Items.Add(img);
                }
            }
            dispatcherTimer.Start();
            lasttime = DateTimeOffset.Now;
            ReplaceShow.Begin();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            if (stackPanel.Opacity == 1)
            {
                HideDetail.Begin();
                genTxt = pMenu.Name;
                genImg = pMenu.Background;
                generalHide.Begin();
            }
            if (stackPanel.Opacity == 0)
            {
                Frame.GoBack();
            }
        }
    }
}
