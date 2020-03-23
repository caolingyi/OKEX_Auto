using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OKEX.Auto.Core.ExtensionHttpClient;
using OKEX.Auto.Core.ExtensionHttpClient.Interface;

namespace OKEX.Auto.TradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BIBILeverageController : ControllerBase
    {

        private readonly ILogger<BIBILeverageController> _logger;
        private readonly OKEXSettings _oKEXSettings;
        private readonly IOKEXHttpClient _iOKEXHttpClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="publicContractKLineRepository"></param>
        /// <param name="oKEXSettings"></param>
        /// <param name="iOKEXHttpClient"></param>
        public BIBILeverageController(ILogger<BIBILeverageController> logger,
            OKEXSettings oKEXSettings,
            IOKEXHttpClient iOKEXHttpClient)
        {
            _logger = logger;
            _oKEXSettings = oKEXSettings;
            _iOKEXHttpClient = iOKEXHttpClient;
        }

        [HttpGet]
        [Route("instruments")]
        public async Task<ActionResult> instruments()
        {
            try
            {
                var url = _oKEXSettings.BaseUrl + BIBI.instrumentsUrl;
                var response = await _iOKEXHttpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<ArrayList>(response);
                if (response[0] == '[')
                {
                    
                        await _publicContractKLineRepository.AddRangeAsync(list);
                }
                else
                {
                    return Ok(response);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "失败！");
                return StatusCode(500);
            }
        }
    }
}