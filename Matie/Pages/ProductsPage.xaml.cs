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
using Matie.Databases;

namespace Matie.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        bool isSort = false;
        int currentPage = 1;
        int itemsPerPage = 6;
        int totalPages = 1;

        public ProductsPage()
        {
            InitializeComponent();
            if (App.currentUser.Role.id != 1)
            {
                New.Visibility = Visibility.Collapsed;
                New.Visibility = Visibility.Collapsed;

            }
            Refresh();
        }
        public void Refresh()
        {
            ProductsWP.Children.Clear();

            var query = App.db.Product.AsQueryable();

            // Применяем фильтр, только если в поиске есть текст
            string searchText = SearchTB.Text.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(p => p.Name.ToLower().Contains(searchText));
            }

            if (isSort)
                query = query.OrderBy(p => p.Name);

            var allProducts = query.ToList();

            if (allProducts.Count == 0)
            {
                CountOnPageTb.Text = "Нет товаров";
                return;
            }

            totalPages = (int)Math.Ceiling((double)allProducts.Count / itemsPerPage);
            if (currentPage > totalPages) currentPage = totalPages;
            if (currentPage < 1) currentPage = 1;

            var pagedProducts = allProducts
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            foreach (var product in pagedProducts)
            {
                ProductsWP.Children.Add(new UC.ProductUC(product));
            }

            CountOnPageTb.Text = $"Страница {currentPage} из {totalPages}";
        }



        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditProductPage(new Product()));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            isSort = !isSort;
            Refresh();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.WindowState = WindowState.Minimized;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                Refresh();
            }

        }
      

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                Refresh();
            }
        }

    }
}
