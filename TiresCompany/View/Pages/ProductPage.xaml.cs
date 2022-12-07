﻿using System;
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
using TiresCompany.Model;

namespace TiresCompany.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        Core db = new Core();
        List<ProductType> productTypes;
        bool reverseType;
        int page = 1;
        int countElement = 10;
        public ProductPage()
        {
            InitializeComponent();
            //сортировка 
            List<string> sotrTypeList = new List<string>()
            {
                "наименование","остаток на складе","стоимость"
            };
            SortComboBox.ItemsSource = sotrTypeList;
            //фильтрация
            productTypes = new List<ProductType>
            {
                new ProductType()
                {
                    ID=0,
                    Title="Все типы"
                }
            };
            productTypes.AddRange(db.context.ProductType.ToList());
            FilterComboBox.ItemsSource = productTypes;
            UpdateUI();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            UpdateUI();
        }
        private void UpdateUI()
        {

          
           // ProductListView.ItemsSource = GetRows();
            CountRowsTextBlock.Text = $"{GetRows().Count()} / {db.context.Product.ToList().Count()}";
            if (GetRows().Count > 10)
            {
                DisplayPagination(page);
                List<Product> displayProduct = GetRows()
                    .Skip((page - 1) * countElement)
                    .Take(countElement)
                    .ToList();
                
            }
            else
            {
                PaginationListView.Visibility = Visibility.Collapsed;
            }
        }

        private List<Product> GetRows()
        {
            List<Product> arrayProduct = db.context.Product.ToList();
            string searchData = SearchTextBox.Text.ToUpper();
            if (!String.IsNullOrEmpty(SearchTextBox.Text) && SearchTextBox.Text!= "Введите наименование продукта")
            {
                arrayProduct = arrayProduct.Where(x=>x.Title.ToUpper().Contains(searchData)).ToList();
            }
            int filter = Convert.ToInt32(FilterComboBox.SelectedValue);
            if (FilterComboBox.SelectedIndex==0)
            {
                arrayProduct = arrayProduct.ToList();
            }
            else
            {
                arrayProduct = arrayProduct.Where(x => x.ProductTypeID==filter).ToList();
            }
            int value = SortComboBox.SelectedIndex;
            if (value == 0)
            {
                arrayProduct = arrayProduct.OrderBy(p => p.Title).ToList();
            }
            else if (value == 1)
            {
                arrayProduct = arrayProduct.OrderBy(p => p.ProductionWorkshopNumber).ToList();
            }
            else if (value == 2)
            {
                arrayProduct = arrayProduct.OrderBy(p => p.CostProduct).ToList();
            }
            if (reverseType)
            {
                arrayProduct.Reverse();
            }
            return arrayProduct;
        }

        private void SearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUI();
        }

        private void FilterComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
        }

        private void ReverseButtonClick(object sender, RoutedEventArgs e)
        {
            reverseType = !reverseType;
            UpdateUI();
        }

        private void SortComboBoxSelectionChanget(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
        }

        private void AddProductButtonClic(object sender, RoutedEventArgs e)
        {
            Product item = ProductListView.SelectedItem as Product;
            if (item == null)
            {
                this.NavigationService.Navigate(new UpdateProductPage());
            }
            else
            {
                this.NavigationService.Navigate(new UpdateProductPage(item,db));
            }
            
        }

        private void ProductListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddProductButton.Content = "Редактировать товар";
        }
        private int GetPagesCount()
        {
            int countPage = 0;
            int countElement = 10;
            int count = GetRows().Count;
            if (count > countElement)
            {
                countPage = Convert.ToInt32(Math.Ceiling(count * 1.0 / countElement));
            }
            return countPage;
        }
        public void DisplayPagination(int page)
        {
            List<PageItem> source = new List<PageItem>();
            for (int i = 1; i <= GetPagesCount(); i++)
                source.Add(new PageItem(i, i == page));
                 .ItemsSource = source;
            PrevTextBlock.Visibility = (page <= 1 ? Visibility.Hidden : Visibility.Visible);
            NextTextBlock.Visibility = (page >= GetPagesCount() ? Visibility.Hidden : Visibility.Visible);
            foreach (var item in source)
            {
                Console.WriteLine(item.Num);
            }
        }
        
    }
    class PageItem
    {
        public int Num { get; set; } //
        public string Selected { get; set; }
        public string TextColor { get; set; }
        public string FontWeight { get; set; }
        public PageItem()
        {
            Num = 0;
            Selected = "None";
            TextColor = "Black";
            FontWeight = "Normal";
        }
        public PageItem(int num, bool selected)
        {
            Num = num;
            Selected = (selected ? "Underline" : "None");
            TextColor = (selected ? "Blue" : "Black");
            FontWeight = (selected ? "Bold" : "Normal");
        }       
    }
    
}
