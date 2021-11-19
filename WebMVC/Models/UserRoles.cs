using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class UserToken
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
