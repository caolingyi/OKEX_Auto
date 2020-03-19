using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OKEX.Auto.TradeApi.Controllers
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name=""></param>
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
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
                return Ok("ok 了");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "失败！");
                return StatusCode(500);
            }
        }
    }
}