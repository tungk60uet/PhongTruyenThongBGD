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
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Phong_truyen_thong
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Menu> infoListMenu = new List<Menu>();
        int idBtnSelected=0,count=0,temp=0;
        DispatcherTimer dispatcherTimer;
        DateTimeOffset lasttime = DateTimeOffset.Now;
        Menu menuObj=null;
        private ImageBrush imgSource(string path= "ms-appx:///Assets/image/imgTest.jpg")
        {
            ImageBrush brush = new ImageBrush();
            BitmapImage bitmapImage = new BitmapImage(new Uri(path));
            brush.ImageSource = bitmapImage;
            brush.Stretch = Stretch.UniformToFill;
            return brush;
        }
        public MainPage()
        {
            DBmanager.ConnectDatabase();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            this.InitializeComponent();
            infoListMenu = StaticVariable.listMenu.FindAll(x => x.IdParent == 0);
            if (StaticVariable.EditMode == 1)
            {
                add_Btn.Visibility = Visibility.Visible;
                editTheme_Btn.Visibility = Visibility.Visible;
                ExitEditMode_Btn.Visibility = Visibility.Visible;

                delItem.Children.Clear();
                for (int i = 0; i < infoListMenu.Count; i++)
                {
                    Button tmp = createBtn("ms-appx:///Assets/image/x.png",20,20);
                    tmp.Margin = new Thickness((Math.Cos(i * 2 * Math.PI / infoListMenu.Count + Math.PI / 2) + 1) * (menuItem.Width - tmp.Width-80) / 2-30, (Math.Sin(i * 2 * Math.PI / infoListMenu.Count + Math.PI / 2) + 1) * (menuItem.Height - tmp.Height-80) / 2, 0, 0);
                    tmp.Click += (s, e) => {
                        DBmanager.connection.Delete(StaticVariable.listMenu.Find(x => x.Id == infoListMenu[delItem.Children.IndexOf(s as Button)].Id));
                        Frame.Navigate(typeof(MainPage));
                    };
                    delItem.Children.Add(tmp);
                }

                editItem.Children.Clear();
                for (int i = 0; i < infoListMenu.Count; i++)
                {
                    Button tmp = createBtn("ms-appx:///Assets/image/edit.png", 20, 20);
                    tmp.Margin = new Thickness((Math.Cos(i * 2 * Math.PI / infoListMenu.Count + Math.PI / 2) + 1) * (menuItem.Width - tmp.Width - 80) / 2-30, (Math.Sin(i * 2 * Math.PI / infoListMenu.Count + Math.PI / 2) + 1) * (menuItem.Height - tmp.Height - 80) / 2+80, 0, 0);
                    tmp.Click += (s, e) => {
                        menuObj = StaticVariable.listMenu.Find(x => x.Id == infoListMenu[editItem.Children.IndexOf(s as Button)].Id);
                        TenNut.Text = menuObj.Name;
                        comboBox.SelectedIndex = menuObj.Theme;
                        image.Source = new BitmapImage(new Uri(menuObj.Background));
                        Rect.Visibility = Visibility.Visible;
                        BtnDetail.Visibility = Visibility.Visible;
                        //  DBmanager.connection.Delete(StaticVariable.listMenu.Find(x => x.Id == infoListMenu[editItem.Children.IndexOf(s as Button)].Id));
                        //  Frame.Navigate(typeof(MainPage));
                    };
                    editItem.Children.Add(tmp);
                }
            }
            rootGrid.Background = imgSource("ms-appx:///Assets/image/imgTest.jpg");
            createListBtn();
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            
        }

        private void createListBtn()
        {
            menuItem.Children.Clear();
            
            //Debug.WriteLine("thanh cong " + infoListMenu[0].Background);
            for (int i = 0; i < infoListMenu.Count; i++)
            {
                Button tmp = createBtn(infoListMenu[i].Background);
                tmp.Content = infoListMenu[i].Name;
                tmp.Margin = new Thickness((Math.Cos(i * 2 * Math.PI / infoListMenu.Count + Math.PI / 2) + 1) * (menuItem.Width - tmp.Width) / 2, (Math.Sin(i * 2 * Math.PI / infoListMenu.Count + Math.PI / 2) + 1) * (menuItem.Height - tmp.Height) / 2, 0, 0);
                tmp.Click += (s, e) => {
                    idBtnSelected =infoListMenu[menuItem.Children.IndexOf(s as Button)].Id;
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

        private void menuHide_Completed(object sender, object e)
        {
            Menu a=infoListMenu.Find(x => x.Id == idBtnSelected);
           
            if (a.Theme == StaticVariable.SUB_MENU) Frame.Navigate(typeof(SubPage), a);
            if (a.Theme == StaticVariable.THEME1) Frame.Navigate(typeof(H_Page1), a);
            if (a.Theme == StaticVariable.THEME2) Frame.Navigate(typeof(H_Page2), a);
        }
        private void ImageBrush_ImageOpened(object sender, RoutedEventArgs e)
        {
            menuShow.Begin();
        }
        private void editTheme_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPassOK_Click(object sender, RoutedEventArgs e)
        {
            if (tbPass.Text == "admin") StaticVariable.EditMode = 1;
            NhapPass.Visibility = Visibility.Collapsed;
            Frame.Navigate(typeof(MainPage));
        }

        private void ExitEditMode_Btn_Click(object sender, RoutedEventArgs e)
        {
            StaticVariable.EditMode = 0;
            Frame.Navigate(typeof(MainPage));
        }

        private async void image_Tapped(object sender, TappedRoutedEventArgs e)
        {;
              FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                // Application now has read/write access to the picked file
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    try
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        //   bitmapImage.DecodePixelWidth = 300;
                        await bitmapImage.SetSourceAsync(fileStream);
                        image.Source = bitmapImage;
                        // Set the image source to the selected bitmap 
                        int x = int.Parse(await FileIO.ReadTextAsync(await ApplicationData.Current.LocalFolder.GetFileAsync("count.txt")));
                        Debug.WriteLine(x);
                        BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream.CloneStream());
                        SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                        StorageFile file_Save = await ApplicationData.Current.LocalFolder.CreateFileAsync(x.ToString() + ".png", CreationCollisionOption.ReplaceExisting);
                        x++;
                        await FileIO.WriteTextAsync(await ApplicationData.Current.LocalFolder.CreateFileAsync("count.txt", CreationCollisionOption.ReplaceExisting),x.ToString());
                        BitmapEncoder encoder = await BitmapEncoder.CreateAsync(Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId, await file_Save.OpenAsync(FileAccessMode.ReadWrite));
                        encoder.SetSoftwareBitmap(softwareBitmap);
                        await encoder.FlushAsync();
                    }
                    catch { }
                }
            }
            else
            {

            }
        }

        private void Rect_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Rect.Visibility = Visibility.Collapsed;
            BtnDetail.Visibility = Visibility.Collapsed;
            TenNut.Text = "";
            comboBox.SelectedIndex = 0;
            image.Source= new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png"));
            Frame.Navigate(typeof(MainPage));
        }

        private async void XacNhanNut_Click(object sender, RoutedEventArgs e)
        {
            int x = int.Parse(await FileIO.ReadTextAsync(await ApplicationData.Current.LocalFolder.GetFileAsync("count.txt")));
            if (menuObj != null) {
                menuObj.Background = (x - 1).ToString();
                menuObj.Name = TenNut.Text;
                menuObj.Theme = comboBox.SelectedIndex;
                DBmanager.connection.Update(menuObj);
                menuObj = null;
            } 
            else
            DBmanager.connection.Insert(new Menu { Background=(x-1).ToString(), Name=TenNut.Text, Theme=comboBox.SelectedIndex});
            Rect.Visibility = Visibility.Collapsed;
            BtnDetail.Visibility = Visibility.Collapsed;
            TenNut.Text = "";
            comboBox.SelectedIndex = 0;
            image.Source = new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png"));
            Frame.Navigate(typeof(MainPage));
        }

        private void add_Btn_Click(object sender, RoutedEventArgs e)
        {
            Rect.Visibility = Visibility.Visible;
            BtnDetail.Visibility = Visibility.Visible;
        }

        private void main_Btn_Click(object sender, RoutedEventArgs e)
        {
            DateTimeOffset now = DateTimeOffset.Now;
            if ((now - lasttime).Milliseconds < 400) count++;
            else count = 0;
            if (count == 10) NhapPass.Visibility = Visibility.Visible;
           // Debug.WriteLine(count);
            lasttime = now;
        }
    }
}
