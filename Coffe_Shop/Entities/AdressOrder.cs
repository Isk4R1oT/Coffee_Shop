using Coffee_Shop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee_Shop.Entities
{
    interface IAdressBilder
    {
        IAdressBilder Adres(string adres);
        IAdressBilder City(string city);
        IAdressBilder State(string state);
        IAdressBilder ZipCode(int zipcode);
        IAdressBilder Country(string country);
        AdressOrder Create();

    }
    public class AdressOrder
    {
        public string Adress { get; set;}
        public string City{ get; set; }
        public string State { get; set; }
        public int ZipCOde {  get; set; }
        public string Country{ get; set; }
    }
    class AdressOrderBilder : IAdressBilder 
    {
        private AdressOrder _adressOrder { get; set; }

        public AdressOrderBilder(AdressOrder adressOrder)
        {
             _adressOrder = adressOrder;
        }

        public IAdressBilder Adres(string adres)
        {
           _adressOrder.Adress = adres;
            return this;
        }

        public IAdressBilder City(string city)
        {
            _adressOrder.City = city;
            return this;
        }

        public IAdressBilder Country(string country)
        {
            _adressOrder.Country = country;
            return this;
        }

        public AdressOrder Create()
        {
           return _adressOrder;
        }

        public IAdressBilder State(string state)
        {
           _adressOrder.State = state;
            return this;
        }

        public IAdressBilder ZipCode(int zipcode)
        {
            _adressOrder.ZipCOde = zipcode;
            return this;
        }
    }


}
