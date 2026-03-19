using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VinylRecordsApplication_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OpenPages(Page page) => frame.Navigate(page);

        private void OpenRecordList(object sender, RoutedEventArgs e)
        {

        }

        private void OpenRecordAdd(object sender, RoutedEventArgs e)
        {

        }

        private void ExportRecord(object sender, RoutedEventArgs e)
        {

        }

        private void OpenManufacruersList(object sender, RoutedEventArgs e)
        {

        }

        private void OpenManufacruersAdd(object sender, RoutedEventArgs e)
        {

        }

        private void OpenSupplyList(object sender, RoutedEventArgs e)
        {

        }

        private void OpenSupplyAdd(object sender, RoutedEventArgs e)
        {

        }

        private void OpenStateList(object sender, RoutedEventArgs e)
        {

        }

        private void OpenStateAdd(object sender, RoutedEventArgs e)
        {

        }
    }
}
