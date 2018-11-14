
using Syncfusion.XForms.DataForm;
using Syncfusion.XForms.DataForm.Editors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PickerItemsFromSQLite
{
    public class DataFormBehavior : Behavior<ContentPage>
    {
     
        SfDataForm dataForm;

        ViewModel ViewModel;
        protected override void OnAttachedTo(ContentPage bindable)
        {

            ViewModel = new ViewModel();
            base.OnAttachedTo(bindable);
            dataForm = bindable.FindByName<SfDataForm>("dataForm");
            dataForm.DataObject = ViewModel.OrderItemCollection[0];
            dataForm.AutoGeneratingDataFormItem += DataForm_AutoGeneratingDataFormItem;
            if (Device.RuntimePlatform != Device.UWP)
                dataForm.RegisterEditor("City", "Picker");    
        }

        private void DataForm_AutoGeneratingDataFormItem(object sender, AutoGeneratingDataFormItemEventArgs e)
        {
            if (Device.RuntimePlatform != Device.UWP)
            {
                if (e.DataFormItem != null && e.DataFormItem.Name == "City")
                {
                    var itemSource = ViewModel.GetPickerItems();
                    (e.DataFormItem as DataFormPickerItem).DisplayMemberPath = "CityName";
                    (e.DataFormItem as DataFormPickerItem).ValueMemberPath = "CityName";
                    (e.DataFormItem as DataFormPickerItem).ItemsSource = itemSource;
                }
            }
        }
    }
}
