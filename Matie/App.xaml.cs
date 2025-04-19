using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Matie.Databases;

namespace Matie
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static Matie_DBEntities db = new Matie_DBEntities();
        public static User currentUser;
    }
}
