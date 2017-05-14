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
    public sealed partial class H_Page2 : Page
    {
        List<Post> posts = new List<Post>();
        DispatcherTimer dispatcherTimer;
        DateTimeOffset lasttime;
        Menu pMenu;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            pMenu = e.Parameter as Menu;
            generalTxt.Text = pMenu.Name;
            //    Debug.WriteLine(pMenu.Id);
            foreach (var p in StaticVariable.listPost)
            {
                //  Debug.WriteLine(p.IdGroup);
                if (p.IdGroup == pMenu.Id) posts.Add(p);
            }
        }
        public H_Page2()
        {
            this.InitializeComponent();
         
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
        }
        private void dispatcherTimer_Tick(object sender, object e)
        {
            if (flipView.SelectedIndex < flipView.Items.Count - 1) flipView.SelectedIndex++;
            else flipView.SelectedIndex = 0;
        }
        private void menu_ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Post p = (Post)e.ClickedItem;
            DetailContent.Text = p.Content;
            detailTitle.Text = p.Title;
            if (p.TypePost == 1)
            {
                mediaElement.Source= new Uri(p.Media);
                mediaElement.Visibility = Visibility.Visible;
                mediaElement.Play();
                flipView.Visibility = Visibility.Collapsed;
            }
            else
            {
                flipView.Visibility = Visibility.Visible;
                mediaElement.Visibility = Visibility.Collapsed;
                mediaElement.Stop();
                string[] res = p.Media.Split(',');
                for (int i = 0; i < res.Length; i++)
                {
                    if (res[i].Trim() == "") continue;
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(res[i]));
                    img.Stretch = Stretch.Uniform;
                    flipView.Items.Add(img);
                }
                dispatcherTimer.Start();
                lasttime = DateTimeOffset.Now;
            }
            showDetail.Begin();
        }

        private void detailbackBtn_Click(object sender, RoutedEventArgs e)
        {
            hideDetail.Begin();
           
        }

        private void rectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            detailbackBtn_Click(null,null);
        }

        private void hideDetail_Completed(object sender, object e)
        {
            mediaElement.Stop();
            dispatcherTimer.Stop();
        }

        private void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            Debug.WriteLine((time - lasttime).Seconds);
            if ((time - lasttime).Seconds != 2) dispatcherTimer.Stop();
            lasttime = time;
        }

        private void ReplaceHide_Completed(object sender, object e)
        {
            flipView.Items.Clear();
            dispatcherTimer.Stop();
            Post p = (Post)navListView.SelectedItem;
            detailTitle.Text = p.Title;
            DetailContent.Text = p.Content;
            if (p.TypePost == 1)
            {
                mediaElement.Source = new Uri(p.Media);
                mediaElement.Visibility = Visibility.Visible;
                mediaElement.Play();
                flipView.Visibility = Visibility.Collapsed;
            }
            else
            {
                flipView.Visibility = Visibility.Visible;
                mediaElement.Visibility = Visibility.Collapsed;
                mediaElement.Stop();
                string[] res = p.Media.Split(',');
                for (int i = 0; i < res.Length; i++)
                {
                    if (res[i].Trim() == "") continue;
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(res[i]));
                    img.Stretch = Stretch.Uniform;
                    flipView.Items.Add(img);
                }
                dispatcherTimer.Start();
                lasttime = DateTimeOffset.Now;
            }
            ReplaceShow.Begin();
        }

        private void navListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ReplaceHide.Begin();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
