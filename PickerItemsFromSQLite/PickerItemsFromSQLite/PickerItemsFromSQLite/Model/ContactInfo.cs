using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickerItemsFromSQLite
{
    public class ContactInfo
    {

        public ContactInfo()
        {

        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }

        public string City { get; set; }
    }

    public class PickerItem
    {
        public string CityName { get; set; }
    }
}
