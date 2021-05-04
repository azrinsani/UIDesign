using System.ComponentModel;

namespace ContactManager.ViewModels
{
    public class ContactAdViewModel : BaseContactViewModel, INotifyPropertyChanged
    {
        public ContactAdViewModel() { this.ContactViewType = ContactViewType.ContactAd; }
        public bool IsInternalAd { get => isInternalAd; set => SetProperty(ref isInternalAd, value); }
        private bool isInternalAd = true;
    }
}
