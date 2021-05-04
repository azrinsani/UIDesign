using ProtoBuf;
using System.ComponentModel;
using ContactManager.Models;

namespace ContactManager.ViewModels
{
    [ProtoContract]
    public class CustomDataViewModal: DataViewModel, INotifyPropertyChanged
    {
        public CustomDataViewModal() { }
        public CustomDataViewModal(string id, string type, string data)
        {
            this.Id = id;
            this.Type = type;
            this.Data = data;
        }           
        public CustomData ToModel()
        {
            return new CustomData(this.Id, this.Type, this.Data);
        }
    }
}
