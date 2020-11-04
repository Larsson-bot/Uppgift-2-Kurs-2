using LibaryAccess.Data;
using LibaryAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Uppgift_2_Kurs_2.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Uppgift_2_Kurs_2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
     
 
        public MainPage()
        {
            this.InitializeComponent();
            
        
        }

      
       
   

        private  void btnCreateErrand_Click(object sender, RoutedEventArgs e)
        {
           
            frame.Navigate(typeof(CreateErrand));
 
  
      
                }

        private void btnUpdateErrand_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(UpdateErrand));
        }

        private void btnCompleteErrands_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(ListingFinishedErrands));
        }

        private  void btnOngoingErrands_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(ListingCurrentErrands));

        }
    }
}
