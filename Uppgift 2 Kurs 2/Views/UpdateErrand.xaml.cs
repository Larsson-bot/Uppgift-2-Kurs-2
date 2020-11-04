using LibaryAccess.Data;
using LibaryAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.WindowManagement;
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
    public sealed partial class UpdateErrand : Page
    {
        private IEnumerable<Errand> errands { get; set; }
        public UpdateErrand()
        {
            this.InitializeComponent();
            GetErrands().GetAwaiter();
            GetStatus();
        }
        private void GetStatus()
        {
            cbxChooseStatus.ItemsSource = SettingsData.GetStatus();
        }
        private async Task GetErrands()
        {
            errands = await SqliteContext.GetErrandsAsync();
            LoadAllErrands();
        }
        private void LoadAllErrands()
        {
            gvErrands.ItemsSource = errands.ToList();
        }

        private async void btnUpdateErrand_Click(object sender, RoutedEventArgs e)
        {
            string status = cbxChooseStatus.SelectedItem.ToString();
            long idFromTbx = Convert.ToInt64(tbxChooseId.Text);
            await SqliteContext.UpdateErrandStatus(status,idFromTbx);
        }
    }
}
