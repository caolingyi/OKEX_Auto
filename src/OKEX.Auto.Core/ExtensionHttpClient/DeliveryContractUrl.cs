using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.ExtensionHttpClient
{
    public static class DeliveryContractUrl
    {
        public static string KLineUrl = $"/api/futures/v3/instruments/{0}/candles?start={1}&end={2}&granularity={3}";
    }
}
