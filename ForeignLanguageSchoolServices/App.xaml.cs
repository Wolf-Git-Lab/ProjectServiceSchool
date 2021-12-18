using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ForeignLanguageSchoolServices
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.ForeignLanguageSchoolServicesEntities Context = new Entities.ForeignLanguageSchoolServicesEntities();
        public static Entities.User Users = null;
    }
}
