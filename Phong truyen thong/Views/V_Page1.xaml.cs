using Phong_truyen_thong.Model;
using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Phong_truyen_thong.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class V_Page1 : Page
    {
        List<Post> posts;
        public V_Page1()
        {
            posts = StaticVariable.listPost;
            this.InitializeComponent();
        }

        private void menu_ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowDetail.Begin();
            generalHide.Begin();
        }

        private void generalHide_Completed(object sender, object e)
        {
            generalShow.Begin();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            HideDetail.Begin();
            generalHide.Begin();
        }
    }
}
