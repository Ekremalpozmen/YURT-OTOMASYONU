using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURT_OTOMASYONU.ViewModels.Student
{
    public class CallHelpViewModel
    {
        public int Id { get; set; }
        public Nullable<int> OgrenciId { get; set; }
        public Nullable<int> EnvanterNo { get; set; }
        public string Aciklama { get; set; }
        public Nullable<bool> KabulDurumu { get; set; }

    }
}
