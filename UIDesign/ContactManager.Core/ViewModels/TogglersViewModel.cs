using System.ComponentModel;
using ContactManager.Models;
namespace ContactManager.ViewModels
{
    public class TogglersViewModel : BaseContactViewModel, INotifyPropertyChanged
    {
        public TogglersViewModel() { this.ContactViewType = ContactViewType.Togglers; }
    }
}
