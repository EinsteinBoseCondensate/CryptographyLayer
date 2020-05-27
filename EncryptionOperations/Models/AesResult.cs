using System;
using System.Collections.Generic;
using System.Text;

namespace CryptographyLayer.EncryptionOperations.Models
{
    public class AesResult
    {
        public string CipheredData { get; set; }
        public string Nonce { get; set; }
    }
}
