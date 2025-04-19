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

namespace Matie.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }



        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.db.User.ToList()
                .Any(x => x.Phone == PhoneTb.Text && x.Password == PassTb.Text))
            {
                App.currentUser = App.db.User.ToList()
                    .First(x => x.Phone == PhoneTb.Text && x.Password == PassTb.Text);

                NavigationService.Navigate(new ProductsPage());
            }

            else
            {
                MessageBox.Show("Проверьте данные для входа!");
            }
        }
    }
}
