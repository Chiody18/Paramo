using System;
using System.Collections.Generic;
using Sat.Recruitment.Api.Utils;

namespace Sat.Recruitment.Api.Entities
{
    public class User
    {
        private static readonly Dictionary<string, Func<decimal, decimal>> gifLookup = new Dictionary<string, Func<decimal, decimal>>
        {
            { UserTypeEnum.Normal.ToString(), money => money > 100 ? money * 0.12m : money > 10 ? money * 0.8m : 0 },
            { UserTypeEnum.SuperUser.ToString(), money => money > 100 ? money * 0.20m : 0 },
            { UserTypeEnum.Premium.ToString(), money => money > 100 ? money * 2 : 0 }
        };
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }

        public User()
        {
            
        }
        public User(string name, string email, string address, string phone, string userType, string money) 
        {
            var calculateGif = gifLookup.GetValueOrDefault(userType, money => 0);
            var gif = calculateGif(decimal.Parse(money));
            Name = name;
            Email = EmailUtils.NormalizeEmail(email);
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = decimal.Parse(money) + gif;
        }
    }
}
