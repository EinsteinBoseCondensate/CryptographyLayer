using System;
using System.Collections.Generic;
using System.Text;

namespace CryptographyLayer
{
    public class CryptoConfig
    {
        public CryptographySection CryptographySection { get; set; }
    }

    public class CryptographySection
    {
        public string AesKey { get; set; }
        public string HashSalt { get; set; }
    }
}
