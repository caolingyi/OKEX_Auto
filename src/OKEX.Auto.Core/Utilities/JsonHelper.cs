using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OKEX.Auto.Core.Utilities
{

    /// <summary>
    /// JSON序列化、反序列化扩展类。
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 对象序列化成JSON字符串。
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <param name="ignoreProperties">设置需要忽略的属性</param>
        /// <returns></returns>
        public static string ToJson(object obj,params string[] ignoreProperties)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    DateFormatString = "yyyy-MM-dd HH:mm:ss",
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new JsonPropertyContractResolver(ignoreProperties)
                };
                return JsonConvert.SerializeObject(obj, settings);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// JSON字符串序列化成对象。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(string json)
        {
            try
            {
                return (json == null) ? default(T) : JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// JSON字符串序列化成集合。
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns></returns>
        public static List<T> ToList<T>(string json)
        {
            try
            {
                return json == null ? null : JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch (Exception)
            {
                return default(List<T>);
            }
        }

        /// <summary>
        /// JSON字符串序列化成DataTable。
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns></returns>
        public static DataTable ToTable(string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject<DataTable>(json);
        }
    }

    /// <summary>
    /// JSON分解器-设置。
    /// </summary>
    public class JsonPropertyContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// 需要排除的属性。
        /// </summary>
        private IEnumerable<string> _listExclude;

        public JsonPropertyContractResolver(params string[] ignoreProperties)
        {
            this._listExclude = ignoreProperties;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            //设置需要输出的属性。
            return base.CreateProperties(type, memberSerialization).ToList().FindAll(p => !_listExclude.Contains(p.PropertyName));
        }
    }
    
}
