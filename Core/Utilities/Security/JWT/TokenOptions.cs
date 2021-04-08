using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class TokenOptions
    {
        /// <summary>
        /// Kullanıcılar
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// İmzalayanlar
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Token süresi(Dk)
        /// </summary>
        public int AccessTokenExpiration { get; set; }

        public string SecurityKey { get; set; }

    }
}
