using SQLite;
using Syncfusion.XForms.DataForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PickerItemsFromSQLite
{
    public class ViewModel
    {
        SQLiteConnection database;
        ObservableCollection<ContactInfo> orderItemCollection;
        ObservableCollection<ContactInfo> OrderList;
        public ObservableCollection<ContactInfo> OrderItemCollection
        {
            get
            {
                if (orderItemCollection == null)
                {
                    GetItems();
                    orderItemCollection = OrderList;
                }
                return orderItemCollection;
            }
        }

        SQLiteConnection pickerData;      
        List<PickerItem> pickerList;
        public ViewModel()
        {
            //create the table for picker data.
            pickerData = DependencyService.Get<ISQLite>().GetConnection();
            pickerData.CreateTable<PickerItem>();

            // Insert items into table.
            pickerData.Query<PickerItem>("INSERT INTO PickerItem (CityName)values ('Chennai')");
            pickerData.Query<PickerItem>("INSERT INTO PickerItem (CityName)values ('Vatican')");
            pickerData.Query<PickerItem>("INSERT INTO PickerItem (CityName)values ('Paris')");


            database = DependencyService.Get<ISQLite>().GetConnection();
            // Create the table
            database.CreateTable<ContactInfo>();
            database.Query<ContactInfo>("INSERT INTO ContactInfo (ID,Name)values (1002,'Blake')");
            database.Query<ContactInfo>("INSERT INTO ContactInfo (ID,Name)values (1003,'Catherine')");
            database.Query<ContactInfo>("INSERT INTO ContactInfo (ID,Name)values (1004,'Jude')");
        }

        public async void GetItems()
        {
            // Changing the database table items as ObservableCollection
            var table = (from i in database.Table<ContactInfo>() select i);

            OrderList = new ObservableCollection<ContactInfo>();

            foreach (var order in table)
            {
                OrderList.Add(new ContactInfo()
                {
                    ID = order.ID,
                    Name = order.Name
                });
            }
            await Task.Delay(2000);
           
        }

        public IEnumerable<PickerItem> GetPickerItems()
        {
            var table = (from i in pickerData.Table<PickerItem>() select i);
            pickerList = new List<PickerItem>();
            foreach (var order in table)
            {
                pickerList.Add(new PickerItem()
                {
                    CityName = order.CityName
                });
            }
            return pickerList;
        }
    }
}
