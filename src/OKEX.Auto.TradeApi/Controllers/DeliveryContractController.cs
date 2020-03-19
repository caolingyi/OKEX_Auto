using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OKEX.Auto.Core.Domain.AggregatesModel.contract;
using OKEX.Auto.Core.Domain.Interface;
using OKEX.Auto.Core.ExtensionHttpClient;
using OKEX.Auto.Core.ExtensionHttpClient.Interface;
using OKEX.Auto.TradeApi.Model;

namespace OKEX.Auto.TradeApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class DeliveryContractController : ControllerBase
    {
        private readonly ILogger<DeliveryContractController> _logger;
        private readonly IPublicContractKLineRepository _publicContractKLineRepository;
        private readonly OKEXSettings _oKEXSettings;
        private readonly IOKEXHttpClient _iOKEXHttpClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="publicContractKLineRepository"></param>
        /// <param name="oKEXSettings"></param>
        /// <param name="iOKEXHttpClient"></param>
        public DeliveryContractController(ILogger<DeliveryContractController> logger,
            IPublicContractKLineRepository publicContractKLineRepository,
            OKEXSettings oKEXSettings,
            IOKEXHttpClient iOKEXHttpClient)
        {
            _logger = logger;
            _publicContractKLineRepository = publicContractKLineRepository;
            _oKEXSettings = oKEXSettings;
            _iOKEXHttpClient = iOKEXHttpClient;
        }

        /// <summary>
        /// 请求交易平台 k线数据并保存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RequestKLine")]
        public async Task<ActionResult> RequestKLine()
        {
            try
            {
                var requestModel = new KLineRequestModel
                {
                    instrument_id = "BTC-USD-200320",
                    start = "2020-01-01T00:00:00Z",
                    end = "2020-03-19T00:00:00Z",
                    granularity = "900"
                };

                var url = _oKEXSettings.BaseUrl + string.Format(DeliveryContractUrl.KLineUrl, requestModel.instrument_id, requestModel.start, requestModel.end, requestModel.granularity);
                var response = await _iOKEXHttpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<List<KLineResponseModel>>(response);
                if (result.Count > 0)
                {
                    var list = new List<PublicContractKLine>();
                    foreach (var item in result)
                    {
                        var model = new PublicContractKLine();
                        model.instrument_id = requestModel.instrument_id;
                        model.start = requestModel.start;
                        model.end = requestModel.end;
                        model.granularity = requestModel.granularity;

                        model.timestamp = item.timestamp;
                        model.open = item.open;
                        model.high = item.high;
                        model.low = item.low;
                        model.close = item.close;
                        model.volume = item.volume;
                        model.currency_volume = item.currency_volume;
                        list.Add(model);
                    }
                    await _publicContractKLineRepository.AddRangeAsync(list);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "失败！");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// 获取库中的k线数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetKLine")]
        public async Task<ActionResult> GetKLine()
        {
            try
            {
                var list = await _publicContractKLineRepository.GetListAsync();

                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "失败！");
                return StatusCode(500);
            }
        }
    }
}