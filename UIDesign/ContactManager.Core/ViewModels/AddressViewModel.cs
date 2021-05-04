using ProtoBuf;
using System.ComponentModel;
using ContactManager.Models;

namespace ContactManager.ViewModels
{
    [ProtoContract]
    public class AddressViewModel : DataViewModel, INotifyPropertyChanged
    {
        public AddressViewModel() { }
        public AddressViewModel(string id, string data, string type, string customType, string street, string pOBox, string neighborhood, string city, string region, string postcode, string country)
            : base(id, data, type, customType) { this.Street = street;this.POBox = pOBox; this.Neighborhood = neighborhood; this.City = city; this.Region = region; this.Postcode = postcode; this.Country = country; }
        
        public AddressData ToModel()
        {
            return new AddressData(Id, Data, Type, CustomType, Street, POBox, Neighborhood, City, Region, Postcode, Country);
        }

        [ProtoMember(1)]
        public string Street { get => street; set => SetProperty(ref street, value); }
        private string street;

        
        [ProtoMember(2)]
        public string POBox { get => pOBox; set => SetProperty(ref pOBox, value); }
        private string pOBox;

        
        [ProtoMember(3)]
        public string Neighborhood { get => neighborhood; set => SetProperty(ref neighborhood, value); }
        private string neighborhood;

        
        [ProtoMember(4)]
        public string City { get => city; set => SetProperty(ref city, value); }
        private string city;

        
        [ProtoMember(5)]
        public string Region { get => region; set => SetProperty(ref region, value); }
        private string region;

        
        [ProtoMember(6)]
        public string Postcode { get => postcode; set => SetProperty(ref postcode, value); }
        private string postcode;

        
        [ProtoMember(11)]
        public string Country { get => country; set => SetProperty(ref country, value); }
        private string country;

    }
}
