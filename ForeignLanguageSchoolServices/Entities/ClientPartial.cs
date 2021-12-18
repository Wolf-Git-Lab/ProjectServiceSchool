using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguageSchoolServices.Entities
{
    public partial class Client
    {
        /// <summary>
        /// Свойства для вывода ФИО
        /// </summary>
        public string ClientFIO
        {
            get
            {

                return $"{FirstName} {LastName} {Patronymic}";

            }
        }
    }
}
