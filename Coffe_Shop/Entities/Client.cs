using Coffe_Shop.Entityes;
using Coffee_Shop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Coffee_Shop.Entities
{

    interface IClentBilder
    {
        IClentBilder Name(string name); 
        IClentBilder Email(string email);

        IClentBilder Password(string password);

        Client Bild();
    }
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public Basket Basket { get; set; }
       
        public Client()
        {

        }
    }

    class ClientBilder : IClentBilder
    {
        private Client _client {  get; set; }

        public ClientBilder()
        {
            _client = new Client();
        }
        public IClentBilder Email(string email)
        {
            _client.Email = email;
            return this;
        }

        public IClentBilder Name(string name)
        {
            _client.Name = name;
            return this;
        }

        public IClentBilder Password(string pass)
        {
            _client.Password = PasswordHash.ComputeSHA256Hash(pass);
            return this;
        }

        public Client Bild()
        {
           return _client;
        }
    }
    static class PasswordHash
    {
        public static string ComputeSHA256Hash(this string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);

                // Преобразование массива байтов в строку
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static bool CheckPass(int id,string pass)
        {
            using(var context = new Context())
            {
                var client = context.Clients.First(x=>x.Id == id);
                if(client.Password == pass.ComputeSHA256Hash())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
