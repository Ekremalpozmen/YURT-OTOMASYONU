using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YURT_OTOMASYONU.Data;
using YURT_OTOMASYONU.ViewModels.Security;

namespace YURT_OTOMASYONU.Services.Security
{
    public class SecurityService
    {
        private readonly YurtOtomasyonEntities _context;

        public SecurityService(YurtOtomasyonEntities context)
        {
            _context = context;

        }
        public Ogrenci OgrenciLogin(LoginViewModel login)
        {
            return _context.Ogrenci.FirstOrDefault(x => x.Sifre == login.Password && x.KullaniciAdi == login.Username);
        }
    }
}
