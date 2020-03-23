using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.ExtensionHttpClient
{
    public static class DeliveryContractUrl
    {
        public static string KLineUrl = "/api/futures/v3/instruments/{0}/candles?start={1}&end={2}&granularity={3}";
    }
    public static class BIBI
    {
        public static string instrumentsUrl = "/api/spot/v3/instruments";
        public static string bookUrl = "/api/spot/v3/instruments/{0}/book?size={1}&depth={2}";
        public static string tickerUrl = "/api/spot/v3/instruments/ticker";
        public static string instrumenttickerUrl = "/api/spot/v3/instruments/{0}/ticker";
        public static string tradesUrl = "/api/spot/v3/instruments/{0}/trades?limit={1}";
        public static string candlesUrl = "/api/spot/v3/instruments/{0}/candles?granularity={1}&start={2}&end={3}";
        public static class BIBILeverage
        {
            public static string accountsUrl = "/api/margin/v3/accounts";
            public static string instrumentaccountsUrl = "/api/margin/v3/accounts/{0}";
            public static string ledgerUrl = "/api/margin/v3/accounts/{0}/ledger?limit={1}&type={2}&after={3}";
            public static string availabilityUrl = "/api/margin/v3/accounts/availability";
            public static string instrumentavailabilityUrl = "/api/margin/v3/accounts/{0}/availability";
            public static string borrowedUrl = "/api/margin/v3/accounts/borrowed?status={0}&limit={1}&after={2}";
            public static string instrumentborrowedUrl = "/api/margin/v3/accounts/{0}/borrowed?limit={1}&status={2}&after={3}";
            public static string borrowUrl = "/api/margin/v3/accounts/borrow";
            public static string repaymentUrl = "/api/margin/v3/accounts/repayment";
            public static string ordersUrl = "/api/margin/v3/orders";
            public static string cancel_ordersUrl = "/api/margin/v3/cancel_orders/{0}";
            public static string getOrdersUrl = "/api/margin/v3/orders?instrument_id={0}&state={1}&limit={2}&after={3}";
            public static string getOrderUrl = "/api/margin/v3/orders/{0}";
            public static string orders_pendingUrl = "/api/margin/v3/orders_pending?limit={0}&instrument_id={1}&after={2}";
            public static string fillsUrl = "/api/margin/v3/fills?order_id={0}&instrument_id={1}&limit={2}&after={3}";
            public static string getleverageUrl = "/api/margin/v3/accounts/{0}/leverage"; 
            public static string setleverageUrl = "/api/margin/v3/accounts/{0}/leverage";

            public static string mark_priceUrl = "/api/margin/v3/instruments/{0}/mark_price";
        }
    }
    
}
