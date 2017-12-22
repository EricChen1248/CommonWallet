using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWallet.DataClasses
{
    public class WalletData
    {
        public int ID { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }
    }
}
