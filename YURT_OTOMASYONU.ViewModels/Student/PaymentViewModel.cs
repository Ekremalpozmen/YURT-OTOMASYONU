using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURT_OTOMASYONU.ViewModels.Student
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public Nullable<int> OgrenciId { get; set; }
        public Nullable<System.DateTime> OdemeTarihi { get; set; }
        public Nullable<int> Tutar { get; set; }
    }
}
