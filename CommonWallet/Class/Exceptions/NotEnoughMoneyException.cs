using System;

namespace CommonWallet.Class.Exceptions
{
    [Serializable]
    internal class NotEnoughMoneyException : Exception
    {
        internal NotEnoughMoneyException(string walletName) : base($"{walletName} 金額不足")
        {
            
        }
    }
}
