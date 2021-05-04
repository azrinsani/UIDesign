using System;
using System.Globalization;
using Xamarin.Forms;
using AzUtil.Core;
using ContactManager.ViewModels;

namespace ContactManager.Converters
{
    public class ContactViewSelector : DataTemplateSelector
    {
        public DataTemplate Togglers { get; set; }
        public DataTemplate Contact { get; set; }
        public DataTemplate ContactAd { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var itemVM = (BaseContactViewModel)item;
            return itemVM.ContactViewType switch
            {
                ContactViewType.Togglers => Togglers,
                ContactViewType.ContactAd => ContactAd,
                _ => Contact,
            };
        }
    }

    public class ContactGroupItemSelector : DataTemplateSelector
    {
        public DataTemplate ContactGroup { get; set; }
        public DataTemplate ContactShowHideGroups { get; set; }
        public DataTemplate ContactNewGroup { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var gmVM = (GroupMembershipViewModel)item;
            if (gmVM.GroupMembershipChipType == GroupMembershipChipType.ShowHideGroupsButton) return ContactShowHideGroups;
            if (gmVM.GroupMembershipChipType == GroupMembershipChipType.NewButton) return ContactNewGroup;
            return ContactGroup;
        }
    }
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value != 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
    public class BoolTrueToOpacityLowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = (bool)value ? (double)0.2 : (double)1;
            return res;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class ContactActionNoneOrDoneToFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ContactAction actionType)
            {
                return (actionType != ContactAction.None && actionType != ContactAction.Done);
            }
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ContactActionNoneToFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ContactAction actionType)
            {
                return (actionType != ContactAction.None);
            }
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BoolTrueToFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = !(bool)value;
            return res;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class StringNullOrEmptyToFalse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is string s && !s.IsNullOrEmpty());
            
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    public class BoolTrueToIconColorSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var res = (bool)value? Color.Accent : Color.FromHex("#CCC");
            return res;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    




  
}
