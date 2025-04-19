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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Matie.Databases;
using Microsoft.Win32;

namespace Matie.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        Product product;
        byte[] bytes;
        public AddEditProductPage(Product _product)
        {
            InitializeComponent();
            product = _product;
            DataContext = product;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            var opn = new OpenFileDialog();
            opn.Title = "Выберите изображение";
            opn.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff|All Files|*.*";
            if (opn.ShowDialog() == true)
            {
                bytes = File.ReadAllBytes(opn.FileName);
                if (bytes != null)
                {
                    MemoryStream memoryStream = new MemoryStream(bytes);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = memoryStream;
                    image.EndInit();
                    IconIMG.Source = image;
                }
            }

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            product.EditDate = DateTime.Now;
            if (!(bytes == null))
            {
                product.Icon = bytes;
            }
            if (product.id == 0)
            {
                App.db.Product.Add(product);
            }
            App.db.SaveChanges();
            NavigationService.GoBack();
        }
    }
}
