using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KB_RSA
{
    public class RSAKeyModel
    {
        public RSAParameters publicKey { get; set; }
        public RSAParameters privateKey { get; set; }
    }
}
