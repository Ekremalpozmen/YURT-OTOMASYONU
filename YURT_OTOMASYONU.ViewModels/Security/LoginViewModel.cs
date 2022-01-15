using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURT_OTOMASYONU.ViewModels.Security
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual bool RememberMe { get; set; }
    }
}
