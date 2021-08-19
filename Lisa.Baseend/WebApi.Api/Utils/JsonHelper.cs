using Newtonsoft.Json;

namespace WebApi.Api.Utils
{
    public class JsonHelper
    {
        public static string Serialize(dynamic obj){
            JsonSerializerSettings settings = new JsonSerializerSettings();

            // 避免循环引用
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // 指定日期的字符串格式
            settings.DateFormatString="yyyy-MM-dd HH:mm:ss";

            // 让属性序列化为小驼峰格式（属性名首字母小写）
            settings.ContractResolver=new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();


            return JsonConvert.SerializeObject(obj,Formatting.Indented,settings);
        }
    }
}