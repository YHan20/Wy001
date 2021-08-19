namespace WebApi.Api.ParamModel
{
    public class Pager
    {
        // 当前页码（当前第几页）
        public int PageIndex{get;set;}

        // 当前页大小（每页条数）
        public int PageSize{get;set;}

        // 总条数
        public int RowsTotal{get;set;}
    }
}