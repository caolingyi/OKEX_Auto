using OKEX.Auto.Core.Domain.Models.Okex.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.Domain.Interface.OKEX
{
    interface IOkexAccountManager
    {
        Task<List<OkexAccountWallet>> getWalletInfoAsync();

        Task<List<OkexAccountWithDrawalFee>> getWithDrawalFeeAsync(string currency);
    }
}
