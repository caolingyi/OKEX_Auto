namespace OKEX.Auto.Core.Utilities
{
    public class JsonResponseModel
    {
        public JsonResponseModel(bool ok = true, string message = null)
        {
            Ok = ok;
            Message = message;
        }
        public bool Ok { get; private set; } = true;
        public string Message { get; set; }
    }
    public class JsonResponseModel<T> : JsonResponseModel
    {
        public T Data { get; private set; }
        public JsonResponseModel(T data, bool ok = true, string message = null) : base(ok, message)
        {
            Data = data;
        }
    }
}