using Microsoft.Win32;
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
using TiresCompany.Model;
using TiresCompany.View.Pages;


namespace TiresCompany.View
{
    /// <summary>
    /// Логика взаимодействия для UpdateProductPage.xaml
    /// </summary>
    public partial class UpdateProductPage : Page
    {
        Core db=new Core() ;
        Product product;
        Product activeProduct;
        /// <summary>
        /// Добавление нового
        /// </summary>
        /// <param name="product"></param>
        /// <param name="db"></param>
        public UpdateProductPage()
        {
            InitializeComponent();
            TypeComboBox.ItemsSource = db.context.ProductType.ToList();
            activeProduct = new Product();
            this.DataContext = product;
        }
        /// <summary>
        /// Редактирование продукта
        /// </summary>
        /// <param name="product"></param>
        /// <param name="db"></param>
        public UpdateProductPage(Product product,Core db)
        {
            InitializeComponent();
            TypeComboBox.ItemsSource = db.context.ProductType.ToList();
            
            
            activeProduct = product;
            this.DataContext = activeProduct;
            this.db = db;
        }


        private void ChangeImageButtonClick(object sender, RoutedEventArgs e)
        {
            Uri source;
            OpenFileDialog win = new OpenFileDialog();
            if (win.ShowDialog() == true)
            {
             
                source = new Uri(win.FileName);
                ProductImage.Source = new BitmapImage(source);
            }


            activeProduct.Image= "/products/"+System.IO.Path.GetFileName(win.FileName);
              
            }

        private void ImageButtonClick(object sender, RoutedEventArgs e)
        {
           
            db.context.SaveChanges();
            this.NavigationService.Navigate(new ProductPage());
        }
    }
}
