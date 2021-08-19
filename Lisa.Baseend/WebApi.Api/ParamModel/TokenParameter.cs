namespace WebApi.Api.ParamModel
{
    public class TokenParameter
    {
        //token的密钥，不能泄露，泄露意味着所有人都可以生成你的token并且访问你的服务或者接口
        public string Secret { get; set; }
        //发行人（可以是个人或组织）
        public string Issuer { get; set; }
        //token的作用时间，单位为分钟，过期后需要重新获取
        public int AccessExpiration { get; set; }
        //类似一个token的东西的作用时间，单位为分钟，用于重新获取token
        public int RefreshExpiration { get; set; }
    }
}