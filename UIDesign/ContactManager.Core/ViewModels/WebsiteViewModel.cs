using ProtoBuf;
using ContactManager.Models;
namespace ContactManager.ViewModels
{
    [ProtoContract]
    public class WebsiteViewModel : DataViewModel
    {
        public WebsiteViewModel() { }
        public WebsiteViewModel(string id, string data, string type, string customType) : base(id, data, type, customType) { }
        public WebsiteData ToModel()
        {
            return new WebsiteData(this.Id, this.Type, this.Data, this.CustomType);
        }
    }
}
