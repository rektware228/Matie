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
using Matie.Pages;

namespace Matie.UC
{
    /// <summary>
    /// Логика взаимодействия для ProductUC.xaml
    /// </summary>
    public partial class ProductUC : UserControl
    {
        Product product;
        public ProductUC(Product _product)
        {
            InitializeComponent();
            product = _product;
            DataContext = product;
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.currentUser.Role.id == 1)
            {
                NavigationService.GetNavigationService(this).Navigate(new AddEditProductPage(product));
            }
        }


    }
}
