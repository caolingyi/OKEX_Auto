using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                //var result = JsonConvert.DeserializeObject<ArrayList>(response);
                if (response[0] == '[')
                {
                    var result = JArray.Parse(response);

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

                            model.timestamp = item[0].ToString();
                            model.open = item[1].ToString();
                            model.high = item[2].ToString();
                            model.low = item[3].ToString();
                            model.close = item[4].ToString();
                            model.volume = item[5].ToString();
                            model.currency_volume = item[6].ToString();
                            list.Add(model);
                        }
                        await _publicContractKLineRepository.AddRangeAsync(list);
                    }
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

        [NonAction]
        private string[,] TranStrToTwoArray(string original)
        {
            if (original.Length == 0)
            {
                throw new IndexOutOfRangeException("original's length can not be zero");
            }
            //将字符串转换成数组（字符串拼接格式：***,***#***,***#***,***，例如apple,banana#cat,dog#red,black）
            string[] originalRow = original.Split('#');
            string[] originalCol = originalRow[0].Split(','); //string[,]是等长数组，列维度一样，只要取任意一行的列维度即可确定整个二维数组的列维度
            int x = originalRow.Length;
            int y = originalCol.Length;
            string[,] twoArray = new string[x, y];
            for (int i = 0; i < x; i++)
            {
                originalCol = originalRow[i].Split(',');
                for (int j = 0; j < originalCol.Length; j++)
                {
                    twoArray[i, j] = originalCol[j];
                }
            }
            return twoArray;
        }
    }
}