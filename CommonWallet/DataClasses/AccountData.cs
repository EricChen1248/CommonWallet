using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWallet.DataClasses
{
    public class AccountData
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AccountName { get; set; }
        public decimal Money { get; set; }
    }
}
