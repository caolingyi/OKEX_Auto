using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OKEX.Auto.Core.Domain.Manager.OKEX;
using OKEX.Auto.Core.Domain.Models.Okex.Account;

namespace OKEX.Auto.TradeApi.Controllers.Okex
{
    [Route("api/OkexAccount")]
    [ApiController]
    public class OkexAccountController : ControllerBase
    {
        private readonly ILogger _logger;

        public OkexAccountController(
            ILogger<OkexAccountController> logger
            )
        {
            _logger = logger;
        }


        [Route("getWalletInfo")]
        [HttpGet]
        public async Task<List<OkexAccountWallet>> getWalletInfoAsync()
        {
            var data = await new OkexAccountManager().getWalletInfoAsync();
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        [Route("getWithDrawalFee")]
        [HttpGet]
        public async Task<List<OkexAccountWithDrawalFee>> getWithDrawalFeeAsync(string currency)
        {
            var data = await new OkexAccountManager().getWithDrawalFeeAsync(currency);
            return data;
        }

    }
}