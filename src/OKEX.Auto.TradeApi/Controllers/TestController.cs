using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OKEX.Auto.Core.Domain.AggregatesModel;
using OKEX.Auto.Core.Domain.Interface;
using OKEX.Auto.Core.Utilities;

namespace OKEX.Auto.TradeApi.Controllers
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IBaseCurrencyRepository _baseCurrencyRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name=""></param>
        public TestController(ILogger<TestController> logger,
            IBaseCurrencyRepository baseCurrencyRepository)
        {
            _logger = logger;
            _baseCurrencyRepository = baseCurrencyRepository;
        }

        /// <summary>
        /// get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var model = new BaseCurrency
                {
                    currency = "test1",
                    name = "test2",
                    can_deposit = "1",
                    can_withdraw = "0",
                    min_withdrawal = "100"
                };
                await _baseCurrencyRepository.InsertAsync(model);
                return Ok(JsonHelper.ToJson(model));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "失败！");
                return StatusCode(500);
            }
        }
    }
}