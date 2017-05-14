using Phong_truyen_thong.Model;
using Phong_truyen_thong.Views;
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

namespace Phong_truyen_thong
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SubPage : Page
    {
        List<Menu> infoListMenu=new List<Menu>();
        int idBtnSelected = -1;
        Menu pMenu;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            pMenu = e.Parameter as Menu;
            foreach (Menu m in StaticVariable.listMenu)
            {
                if (m.IdParent == pMenu.Id) infoListMenu.Add(m);
            }
            createListBtn();
        }
        private ImageBrush imgSource(string path = "ms-appx:///Assets/image/imgTest.jpg")
        {
            ImageBrush brush = new ImageBrush();
            BitmapImage bitmapImage = new BitmapImage(new Uri(path));
            brush.ImageSource = bitmapImage;
            brush.Stretch = Stretch.UniformToFill;
            return brush;
        }
        public SubPage()
        {
            this.InitializeComponent();
        }
        private void createListBtn()
        {
            menuItem.Children.Clear();
            for (int i = 0; i < infoListMenu.Count; i++)
            {
                Button tmp = createBtn(infoListMenu[i].Background);
                tmp.Content = infoListMenu[i].Name;
                tmp.Margin = new Thickness((Math.Cos(i * 2 * Math.PI / infoListMenu.Count + Math.PI / 2) + 1) * (menuItem.Width - tmp.Width) / 2, (Math.Sin(i * 2 * Math.PI / infoListMenu.Count + Math.PI / 2) + 1) * (menuItem.Height - tmp.Height) / 2, 0, 0);
                tmp.Click += (s, e) => {
                    idBtnSelected = infoListMenu[menuItem.Children.IndexOf(s as Button)].Id;
                    menuHide.Begin();
                };
                menuItem.Children.Add(tmp);
            }
        }

        private Button createBtn(string background, int width = 100, int height = 100)
        {
            Button Btn = new Button();
            Btn.Height = height;
            Btn.Width = width;
            Btn.HorizontalAlignment = HorizontalAlignment.Left;
            Btn.VerticalAlignment = VerticalAlignment.Top;
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(background));
            Btn.Background = ib;
            Btn.Style = Application.Current.Resources["imgBtn"] as Style;
            return Btn;
        }
        private void ImageBrush_ImageOpened(object sender, RoutedEventArgs e)
        {
            menuShow.Begin();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            menuHide.Begin();
            //Frame.Navigate(typeof(MainPage));
        }

        private void menuHide_Completed(object sender, object e)
        {
            if(idBtnSelected==-1) Frame.Navigate(typeof(MainPage));
            else
            {
                Menu a = infoListMenu.Find(x => x.Id == idBtnSelected);
                if (a.Theme == StaticVariable.THEME1) Frame.Navigate(typeof(H_Page1), a);
                if (a.Theme == StaticVariable.THEME2) Frame.Navigate(typeof(H_Page2), a);
            }
        }
    }
}
