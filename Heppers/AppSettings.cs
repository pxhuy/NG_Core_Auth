using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NG_Core_Auth.Heppers
{
    public class AppSettings
    {
        // Properties for JWT Token Signture

        public string Site { get; set; }
        public string Audience { get; set; } // khán giả ??
        public string ExpireTime { get; set; } // thời gian hết hạn
        public string Secret { get; set; }

    }
}
