using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MiniGuids;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodes_Service.Helper
{
    public static class StringHelper
    {
        public static string GeneatePromo()
        {
            var miniGuid = MiniGuid.NewGuid();

            //implicit conversions (and vice-versa too)
            string someString = miniGuid;
            Guid someGuid = miniGuid;

            //explicit stringifying and parsing
            var str = miniGuid.ToString();
            var sameMiniGuid = str.Substring(0, 5);
            var intGuid = codeFromCoupon(str);
            var PromoCode = sameMiniGuid + intGuid.ToString().Substring(0, 6);
            Random num = new Random();

            PromoCode = new string(PromoCode.ToCharArray().
                            OrderBy(s => (num.Next(2) % 2) == 0).ToArray());
            return PromoCode;
        }

        const string ALPHABET = "A1G4FO6LEWV3TC7P8Y3ZHNI7UDBX8SM0QKzawemckdjeufysoeui";
        public static uint codeFromCoupon(string coupon)
        {
            uint n = 0;
            for (int i = 0; i < 6; ++i)
                n = n | (((uint)ALPHABET.IndexOf(coupon[i])) << (5 * i));
            return n;
        }


    }
}
