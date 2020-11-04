using LibaryAccess.Data;
using LibaryAccess.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Uppgift_2_Kurs_2.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListingCurrentErrands : Page
    {
  
     private  IEnumerable<Errand> errands { get; set; }
        public  ListingCurrentErrands()
        {
            this.InitializeComponent();
            GetErrands().GetAwaiter();
        }
        private async Task GetErrands()
        {
            errands = await SqliteContext.GetErrandsAsync();
            LoadAllErrands();
        }
        private void LoadAllErrands()
        {
            lvErrands.ItemsSource = errands.ToList();
        }

    }
}
