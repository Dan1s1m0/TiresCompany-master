using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace TiresCompany.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для SelectLogoWindow.xaml
    /// </summary>
    public partial class SelectLogoWindow : Window
    {
        public string selectedLogoPath { get; set; }
        public SelectLogoWindow()
        {
            InitializeComponent();
            List<string> list = new List<string>();
            string targetDirectory = "../../Assets/Images/products";
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                list.Add(fileName);
            LogoListView.ItemsSource = list;
        }


        private void AttachButtonClick(object sender, RoutedEventArgs e)
        {      
        selectedLogoPath=LogoListView.SelectedItem as string;
            DialogResult = true;
            this.Close();
        }
    }
}
