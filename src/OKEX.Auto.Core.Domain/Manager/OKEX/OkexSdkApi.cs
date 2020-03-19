using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OKEX.Auto.Core.Domain.Manager.OKEX
{
    /// <summary>
    /// okex api common params
    /// </summary>
    public class OkexSdkApi
    {

        private readonly List<UerrApiConfig> UerrApiConfigList = new List<UerrApiConfig>()
        {
            
            new UerrApiConfig(){ UserID = Guid.Parse("06624528-0586-4b41-a80d-c35041ca4ec6"),
                ApiKey = "5b4950b0-b201-4647-a15a-b2457df6d844",Secret="79C44F4CC9266CD1B1B32A7A88EDDF4E",PassPhrase="cly11280730"},
        };

        protected string BASEURL = "https://www.okex.me/";

        protected string _apiKey;
        protected string _secret;
        protected string _passPhrase;

        public OkexSdkApi(string apiKey, string secret, string passPhrase)
        {
            this._apiKey =apiKey;
            this._secret =  secret;
            this._passPhrase = passPhrase;
        }

        public OkexSdkApi(Guid userID)
        {
            var uerrApiConfig = UerrApiConfigList.FirstOrDefault(p=>p.UserID == userID);
            this._apiKey = uerrApiConfig.ApiKey;
            this._secret = uerrApiConfig.Secret;
            this._passPhrase = uerrApiConfig.PassPhrase;
        }

        public OkexSdkApi()
        {
            var uerrApiConfig = UerrApiConfigList.FirstOrDefault();
            this._apiKey = uerrApiConfig.ApiKey;
            this._secret = uerrApiConfig.Secret;
            this._passPhrase = uerrApiConfig.PassPhrase;
        }

    }


    public class UerrApiConfig
    {
        /// <summary>
        ///用戶id
        /// </summary>

        public Guid UserID { get; set; }

        public string ApiKey { get; set; }

        public string Secret { get; set; }

        public string PassPhrase { get; set; }

    }

}
