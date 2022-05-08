using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURT_OTOMASYONU.ViewModels.Student
{
    public class PermissionViewModel
    {
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string Aciklama { get; set; }
        public int İletisimNumarasi { get; set; }
        public bool KabulDurumu { get; set; }
    }
}
